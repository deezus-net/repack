using System;
using System.Linq;
using Npgsql;
using repack.Entities;
using repack.Models;
using Xunit;

namespace repack.Test.ModelsTest
{
    public class UserModelTest : BaseTest
    {
        
        private readonly UserModel _userModel;
        public UserModelTest()
        {
            _userModel = new UserModel(Db, AppSetting);
        }

        [Fact]
        public async void Test()
        {
            var name = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();

            var dummyUser = new User
            {
                Name = name,
                Password = password
            };
            // create user
            Assert.True(await _userModel.Update(dummyUser));
            
            // list
            var users = await _userModel.GetList();
            Assert.NotEmpty(users);
            dummyUser = users.First(u => u.Name == name);
            
            // update user
            dummyUser.Name = Guid.NewGuid().ToString();
            Assert.True(await _userModel.Update(dummyUser));
            
            // check user name
            Assert.True(await _userModel.CheckUserName(new User{ Name = name }));
            Assert.True(await _userModel.CheckUserName(dummyUser));
            
            // admin name
            Assert.False(await _userModel.CheckUserName(new User{ Name = TestSettings.RepackAdminId }));
            
            // get
            Assert.NotNull(await _userModel.Get(dummyUser.Id));
            
            // login
            Assert.NotNull(_userModel.Login(dummyUser.Name, dummyUser.Password));
            
            // admin login
            Assert.NotNull(_userModel.Login(TestSettings.RepackAdminId, TestSettings.RepackAdminPassword));

            
            // delete
            Assert.True(await _userModel.Delete(dummyUser.Id));
        }


    }
}