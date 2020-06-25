using System;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using User_Service.Database;
using User_Service.Helpers;
using User_Service.Message;

namespace User_Service.Tests.Database
{

    [TestClass]
    public class UserRepository
    {
        private readonly User_Service.Database.UserRepository _userRepository;

        private readonly Mock<DatabaseContext> databaseContextMock = new Mock<DatabaseContext>();

        public UserRepository()
        {
            _userRepository = new User_Service.Database.UserRepository(databaseContextMock.Object);
        }


        [TestMethod]
        public void TestGetUser()
        {
            //var user = _userRepository.Get(0);

          //  databaseContextMock.Verify(x => x.Users.Find(0), Times.Once);
        }
    }
}