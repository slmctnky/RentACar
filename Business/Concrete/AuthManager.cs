using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using Core.Utilities.Security;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        private ITokenHelper _tokenHelper;
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var result = _tokenHelper.CreateToken(user, _userService.GetClaims(user).Data);
            return new SuccessDataResult<AccessToken>(result, Messages.AccessTokenCreated);

        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
            if (userToCheck==null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password,userToCheck.PasswordHash,userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);

            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);

        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            //if (PasswordHelper.CheckStrength(userForRegisterDto.Password) < PasswordScore.Medium)
            //{
            //    return BadRequest(Messages.WeakPassword);

            //}
            if (!PasswordHelper.ValidatePassword(userForRegisterDto.Password, 5, 8))
            {
                return new ErrorDataResult<User>(Messages.PasswordRequirement);

            }
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            var result= _userService.GetByMail(email);
            if (result.Data!=null)
            {
                return new ErrorResult(Messages.EmailIsUsed);
            }
            return new SuccessResult(Messages.EmailIsNotUsed);
        }
    }
}
