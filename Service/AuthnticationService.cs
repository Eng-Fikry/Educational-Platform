using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service_Abstraction;
using Shared.DTOs.Basket;
using Shared.DTOs.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service
{
    public class AuthnticationService(IConfiguration _configuration,UserManager<User> _userManager, IUnitOfWork _unitOfWork,IMapper _mapper) : IAuthnticationService
    {
        public async Task<UserDto> RegisterAsync(RegisterDto Data)
        {
            var checkemail = await _userManager.FindByEmailAsync(Data.Email);
            ValidateData(Data, checkemail);

            var user = _mapper.Map<User>(Data);

            var newaccount = await _userManager.CreateAsync(user, Data.Password);

            if (newaccount.Succeeded)
            {

                await CreateRoleAndAddUserAccountAsync(Data, user, _unitOfWork);
                // Repository //
                return new UserDto()
                {
                    Id = user.Id,
                    DisplayName = Data.DisplayName!,
                    Email = Data.Email,
                    Role = Data.Role,
                    Token = await CreateTokenAsync(user)
                };
            }
            //هندل الايرور
            throw new Exception();


        }

        private static void ValidateData(RegisterDto Data, User? checkemail)
        {
            string Role = Data.Role;
            if (Data.Password != Data.ConfirmPassword)
                throw new BadRequestException($"The passwords do not match.");
            else if (checkemail is not null)
                throw new BadRequestException($"This email '{Data.Email}' is already registered.");
            else if (Role !="Teacher" && Role !="Student")
                throw new BadRequestException($"'{Role}' Is No Role ");
        }


        public async Task<UserDto> LoginAsync(LoginDto Data)
        {
            var Email= await _userManager.FindByEmailAsync(Data.Email) ?? throw new Exception();//هندل الايرور
            var Password = await _userManager.CheckPasswordAsync(Email, Data.Password);
            var Role = await _userManager.GetRolesAsync(Email);
            if (Password) 
            {
                return new UserDto()
                {
                    Id = Email.Id,
                    DisplayName = Email.DisplayName,
                    Email = Data.Email,
                    Role = Role.FirstOrDefault()!,
                    Token = await CreateTokenAsync(Email)
                };
            }
            throw new Exception();   //هندل الايرور


        }
        public async Task<bool> CheckEmailExistAsync(string Email)
        {
            var checkemail= await _userManager.FindByEmailAsync(Email) ?? throw new EmailOrUserNotFoundException($"This Email '{Email} Is NotFound'");
            return checkemail is not null;
        }

        public async Task<UserDto> GetUserAsync(string Email)
        {
            var GetEmail = await _userManager.FindByEmailAsync(Email) ?? throw new EmailOrUserNotFoundException($"This User '{Email} Is NotFound'");//هندل الايرور
            var Roles = await _userManager.GetRolesAsync(GetEmail);
            var Role=Roles.FirstOrDefault();

            return new UserDto()
            {
                Id = GetEmail.Id,
                DisplayName = GetEmail.DisplayName,
                Email = Email,
                Role = Role!,
                Token = await CreateTokenAsync(GetEmail)
            };
        }

        private async Task<string> CreateTokenAsync(User user) 
        {
            var Claims = new List<Claim>() 
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName!)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
                Claims.Add(new Claim(ClaimTypes.Role,role));
            var secret = _configuration["JWT:SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var Credintials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Audience = _configuration["JWT:Audience"];
            var Issure = _configuration["JWT:Issure"];
            var Expierd = DateTime.UtcNow.AddHours(1);
            var Token = new JwtSecurityToken(issuer: Issure, audience: Audience, claims: Claims, expires: Expierd, signingCredentials: Credintials);
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        private async Task CreateRoleAndAddUserAccountAsync(RegisterDto Data, User user, IUnitOfWork _unitOfWork)
        {
            dynamic UserAccount;
            switch (Data.Role)
            {
                case "Teacher":
                    await _userManager.AddToRoleAsync(user, "Teacher");
                    UserAccount =  _unitOfWork.GetRepository<Teacher, int>();
                    var Teacher=new Teacher()
                    {
                        Email=user.Email!, 
                        Role=Data.Role,
                        UserId=user.Id,
                        PhoneNumber=user.PhoneNumber!,
                        UserName=user.UserName!,
                        JoinedDate=DateTime.Now,
                        
                        

                    };
                    await UserAccount.AddAsync(Teacher);
                    break;
                case "Student":
                    await _userManager.AddToRoleAsync(user, "Student");
                    UserAccount = _unitOfWork.GetRepository<Student, int>();
                    var BasketId= Guid.NewGuid().ToString();
                    var Student = new Student()
                    {
                        Email = user.Email!,
                        Role = Data.Role,
                        UserId = user.Id,
                        PhoneNumber = user.PhoneNumber!,
                        UserName = user.UserName!,
                        JoinedDate = DateTime.Now,
                        BasketId= BasketId


                    };
                    await UserAccount.AddAsync(Student);

                    break;
                default:
                    throw new BadRequestException($"'{Data.Role}' Is No Role ");

            }
            await _unitOfWork.SaveAsync();
        }


    }
}
