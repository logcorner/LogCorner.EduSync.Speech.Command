using LogCorner.EduSync.Speech.Application.Commands;
using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Presentation.Controllers;
using LogCorner.EduSync.Speech.Presentation.Dtos;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
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
            var moqRegisterSpeechUseCase = new Mock<ICreateSpeechUseCase>();
            var moqUpdateSpeechUseCase = new Mock<IUpdateSpeechUseCase>();
            var sut = new SpeechController(moqRegisterSpeechUseCase.Object, moqUpdateSpeechUseCase.Object, It.IsAny<IDeleteSpeechUseCase>());
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
            var moqRegisterSpeechUseCase = new Mock<ICreateSpeechUseCase>();
            moqRegisterSpeechUseCase.Setup(x => x.Handle(It.IsAny<RegisterSpeechCommandMessage>()))
                .Returns(Task.CompletedTask)
                .Callback<RegisterSpeechCommandMessage>(x => registerSpeechCommandMessage = x);
            var moqUpdateSpeechUseCase = new Mock<IUpdateSpeechUseCase>();
            var speechForCreationDto = new SpeechForCreationDto
            {
                Title = "is simply dummy text of the printing",
                Description =
                    @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy
                                text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",
                TypeId =  1
               ,
                Url = "http://myjpg.jpg",
            };

            var sut = new SpeechController(moqRegisterSpeechUseCase.Object, moqUpdateSpeechUseCase.Object, It.IsAny<IDeleteSpeechUseCase>());

            //Act
            IActionResult result = await sut.Post(speechForCreationDto);

            //Assert
            Assert.IsType<OkResult>(result);
            moqRegisterSpeechUseCase.Verify(x => x.Handle(It.IsAny<RegisterSpeechCommandMessage>()), Times.Once);
            Assert.Equal(speechForCreationDto.Title, registerSpeechCommandMessage.Title);
            Assert.Equal(speechForCreationDto.Description, registerSpeechCommandMessage.Description);
            Assert.Equal(speechForCreationDto.TypeId, registerSpeechCommandMessage.Type);
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

        [Fact(DisplayName = "Update Speech When ModelState Is Invalid Should Return BadRequest")]
        public async Task UpdateSpeechWhenModelStateIsInvalidReturnBadRequest()
        {
            //Arrange
            var moq = new Mock<ICreateSpeechUseCase>();
            var moqUpdateSpeechUseCase = new Mock<IUpdateSpeechUseCase>();
            var sut = new SpeechController(moq.Object, moqUpdateSpeechUseCase.Object, It.IsAny<IDeleteSpeechUseCase>());
            sut.ModelState.AddModelError("x", "Invalid ModelState");

            //Act
            IActionResult result = await sut.Put(It.IsAny<SpeechForUpdateDto>());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Update Speech When SpeechId Is Empty Sould Raise PresentationException")]
        public async Task UpdateSpeechWhenSpeechIdIsEmptySouldRaisePresentationException()
        {
            //Arrange
            var moq = new Mock<ICreateSpeechUseCase>();
            var moqUpdateSpeechUseCase = new Mock<IUpdateSpeechUseCase>();
            var sut = new SpeechController(moq.Object, moqUpdateSpeechUseCase.Object, It.IsAny<IDeleteSpeechUseCase>());

            var dto = new SpeechForUpdateDto
            {
                Id = Guid.Empty
            };

            //Act
            //Assert
            await Assert.ThrowsAnyAsync<PresentationException>(() => sut.Put(dto));
        }

        [Fact(DisplayName = "Update Speech When ModelState Is Valid With No Errors Should Return Ok")]
        public async Task UpdateSpeechWhenModelStateIsValidWithNoErrorsShouldReturnOk()
        {
            //Arrange
            UpdateSpeechCommandMessage updateSpeechCommandMessage = null;
            var moqUpdateSpeechUseCase = new Mock<IUpdateSpeechUseCase>();
            moqUpdateSpeechUseCase.Setup(x =>
                    x.Handle(It.IsAny<UpdateSpeechCommandMessage>()))
                .Returns(Task.CompletedTask)
                .Callback<UpdateSpeechCommandMessage>(x => updateSpeechCommandMessage = x);

            var speechForUpdateDto = new SpeechForUpdateDto
            {
                Title = "New is simply dummy text of the printing",
                Description = @"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy
                                text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged",
                TypeId =  1,
                Url = "http://myjpg.jpg",
                Version = 2,
                Id = Guid.NewGuid()
            };

            var sut = new SpeechController(It.IsAny<ICreateSpeechUseCase>(),
                moqUpdateSpeechUseCase.Object, It.IsAny<IDeleteSpeechUseCase>());

            //Act
            var result = await sut.Put(speechForUpdateDto);

            //Assert
            Assert.IsType<OkResult>(result);
            moqUpdateSpeechUseCase.Verify(x => x.Handle(
                It.IsAny<UpdateSpeechCommandMessage>()), Times.Once);
            Assert.Equal(speechForUpdateDto.Id, updateSpeechCommandMessage.SpeechId);
            Assert.Equal(speechForUpdateDto.Title, updateSpeechCommandMessage.Title);
            Assert.Equal(speechForUpdateDto.Description, updateSpeechCommandMessage.Description);
            Assert.Equal(speechForUpdateDto.TypeId.Value, updateSpeechCommandMessage.Type);
            Assert.Equal(speechForUpdateDto.Url, updateSpeechCommandMessage.Url);
            Assert.Equal(speechForUpdateDto.Version, updateSpeechCommandMessage.OriginalVersion);
        }

        [Fact(DisplayName = "Delete Speech With Invalid ModelState Return BadRequest")]
        public async Task DeleteSpeechWithInvalidModelStateReturnBadRequest()
        {
            //Arrange
            var moqRegisterSpeechUseCase = new Mock<ICreateSpeechUseCase>();
            var moqUpdateSpeechUseCase = new Mock<IUpdateSpeechUseCase>();
            var sut = new SpeechController(moqRegisterSpeechUseCase.Object, moqUpdateSpeechUseCase.Object, It.IsAny<IDeleteSpeechUseCase>());
            sut.ModelState.AddModelError("x", "Invalid ModelState");

            //Act
            IActionResult result = await sut.Delete(It.IsAny<SpeechForDeleteDto>());

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Delete Speech With Valid ModelState Return Ok")]
        public async Task DeleteSpeechWithValidModelStateReturnOk()
        {
            //Arrange
            DeleteSpeechCommandMessage deleteSpeechCommandMessage = null;
            var moqDeleteSpeechUseCase = new Mock<IDeleteSpeechUseCase>();
            moqDeleteSpeechUseCase.Setup(x => x.Handle(It.IsAny<DeleteSpeechCommandMessage>()))
                .Returns(Task.CompletedTask)
                .Callback<DeleteSpeechCommandMessage>(x => deleteSpeechCommandMessage = x);

            var speechForDeleteDto = new SpeechForDeleteDto
            {
                Id = Guid.NewGuid(),
                Version = 1
            };

            var sut = new SpeechController(It.IsAny<ICreateSpeechUseCase>(), It.IsAny<IUpdateSpeechUseCase>(), moqDeleteSpeechUseCase.Object);

            //Act
            IActionResult result = await sut.Delete(speechForDeleteDto);

            //Assert
            Assert.IsType<OkResult>(result);
            moqDeleteSpeechUseCase.Verify(x => x.Handle(It.IsAny<DeleteSpeechCommandMessage>()), Times.Once);
            Assert.Equal(speechForDeleteDto.Id, deleteSpeechCommandMessage.SpeechId);
            Assert.Equal(speechForDeleteDto.Version, deleteSpeechCommandMessage.OriginalVersion);
        }
    }
}