using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResponse> LoginAsync(LoginRequest loginrequest);
        public Task<UserResponse> RegisterAsync(RegisterRequest registerrequest);

        public Task<AddressDto> CreateOrUpdateAddressAsync(AddressDto address, string email);
    }
}
