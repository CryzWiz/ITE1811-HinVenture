using HiN_Ventures.Controllers;
using HiN_Ventures.Models;
using HiN_Ventures.Models.AccountViewModels;
using HiN_Ventures.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiN_Ventures_UnitTests
{
    [TestClass]
    public class AccountControllerTest
    {
        
        [TestMethod]
        public async Task RegisterAddsRoleToUser()
        {
            // Arrange
            var controller = new AccountController(
                It.IsAny<UserManager<ApplicationUser>>(),
                It.IsAny<SignInManager<ApplicationUser>>(),
                It.IsAny<IEmailSender>(),
                It.IsAny<ILogger<AccountController>>());

            var register = new RegisterViewModel {
                Email = "testepost@hinventures.no",
                Password = "Passord123456",
                ConfirmPassword = "Passord123456",
                UserName = "Brukernavn",
                FirstName = "Fornavn",
                LastName = "Etternavn"
            };

            // Ikke helt sikker på hvordan dette skal gjøres i AccountController. BK

            // Act
        }
    }
}
