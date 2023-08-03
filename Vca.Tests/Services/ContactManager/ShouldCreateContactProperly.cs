using Moq;
using Vca.Data.Entities.Identity;
using Vca.Models;
using Vca.Tests.FakeUtils;

namespace Vca.Tests.Services.ContactManager
{
    [TestClass]
    public class ShouldCreateContactProperly : ContactManagerSpecification
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
                 CreatedAt = FakeValueHelper.RandomDate,
                 Name = FakeValueHelper.NextString,
                 PhoneNumber = FakeValueHelper.RandomPhoneNumber
            };
        }

        protected override void When()
        {
            base.When();
            _appResult = ContactManager.CreateContactAsync(_userId, _expectedContact, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void ShouldCreateContact() 
        {
            Assert.IsTrue(_appResult.Succeded);
            var createdContact = _appResult.Data;
            Assert.AreEqual(createdContact.Name, _expectedContact.Name);
            Assert.AreEqual(createdContact.PhoneNumber, _expectedContact.PhoneNumber);
        }
    }
}
