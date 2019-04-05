using System.Linq;
using repack.Entities;
using repack.Models;
using Xunit;

namespace repack.Test.Models
{
    public class UserModelTest
    {
        private readonly UserModel _userModel;

        public UserModelTest()
        {
            var testDb = new TestDb();
            var appSetting = new AppSetting(){ Salt = Environment.RepackSalt };
            _userModel = new UserModel(testDb.Db, appSetting);
        }

        [Fact]
        public async void GetListTest()
        {
            var users = await _userModel.GetList();
            Assert.NotEmpty(users);
        }
        
        [Fact]
        public async void GetTest()
        {
            var users = await _userModel.GetList();
            var user = await _userModel.Get(users.First().Id);
            Assert.NotNull(user);
        }
        
        [Fact]
        public async void CheckUserNameTest()
        {
            var users = await _userModel.GetList();
            var result = await _userModel.CheckUserName(users.First());
            Assert.True(result);
            
            result = await _userModel.CheckUserName(new User{ Name = users.First().Name });
            Assert.False(result);
        }
    }
}