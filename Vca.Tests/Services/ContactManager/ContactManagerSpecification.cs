using AutoMapper;
using Moq;
using Vca.Abstractions.Services;
using Vca.AutoMapper;
using Vca.Core.Services;
using Vca.Data;
using Vca.Tests.FakeUtils;

namespace Vca.Tests.Services.ContactManager
{
    [TestClass]
    public class ContactManagerSpecification : BaseSpecification, IDisposable
    {
        protected VcaDbContext DbContext { get; private set; }
        protected IMapper Mapper { get; private set; }
        protected IErrorDescriber ErrorDescriber { get; private set; }
        protected IContactManager ContactManager { get; private set; }

        protected override void Given()
        {
            base.Given();

            DbContext = new VcaDbContext(DbHelper.GetDbContextOptions());
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            Mapper = configuration.CreateMapper();
            ErrorDescriber = new ErrorDescriber();

            ContactManager = new Core.Services.ContactManager(Mapper, DbContext, ErrorDescriber);

            RestoreUsers();
        }

        private void RestoreUsers() 
        {
            DbContext.UserContacts.RemoveRange(DbContext.UserContacts);
            DbContext.SaveChanges();

            DbContext.Users.RemoveRange(DbContext.Users);
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DbContext is null)
            {
                return;
            }

            if (!_disposed)
            {
                DbContext.Dispose();
            }
            _disposed = true;
        }

        private bool _disposed = false;
    }
}
