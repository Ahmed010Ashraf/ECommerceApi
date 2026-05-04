using AutoMapper;
using Domain.Exceptions;
using Domain.Models.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstraction;
using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class AuthenticationService(
        UserManager<ApplicationUser> _usermanager,
        IOptions<JWTOptions> jwtOptions,
        IMapper _map
        ) : IAuthenticationService
    {
        public async Task<AddressDto> CreateOrUpdateAddressAsync(AddressDto address , string email)
        {
            var user = await _usermanager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email) ??
                throw new UserNotFoundException(email);

            if(user.Address == null)
            {
                var add = _map.Map<Address>(address);
                user.Address = add;
            }
            else
            {
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;

            }
            await _usermanager.UpdateAsync(user);
            return _map.Map<AddressDto>(user.Address);
        }

        public async Task<UserResponse> LoginAsync(LoginRequest loginrequest)
        {
            //check if the user exist in the database
            var user = await _usermanager.FindByEmailAsync(loginrequest.Email) ??
                throw new UserNotFoundException(loginrequest.Email);

            var isPasswordValid = await _usermanager.CheckPasswordAsync(user, loginrequest.Password);
            if (!isPasswordValid)
                throw new InvalidPasswordException();

            return new UserResponse
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateJwtToken(user)

            };

        }
        public async Task<UserResponse> RegisterAsync(RegisterRequest registerrequest)
        {
            var user = new ApplicationUser
            {
                DisplayName = registerrequest.DisplayName,
                Email = registerrequest.Email,
                UserName = registerrequest.UserName,
                PhoneNumber = registerrequest.PhoneNumber,
            };

            var res = await _usermanager.CreateAsync(user, registerrequest.Password);

            if (res.Succeeded)
            {
                return new UserResponse
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await GenerateJwtToken(user)
                };
            }
            throw new badRequestException(res.Errors.Select(e => e.Description).ToList());
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var options = jwtOptions.Value;
            var claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Email , user.Email),
                //new Claim(JwtRegisteredClaimNames.Sub , user.Id),
                //new Claim(JwtRegisteredClaimNames.GivenName , user.DisplayName),
                new Claim (ClaimTypes.Name , user.DisplayName),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Email , user.Email),
            };

            var roles = await _usermanager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role , role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var signingCredentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(options.DurationInDayes),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    }
