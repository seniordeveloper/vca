using Moq;
using Vca.Data.Entities.Identity;
using Vca.Models;
using Vca.Tests.FakeUtils;

namespace Vca.Tests.Services.ContactManager
{
    [TestClass]
    public class ShouldUpdateContactProperly : ContactManagerSpecification
    {
        private long _userId;
        private UserContactModel _expectedContact;
        private AppResult<UserContactModel> _appResult;

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

            _expectedContact = new UserContactModel
            {
                Id = FakeValueHelper.NextInt,
                CreatedAt = FakeValueHelper.RandomDate,
                Name = FakeValueHelper.NextString,
                PhoneNumber = FakeValueHelper.RandomPhoneNumber
            };

            DbContext.UserContacts.Add(new Data.Entities.UserContactEntity 
            {
                Id = _expectedContact.Id,
                CreatedAt = FakeValueHelper.RandomDate,
                Name = FakeValueHelper.NextString,
                PhoneNumber = FakeValueHelper.NextString,
                UserId = _userId
            });
            DbContext.SaveChanges();
        }

        protected override void When()
        {
            base.When();
            _appResult = ContactManager.UpdateContactAsync(_userId, _expectedContact, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void ShouldUpdateContact() 
        {
            Assert.IsTrue(_appResult.Succeded);
            var updatedContact = DbContext.UserContacts.Single(w => w.UserId == _userId && w.Id == _expectedContact.Id);
            Assert.IsNotNull(updatedContact);
            Assert.AreEqual(updatedContact.Name, _expectedContact.Name);
            Assert.AreEqual(updatedContact.PhoneNumber, _expectedContact.PhoneNumber);
        }
    }
}
