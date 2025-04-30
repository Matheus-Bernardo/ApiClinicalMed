using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApplication1.DTOS.Login;
using WebApplication1.Enums;
using WebApplication1.Infrastructure.Data;
using WebApplication1.Services.LoginService;
using WebApplication1.Tests.Helpers;
using WebApplication1.Utils;


namespace WebApplication1.Tests.LoginServiceTest
{
    public class LoginServiceTest
    {
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly LoginService _loginService;

        public LoginServiceTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new AppDbContext(dbContextOptions);
            var findUser = new FindUser(dbContext);

            _passwordHasherMock = new Mock<IPasswordHasher>();
            _jwtServiceMock = new Mock<IJwtService>();

            _loginService = new LoginService(
                findUser,
                _passwordHasherMock.Object,
                _jwtServiceMock.Object
            );
        }

        [Fact]
        public async Task Login_WithInvalidEmail_ShouldThrowArgumentException()
        {
            var dto = new LoginDto { email = "x@email.com", password = "123", typeUser = TypeUser.patient };

            var act = async () => await _loginService.login(dto);

            (await Assert.ThrowsAsync<ArgumentException>(act)).Message
                .Should().Be("The Email x@email.com not found");
        }

        [Fact]
        public async Task Login_WithInvalidUserType_ShouldThrowArgumentException()
        {
            var dto = new LoginDto { email = "x@email.com", password = "123", typeUser = TypeUser.patient };
            var user = UserFactory.CreateValidDoctor(dto.email, "hash", TypeUser.doctor);

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            var findUser = new FindUser(dbContext);
            var loginService = new LoginService(findUser, _passwordHasherMock.Object, _jwtServiceMock.Object);

            var act = async () => await loginService.login(dto);

            (await Assert.ThrowsAsync<ArgumentException>(act)).Message
                .Should().Be("You are not a patient user");
        }

        [Fact]
        public async Task Login_WithInvalidPassword_ShouldThrowArgumentException()
        {
            var dto = new LoginDto { email = "x@email.com", password = "senha123", typeUser = TypeUser.patient };
            var user = UserFactory.CreateValidPatient(dto.email, "hash", TypeUser.patient);

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            _passwordHasherMock.Setup(h => h.VerifyHashedPassword("hash", dto.password)).Returns(false);

            var findUser = new FindUser(dbContext);
            var loginService = new LoginService(findUser, _passwordHasherMock.Object, _jwtServiceMock.Object);

            var act = async () => await loginService.login(dto);

            (await Assert.ThrowsAsync<ArgumentException>(act)).Message
                .Should().Be("Invalid password");
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturnToken()
        {
            var dto = new LoginDto { email = "x@email.com", password = "senha123", typeUser = TypeUser.patient };
            var user = UserFactory.CreateValidPatient(dto.email, "hash", TypeUser.patient);

            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new AppDbContext(dbContextOptions);
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            _passwordHasherMock.Setup(h => h.VerifyHashedPassword("hash", dto.password)).Returns(true);
            _jwtServiceMock.Setup(j => j.GenerateToken(user)).Returns("fake-token");

            var findUser = new FindUser(dbContext);
            var loginService = new LoginService(findUser, _passwordHasherMock.Object, _jwtServiceMock.Object);

            var result = await loginService.login(dto);

            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.Token.Should().Be("fake-token");
            result.FirstName.Should().Be(user.firstName);
            result.Role.Should().Be(dto.typeUser.ToString());
        }
    }
}