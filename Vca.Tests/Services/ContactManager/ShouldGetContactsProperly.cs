using Moq;
using Vca.Data.Entities;
using Vca.Data.Entities.Identity;
using Vca.Models;
using Vca.Tests.FakeUtils;

namespace Vca.Tests.Services.ContactManager
{
    [TestClass]
    public class ShouldGetContactsProperly : ContactManagerSpecification
    {
        private long _userId;
        private AppResult<IEnumerable<UserContactModel>> _appResult;
        private int contactsCount = 5;

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

            InitUserContacts(_userId);
        }

        protected override void When()
        {
            base.When();
            _appResult = ContactManager.GetContactsAsync(_userId, It.IsAny<CancellationToken>()).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void ShouldGetContacts() 
        {
            Assert.IsTrue(_appResult.Succeded);
            Assert.AreEqual(_appResult.Data.Count(), contactsCount);
        }

        private void InitUserContacts(long userId) 
        {
            DbContext.UserContacts
                .AddRange(Enumerable.Range(1, contactsCount)
                .Select(id => new UserContactEntity 
                { 
                    Id = id, 
                    CreatedAt = FakeValueHelper.RandomDate, 
                    Name = FakeValueHelper.NextString,
                    PhoneNumber = FakeValueHelper.NextString,
                    UserId = userId
                }));
            DbContext.SaveChanges();
        }
    }
}
