using LogCorner.EduSync.Speech.Application.Commands;
using LogCorner.EduSync.Speech.Application.Interfaces;
using LogCorner.EduSync.Speech.Presentation.Dtos;
using LogCorner.EduSync.Speech.Presentation.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogCorner.EduSync.Speech.Telemetry;

namespace LogCorner.EduSync.Speech.Presentation.Controllers
{
    [Route("api/speech")]
    public class SpeechController : ControllerBase
    {
        private readonly ICreateSpeechUseCase _createSpeechUseCase;
        private readonly IUpdateSpeechUseCase _updateSpeechUseCase;
        private readonly IDeleteSpeechUseCase _deleteSpeechUseCase;

        private readonly IOpenTelemetryService _openTelemetryService;

        public SpeechController(ICreateSpeechUseCase createSpeechUseCase, IUpdateSpeechUseCase updateSpeechUseCase, IDeleteSpeechUseCase deleteSpeechUseCase,
             IOpenTelemetryService openTelemetryService)
        {
            _createSpeechUseCase = createSpeechUseCase;
            _updateSpeechUseCase = updateSpeechUseCase;
            _deleteSpeechUseCase = deleteSpeechUseCase;
            _openTelemetryService = openTelemetryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SpeechForCreationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new RegisterSpeechCommandMessage(dto.Title, dto.Description, dto.Url, dto.TypeId);

            await _createSpeechUseCase.Handle(command);
            _openTelemetryService.DoSomeWork("Create speech", new Dictionary<string, object>
            {
                {"Title",command.Title},
                {"Description",command.Description},
                {"Url",command.Url},
                {"Type",command.Type}
            });
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
                dto.TypeId,
                dto.Version);

            await _updateSpeechUseCase.Handle(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] SpeechForDeleteDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new DeleteSpeechCommandMessage(dto.Id, dto.Version);

            await _deleteSpeechUseCase.Handle(command);
            return Ok();
        }
    }
}