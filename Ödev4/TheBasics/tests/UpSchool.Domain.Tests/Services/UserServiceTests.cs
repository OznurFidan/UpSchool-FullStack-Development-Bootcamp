using FakeItEasy;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;

namespace UpSchool.Domain.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUser_ShouldGetUserWithCorrectId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var cancellationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = userId
            };

            A.CallTo(() => userRepositoryMock.GetByIdAsync(userId, cancellationSource.Token))
                .Returns(Task.FromResult(expectedUser));

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.GetByIdAsync(userId, cancellationSource.Token);

            Assert.Equal(expectedUser, user);
        }
        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            await Assert.ThrowsAsync<ArgumentException>(() => userService.AddAsync("Öznur", "Fidan", 22, null, cancellationSource.Token));
            await Assert.ThrowsAsync<ArgumentException>(() => userService.AddAsync("Öznur", "Fidan", 22, string.Empty, cancellationSource.Token));

        }


        [Fact]
        public async Task AddAsync_ShouldReturn_CorrectUserId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var expectedUser = new User()
            {
                Id = userId
            };

            A.CallTo(() => userRepositoryMock.AddAsync(expectedUser, cancellationSource.Token))
                .Returns(Task.FromResult(1));

            IUserService userService = new UserManager(userRepositoryMock);

            var userGuid = await userService.AddAsync("Serra", "Tug", 28, "serratug@example.com", cancellationSource.Token);


            Assert.IsType<Guid>(userGuid);
            Assert.NotEqual(Guid.Empty, userGuid);

        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            Guid id = Guid.NewGuid();

            IUserService userService = new UserManager(userRepositoryMock);

            var result = await userService.DeleteAsync(id, cancellationSource.Token);

            Assert.True(result);
        }
        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesntExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = Guid.Empty;

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            await Assert.ThrowsAsync<ArgumentException>(() => userService.DeleteAsync(userId, cancellationSource.Token));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            var user = new User { Id = Guid.Empty };

            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(user, cancellationSource.Token));

        }
        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);
            var userWhoseEmailIsNull = new User()
            {
                Id = new Guid(),
                Email= null,
            };
            var userWhoseEmailIsEmty = new User()
            {
                Id = new Guid(),

                Email = string.Empty,
            };

            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(userWhoseEmailIsNull, cancellationSource.Token));
            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(userWhoseEmailIsEmty, cancellationSource.Token));

        }
        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserIsNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);
            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(null, cancellationSource.Token));

        }
        [Fact]
        public async Task GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            List<User> userList = new List<User>()
            {
                new User(){ Id = Guid.NewGuid(), FirstName = "Öznur", LastName = "Fidan", Age = 22, Email = "oznur@email.com"},
                new User() { Id = Guid.NewGuid(), FirstName = "Edanur", LastName = "Fidan", Age = 22, Email = "edanur@email.com" }
            };

            A.CallTo(() => userRepositoryMock.GetAllAsync(cancellationSource.Token))
                .Returns(Task.FromResult(userList));

            IUserService userService = new UserManager(userRepositoryMock);

            var allUsersList = await userService.GetAllAsync(cancellationSource.Token);

            Assert.True(allUsersList.Count >= 2);
        }


    }
}
