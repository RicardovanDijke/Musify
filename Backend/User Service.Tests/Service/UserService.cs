using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using User_Service.Database;
using User_Service.Entities;
using User_Service.Helpers;
using User_Service.Message;
using User_Service.Service;

namespace User_Service.Tests.Service
{
    [TestClass]
    public class UserService
    {
        private readonly IUserService _userService;

        Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        Mock<IUserUpdateSender> userUpdateSenderMock = new Mock<IUserUpdateSender>();

        public UserService()
        {


            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(x => x.Value).Returns(new Mock<AppSettings>().Object);

            _userService = new User_Service.Service.UserService(userRepositoryMock.Object, userUpdateSenderMock.Object, appSettingsMock.Object);
        }

        [TestMethod]
        public void TestAddUser()
        {
            var user1 = new User { UserId = 0L, DisplayName = "TestUser" };
            _userService.Add(user1);

            userRepositoryMock.Verify(x => x.Add(user1), Times.Once);
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            var user1 = new User { UserId = 0L, DisplayName = "TestUser" };

            _userService.Update(user1);

            userRepositoryMock.Verify(x => x.Update(user1), Times.Once);
            userUpdateSenderMock.Verify(x => x.SendUpdate(It.IsAny<string>(), It.IsAny<User>()), Times.Once);
        } 
        [TestMethod]
        public void TestDeleteUser()
        {
            var user1 = new User { UserId = 0L, DisplayName = "TestUser" };

            _userService.Delete(user1);

            userRepositoryMock.Verify(x => x.Delete(user1), Times.Once);
            userUpdateSenderMock.Verify(x => x.SendUpdate(It.IsAny<string>(), It.IsAny<User>()), Times.Once);
        }
    }
}
