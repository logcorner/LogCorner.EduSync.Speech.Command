using LogCorner.EduSync.Speech.Application.UseCases;
using LogCorner.EduSync.Speech.Presentation.Dtos;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LogCorner.EduSync.Speech.Presentation.Controllers
{
    [Route("api/speech")]
    public class SpeechController : ControllerBase
    {
        private readonly IRegisterSpeechUseCase _registerSpeechUseCase;
        private readonly IUpdateSpeechUseCase _updateSpeechUseCase;

        public SpeechController(IRegisterSpeechUseCase registerSpeechUseCase, IUpdateSpeechUseCase updateSpeechUseCase)
        {
            _registerSpeechUseCase = registerSpeechUseCase;
            _updateSpeechUseCase = updateSpeechUseCase;
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SpeechForUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateSpeechCommandMessage(
                dto.Id == Guid.Empty ? throw new PresentationException("The speechId cannot be empty") : dto.Id,
                dto.Title, dto.Description,
                dto.Url,
                dto.Type,
                dto.Version);

            await _updateSpeechUseCase.Handle(command);
            return Ok();
        }
    }
}