using ConsoleApp1.Classes;
using ConsoleApp1.Models;
using ConsoleApp1.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Moq;

namespace TestProject1
{
    [TestFixture]
    public class ControllerTests
    {
        private Bykov2307a2TechContext _context;
        private DbInteraction _dbInteraction;
        private Mock<IUserInteraction> _userInputMock;
        private Mock<IOtherProgramInteraction> _emailSenderMock;
        private Controller _controller;

        [SetUp]
        public void SetUp()
        {
            // Èñïîëüçóåì óíèêàëüíîå èìÿ ÁÄ äëÿ êàæäîãî òåñòà
            var options = new DbContextOptionsBuilder<Bykov2307a2TechContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new Bykov2307a2TechContext(options);
            _dbInteraction = new DbInteraction(_context);
            _userInputMock = new Mock<IUserInteraction>();
            _emailSenderMock = new Mock<IOtherProgramInteraction>();
            _controller = new Controller(_dbInteraction, _userInputMock.Object, _emailSenderMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [TestCase("qwertyu", "Éôûâàûâô1!", "Éôûâàûâô1!")]
        public void RegistrationUser_ValidData(string? login, string? pass, string? passR)
        {
            // Arrange
            var inputData = new List<string?> { login, pass, passR };
            _userInputMock.Setup(x => x.GetRegData()).Returns(inputData);

            // Act
            bool result = _controller.RegistrationUser();

            // Assert
            Assert.IsTrue(result);
            var savedUser = _context.Registrations.FirstOrDefault(r => r.RLogin == login);
            Assert.IsNotNull(savedUser);
            Assert.IsTrue(savedUser.RResult);
            Assert.IsNull(savedUser.RErrorMessage);
        }

        [TestCase("qwertyu", "Éôûâàûâô1!", "Éôûâàûâô1!", true, null)]
        public void RegistrationUser_ExistingLogin(string? login, string? pass, string? passR, bool rst, string? errorM)
        {
            // Arrange
            var existingReg = new Registration
            {
                RLogin = login,
                RPassword = PasswordHasher.MaskPassword(pass),
                RRepeatPassword = PasswordHasher.MaskPassword(passR),
                RResult = rst,
                RErrorMessage = errorM
            };
            _context.Registrations.Add(existingReg);
            _context.SaveChanges();

            var inputData = new List<string?> { login, pass, passR };
            _userInputMock.Setup(x => x.GetRegData()).Returns(inputData);

            // Act
            bool result = _controller.RegistrationUser();

            // Assert
            Assert.IsTrue(result);
            Assert.That(_context.Registrations.Count(), Is.EqualTo(1));
        }

        [TestCase("shor", "123", "123", "Ëîãèí äîëæåí ñîäåðæàòü ìèíèìóì 5 ñèìâîëîâ.")]
        public void RegistrationUser_InvalidData(string? login, string? pass, string? passR, string errorM)
        {
            // Arrange
            var inputData = new List<string?> { login, pass, passR };
            _userInputMock.Setup(x => x.GetRegData()).Returns(inputData);

            // Act
            bool result = _controller.RegistrationUser();

            // Assert
            Assert.IsFalse(result);
            var savedUser = _context.Registrations.FirstOrDefault(r => r.RLogin == login);
            Assert.IsNotNull(savedUser);
            Assert.IsFalse(savedUser.RResult);
            Assert.That(savedUser.RErrorMessage, Is.EqualTo(errorM));
        }
        public class DbTests
        {
            private Bykov2307a2TechContext _context;
            private DbInteraction _dbInteraction;

            [SetUp]
            public void SetUp()
            {
                var options = new DbContextOptionsBuilder<Bykov2307a2TechContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
                _context = new Bykov2307a2TechContext(options);
                _dbInteraction = new DbInteraction(_context);
            }

            [TearDown]
            public void TearDown()
            {
                _context.Dispose();
            }

            [Test]
            public void AddRegInfo()
            {
                // Arrange
                var registration = new Registration
                {
                    RLogin = "testuser",
                    RPassword = PasswordHasher.MaskPassword("pass"),
                    RRepeatPassword = PasswordHasher.MaskPassword("pass"),
                    RResult = true,
                    RErrorMessage = null
                };

                // Act
                _dbInteraction.AddNewRegInfo(registration);
                var retrieved = _context.Registrations.FirstOrDefault(x => x.Equals(registration));

                // Assert
                Assert.IsNotNull(retrieved);
                Assert.That(retrieved.RLogin, Is.EqualTo("testuser"));
                Assert.That(retrieved.RPassword, Is.EqualTo(PasswordHasher.MaskPassword("pass")));
            }

            [Test]
            public void GetRegInfo()
            {
                // Arrange
                var registration = new Registration
                {
                    RLogin = "testuser",
                    RPassword = PasswordHasher.MaskPassword("pass"),
                    RRepeatPassword = PasswordHasher.MaskPassword("pass"),
                    RResult = true,
                    RErrorMessage = null
                };

                // Act
                _context.Registrations.Add(registration);
                _context.SaveChanges();
                var retrieved = _dbInteraction.GetRegInfo("testuser", "pass", "pass");

                // Assert
                Assert.IsNotNull(retrieved);
                Assert.That(retrieved.RLogin, Is.EqualTo("testuser"));
                Assert.That(retrieved.RPassword, Is.EqualTo(PasswordHasher.MaskPassword("pass")));
            }

            [Test]
            public void DeleteRegInfo()
            {
                // Arrange
                var registration = new Registration
                {
                    RLogin = "testuser",
                    RPassword = PasswordHasher.MaskPassword("pass"),
                    RRepeatPassword = PasswordHasher.MaskPassword("pass"),
                    RResult = true,
                    RErrorMessage = null
                };

                // Act
                _context.Registrations.Add(registration);
                _context.SaveChanges();
                _dbInteraction.DeleteRegInfo(registration);
                var retrieved = _context.Registrations.FirstOrDefault(x => x.Equals(registration));

                // Assert
                Assert.IsNull(retrieved);
            }
        }
    }
}
