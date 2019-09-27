
namespace XUnitTestCases.adminTestCases
{
    using BusinessLayer.Service;
    using CommonLayer.Models;
    using Moq;
    using RepositoryLayer.Interface;
    using Xunit;
    /// <summary>
    /// class for Registration testcase
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Method for registration test
        /// </summary>
        [Fact]
        public void AdminRegistrationTest()
        {
            ////we have to mock our service using Moq
            var adminData = new Mock<IAccountManagerRepository>();
            var admin = new AccountManager(adminData.Object);
            var userRegistration = new UserRegistration()
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };

            var result = admin.RegisterUser(userRegistration);
            Assert.NotNull(result);
        }

        /// <summary>
        /// method for login test
        /// </summary>
        [Fact]
        public void LoginTest()
        {
            var loginData = new Mock<IAccountManagerRepository>();
            var login = new AccountManager(loginData.Object);
            var loginModel = new LoginModel()
            {
                UserName = "UserName",
                Password = "Password"
            };

            var result = login.Login(loginModel);
            Assert.NotNull(result);
        }
    }
}
