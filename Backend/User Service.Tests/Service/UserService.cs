using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using User_Service.Database;
using User_Service.Helpers;
using User_Service.Message;
using User_Service.Service;

namespace User_Service.Tests.Service
{
    [TestClass]
    public class UserService
    {
        private readonly IUserService _userService;

        public UserService()
        {
            var userRepositoryMock = new Mock<IUserRepository>();


            var userUpdateSenderMock = new Mock<IUserUpdateSender>();


            var appSettingsMock = new Mock<IOptions<AppSettings>>();
            appSettingsMock.Setup(x => x.Value).Returns(new Mock<AppSettings>().Object);

            _userService = new User_Service.Service.UserService(userRepositoryMock.Object, userUpdateSenderMock.Object, appSettingsMock.Object);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var users = _userService.GetAll().ToList();
            Assert.AreEqual(users.Count, 0);
        }

    }
}
