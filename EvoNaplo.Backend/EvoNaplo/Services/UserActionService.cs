using AutoMapper;
using EvoNaplo.Models.DTO;
using EvoNaplo.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvoNaplo.Services
{
    public class UserActionService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public UserActionService(UserManager<IdentityUser<Guid>> userManager, IMapper mapper, RoleManager<IdentityRole<Guid>> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        

        //Paramshoz a roleName. Valamiért nem működik bodyból jsonnel
        /*public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided.");
            }

            var newRole = new RoleClass
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }*/


        /// <summary>
        /// Felhasználó jogkörhöz rendelése
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<int> AddUserToRole(string userEmail, string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);
            if (user == null)
            {
                return StatusCodes.Status400BadRequest;
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return StatusCodes.Status200OK;
            }

            return StatusCodes.Status500InternalServerError;
        }

        public async Task<string> SignInUser(LoginDTO loginDto)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == loginDto.Email);
            var userSigninResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return GenerateJwt(user, roles);
            }
            else
            {
                return StatusCodes.Status500InternalServerError.ToString();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Diákokkal kapcsolatos dolgok
        public async Task<int> SignUpStudent(StudentDto studentDto)
        {
            var user = _mapper.Map<StudentDto, IdentityUser<Guid>>(studentDto);
            var userCreateResult = await _userManager.CreateAsync(user, studentDto.Password);
            if (userCreateResult.Succeeded)
            {
                return StatusCodes.Status200OK;
            }
            return StatusCodes.Status500InternalServerError;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<int> SignUpMentor(MentorDto mentorDto)
        {
            var user = _mapper.Map<MentorDto, IdentityUser<Guid>>(mentorDto);
            var userCreateResult = await _userManager.CreateAsync(user, mentorDto.Password);
            if (userCreateResult.Succeeded)
            {
                return StatusCodes.Status200OK;
            }
            return StatusCodes.Status500InternalServerError;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// This generates a JWT for user about to login
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private string GenerateJwt(IdentityUser<Guid> user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
