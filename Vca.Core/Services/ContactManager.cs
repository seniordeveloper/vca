using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vca.Abstractions.Services;
using Vca.Data;
using Vca.Data.Entities;
using Vca.Models;
using Vca.Shared;

namespace Vca.Core.Services
{
    /// <summary>
    /// A default implementation of <see cref="IContactManager"/>.
    /// </summary>
    public class ContactManager : IContactManager
    {
        private readonly IMapper _mapper;
        private readonly VcaDbContext _dbContext;
        private readonly IErrorDescriber _errorDescriber;

        public ContactManager(
            IMapper mapper,
            VcaDbContext dbContext,
            IErrorDescriber errorDescriber)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _errorDescriber = errorDescriber;
        }

        public async Task<AppResult<IEnumerable<UserContactModel>>> GetContactsAsync(long userId, CancellationToken cancellationToken)
        {
            var entities = await _dbContext.UserContacts
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return AppResult<IEnumerable<UserContactModel>>.Success(_mapper.Map<IEnumerable<UserContactModel>>(entities));
        }

        public async Task<AppResult<UserContactModel>> CreateContactAsync(long userId, UserContactModel contactModel, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UserContactEntity>(contactModel);
            entity.UserId = userId;
            entity.CreatedAt = TimeHelper.ServerTimeNow;

            await _dbContext.UserContacts.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            contactModel.CreatedAt = entity.CreatedAt;
            contactModel.Id = entity.Id;

            return AppResult<UserContactModel>.Success(contactModel);
        }

        public async Task<AppResult> DeleteContactAsync(long userId, long contactId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.UserContacts
                .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == contactId, cancellationToken);

            if (entity is not null)
            {
                _dbContext.UserContacts.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return AppResult.Success();
            }
            return AppResult.Failed(_errorDescriber.ContactNotFound(contactId));
        }

        public async Task<AppResult<UserContactModel>> FindContactAsync(long userId, long contactId, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.UserContacts
               .AsNoTracking()
               .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == contactId, cancellationToken);

            if (entity is not null)
            {
                return AppResult<UserContactModel>.Success(_mapper.Map<UserContactModel>(entity));
            }
            return AppResult<UserContactModel>.Failed(_errorDescriber.ContactNotFound(contactId));
        }

        public async Task<AppResult<UserContactModel>> UpdateContactAsync(long userId, UserContactModel contactModel, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.UserContacts
             .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == contactModel.Id, cancellationToken);

            if (entity is null) 
            {
                return AppResult<UserContactModel>.Failed(_errorDescriber.ContactNotFound(contactModel.Id));
            }

            _mapper.Map(contactModel, entity);

            await _dbContext.SaveChangesAsync();

            _mapper.Map(entity, contactModel);

            return AppResult<UserContactModel>.Success(contactModel);
        }
    }
}
