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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route(UriTemplates.LOGIN)]
        public async Task<IActionResult> GetAllAsync([FromBody] UserBaseDto userBaseDto)
        {
            var filter = _mapper.Map<User>(userBaseDto);
            var result = await _userService.FindByUser(filter);
            return Ok(result);
        }
    }
}
