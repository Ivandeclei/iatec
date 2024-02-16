using AutoMapper;
using DomainLayer.Models;
using DomainServiceLayer.Interfaces;
using Iatec.Constants;
using Iatec.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iatec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _userService;
        private readonly IMapper _mapper;
        public ParticipantController(IParticipantService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route(UriTemplates.PARTICIPANT)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginateFilterDto paginateFilterDto)
        {
            var filter = _mapper.Map<PaginateFilter>(paginateFilterDto);
            var result = await _userService.GetAllAsync(filter);
            var userReturn = _mapper.Map<IEnumerable<ParticipantResult>>(result);
            return Ok(userReturn);
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route(UriTemplates.PARTICIPANT_GET_FIND)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _userService.GetAsync(id);
            var userReturn = _mapper.Map<ParticipantResult>(result);
            return Ok(userReturn);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(UriTemplates.PARTICIPANT)]
        public async Task<IActionResult> InsertAsync([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<Participant>(userDto);
            await _userService.InsertAsync(user);
            return Ok();
        }

        [Authorize("Bearer")]
        [HttpPut]
        [Route(UriTemplates.PARTICIPANT)]
        public async Task<IActionResult> Update([FromBody] ParticipantDtoPut participantDtoPut)
        {
            var user = _mapper.Map<Participant>(participantDtoPut);
            await _userService.UpdateAsync(user);
            return Ok();
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route(UriTemplates.PARTICIPANT)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
    }
}