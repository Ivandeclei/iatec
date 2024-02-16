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
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }
        
        [Authorize("Bearer")]
        [HttpGet]
        [Route(UriTemplates.EVENT)]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] EventFilterParametersDto? eventFilterParametersDto)
        {
            var filter = _mapper.Map<EventFilterParameters>(eventFilterParametersDto);
           
            var result = await _eventService.GetAllAsync(filter);
            var eventReturn = _mapper.Map<IEnumerable<EventResult>>(result);
            return Ok(eventReturn);
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route(UriTemplates.EVENT_GET_FIND)]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var result = await _eventService.GetAsync(id);
            var eventReturn = _mapper.Map<EventResult>(result);
            return Ok(eventReturn);
        }

        [Authorize("Bearer")]
        [HttpPost]
        [Route(UriTemplates.EVENT)]
        public async Task<IActionResult> InsertCustomerAsync([FromBody] EventBaseDto eventDto)
        {
            
            var eventItem = _mapper.Map<Event>(eventDto);
            await _eventService.InsertAsync(eventItem);
            return Ok();
        }

        [Authorize("Bearer")]
        [HttpPut]
        [Route(UriTemplates.EVENT)]
        public async Task<IActionResult> UpdateCustomer([FromBody] EventBaseDto eventDto)
        {
            var eventItem = _mapper.Map<Event>(eventDto);
            await _eventService.UpdateAsync(eventItem);
            return Ok();
        }

        [Authorize("Bearer")]
        [HttpDelete]
        [Route(UriTemplates.EVENT)]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _eventService.DeleteAsync(id);
            return Ok();
        }
    }
}
