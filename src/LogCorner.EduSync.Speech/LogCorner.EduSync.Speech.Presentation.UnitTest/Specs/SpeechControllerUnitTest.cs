using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Presentation.Controllers;
using LogCorner.EduSync.Speech.Presentation.Dtos;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace LogCorner.EduSync.Speech.Presentation.UnitTest.Specs
{
    public class SpeechControllerUnitTest
    {
        [Fact(DisplayName = "Register Speech With Invalid ModelState Return BadRequest")]
        public async Task RegisterSpeechWithInvalidModelStateReturnBadRequest()
        {
            //Arrange
            var moq = new Mock<IRegisterSpeechUseCase>();
            var sut = new SpeechController(moq.Object);
            sut.ModelState.AddModelError("x", "Invalid ModelState");

            //Act
            IActionResult result = await sut.Post(It.IsAny<SpeechForCreationDto>());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Register Speech With Valid ModelState Return Ok")]
        public async Task RegisterSpeechWithValidModelStateReturnOk()
        {
            //Arrange
            RegisterSpeechCommandMessage registerSpeechCommandMessage = null;
            var moq = new Mock<IRegisterSpeechUseCase>();
            moq.Setup(x => x.Handle(It.IsAny<RegisterSpeechCommandMessage>()))
                .Returns(Task.CompletedTask)
                .Callback<RegisterSpeechCommandMessage>(x => registerSpeechCommandMessage = x);

            var speechForCreationDto = new SpeechForCreationDto
            {
                Title = "is simply dummy text of the printing",
                Description =
                    @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy
                                text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",
                Type = "1",
                Url = "http://myjpg.jpg",
            };

            var sut = new SpeechController(moq.Object);

            //Act
            IActionResult result = await sut.Post(speechForCreationDto);

            //Assert
            Assert.IsType<OkResult>(result);
            moq.Verify(x => x.Handle(It.IsAny<RegisterSpeechCommandMessage>()), Times.Once);
            Assert.Equal(speechForCreationDto.Title, registerSpeechCommandMessage.Title);
            Assert.Equal(speechForCreationDto.Description, registerSpeechCommandMessage.Description);
            Assert.Equal(speechForCreationDto.Type, registerSpeechCommandMessage.Type);
            Assert.Equal(speechForCreationDto.Url, registerSpeechCommandMessage.Url);
        }

        [Fact(DisplayName = "Register Speech With Exception Return InternalServerError")]
        public async Task RegisterSpeechWithExceptionReturnInternalServerError()
        {
            // Arrange
            var mockLog = new Mock<ILogger<SpeechController>>();

            Mock<ILoggerFactory> loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(l => l.CreateLogger(It.IsAny<string>())).Returns(mockLog.Object);

            var middleware = new ExceptionMiddleware(innerHttpContext =>
                throw new PresentationException("Internal Server Error"), loggerFactory.Object);

            var context = new DefaultHttpContext();

            //Act
            await middleware.InvokeAsync(context);

            //Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }
}