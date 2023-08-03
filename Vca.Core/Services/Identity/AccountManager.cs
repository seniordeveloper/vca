using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Claims;
using Vca.Abstractions.Services;
using Vca.Abstractions.Services.Identity;
using Vca.Data;
using Vca.Data.Entities.Identity;
using Vca.Data.Extensions;
using Vca.Models;
using Vca.Models.Identity;

namespace Vca.Core.Services.Identity
{
    /// <summary>
    /// A default implementation of <see cref="IAccountManager"/>.
    /// </summary>
    public class AccountManager : IAccountManager
    {
        private readonly IPasswordHasher<UserEntity> _hasher;
        private readonly VcaDbContext _dbContext;
        private readonly IErrorDescriber _errorDescriber;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public AccountManager(
            IPasswordHasher<UserEntity> hasher,
            VcaDbContext dbContext,
            IErrorDescriber errorDescriber,
            IMapper mapper,
            UserManager<UserEntity> userManager)
        {
            _dbContext = dbContext;
            _hasher = hasher;
            _errorDescriber = errorDescriber;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppResult<UserModel>> SigninAsync(SigninModel signinModel, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email == signinModel.Login, cancellationToken);
            if (user is null)
            {
                return AppResult<UserModel>.Failed(_errorDescriber.UserNameNotFound(signinModel.Login));
            }

            if (_hasher.VerifyHashedPassword(user, user.PasswordHash, signinModel.Password) != PasswordVerificationResult.Success)
            {
                return AppResult<UserModel>.Failed(_errorDescriber.UserNameNotFound(signinModel.Login));
            }

            return AppResult<UserModel>.Success(_mapper.Map<UserModel>(user));
        }

        public async Task<AppResult> CreateUserAsync(SignupModel signupModel, CancellationToken cancellationToken) 
        {
            var existingUser = await _dbContext.Users.FindByEmailAsync(signupModel.Email, cancellationToken);

            if (existingUser is not null) 
            {
                return AppResult.Failed(_errorDescriber.EmailAlreadyInUse(signupModel.Email));
            }

            var entity = new UserEntity
            {
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                UserName = signupModel.Email,
                Email = signupModel.Email,
                LockoutEnabled = false,
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName
            };

            var userResult = await _userManager.CreateAsync(entity, signupModel.Password);

            if (!userResult.Succeeded)
            {
                return AppResult.Failed(userResult.Errors.Select(w => w.Description));
            }

            entity.LockoutEnabled = false;
            entity.LockoutEnd = null;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return AppResult.Success();
        }
    }
}
