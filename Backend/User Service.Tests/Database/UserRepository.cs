using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using User_Service.Database;

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
    }
}