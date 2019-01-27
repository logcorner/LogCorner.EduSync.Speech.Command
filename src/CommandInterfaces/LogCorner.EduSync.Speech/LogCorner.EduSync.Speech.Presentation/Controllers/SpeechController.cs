using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Presentation.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Presentation.Controllers
{
    [Route("api/speech")]
    public class SpeechController : ControllerBase
    {
        private readonly IRegisterSpeechUseCase _registerSpeechUseCase;

        public SpeechController(IRegisterSpeechUseCase registerSpeechUseCase)
        {
            _registerSpeechUseCase = registerSpeechUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SpeechForCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new RegisterSpeechCommandMessage(dto.Title, dto.Description, dto.Url, dto.Type);

            await _registerSpeechUseCase.Handle(command);
            return Ok();
        }
    }
}