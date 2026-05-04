using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager _servicemanager):ControllerBase
    {

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest loginrequest)
        {
            var user = await _servicemanager.IAuthenticationService.LoginAsync(loginrequest);
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest registerrequest)
        {
            return Ok( await _servicemanager.IAuthenticationService.RegisterAsync(registerrequest) );
        }

        [HttpPost("AddOrUpdateAddress")]
        public async Task<ActionResult<AddressDto>> AddOrUpdateAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedaddress = await _servicemanager.IAuthenticationService.CreateOrUpdateAddressAsync(address, email);
            return Ok(updatedaddress);
        }

    }
}
