using AutoMapper;
using DomainLayer.Models;
using DomainServiceLayer.Interfaces;
using Iatec.Constants;
using Iatec.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iatec.Controllers
{
    public class EventParticipantController : ControllerBase
    {
        private readonly IParticipantEventService _participantEventService;
        private readonly IMapper _mapper;
        public EventParticipantController(IParticipantEventService participantEventService, IMapper mapper)
        {
            _participantEventService = participantEventService;
            _mapper = mapper;
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route(UriTemplates.PARTICIPANT_EVENT)]

        public async Task<IActionResult> InsertAsync([FromBody] ParticipantEventPost eventParticipantDto)
        {
                var eventParticipant = _mapper.Map<EventParticipant>(eventParticipantDto);
                await _participantEventService.InsertAsync(eventParticipant);
                return Ok();
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route(UriTemplates.PARTICIPANT_EVENT)]
        public async Task<IActionResult> Delete(ParticipantEventPost eventParticipantDto)
        {
            var eventParticipant = _mapper.Map<EventParticipant>(eventParticipantDto);
            await _participantEventService.DeleteAsync(eventParticipant);
            return Ok();
        }
    }
}
