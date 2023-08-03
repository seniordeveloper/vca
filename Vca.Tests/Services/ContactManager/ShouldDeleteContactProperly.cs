using Moq;
using Vca.Data.Entities.Identity;
using Vca.Models;
using Vca.Tests.FakeUtils;

namespace Vca.Tests.Services.ContactManager
{
    [TestClass]
    public class ShouldDeleteContactProperly : ContactManagerSpecification
    {
        private long _userId;
        private long _deletedContactId;
        private AppResult _appResult;

        protected override void Given()
        {
            base.Given();

            var user = new UserEntity
            {
                Email = FakeValueHelper.NextString,
                UserName = FakeValueHelper.NextString,
                FirstName = FakeValueHelper.NextString,
                LastName = FakeValueHelper.NextString,
                PasswordHash = FakeValueHelper.NextString,
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            _userId = user.Id;
            _deletedContactId = FakeValueHelper.NextInt;

            DbContext.UserContacts.Add(new Data.Entities.UserContactEntity 
            {
                Id = _deletedContactId,
                UserId = _userId,
                Name = FakeValueHelper.NextString,
                PhoneNumber = FakeValueHelper.NextString,
                CreatedAt = FakeValueHelper.RandomDate
            });
            DbContext.SaveChanges();
        }

        protected override void When()
        {
            base.When();
            _appResult = ContactManager.DeleteContactAsync(_userId, _deletedContactId, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void ShouldDeleteContact() 
        {
            Assert.IsTrue(_appResult.Succeded);
            Assert.IsFalse(DbContext.UserContacts.Any(x => x.UserId == _userId && x.Id == _deletedContactId));
        }
    }
}
