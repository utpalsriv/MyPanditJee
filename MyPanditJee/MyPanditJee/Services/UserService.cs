using MongoDB.Driver;

using System.Collections.Generic;

using System;
using MyPanditJee.Service.Interface;
using MyPanditJee.Models;


namespace MyPanditJee.Service
{
    public class UserService : IUserService
    {
        private const string CollectionNamespace = "userRegistration";
        private const string LoginCollectionNamespace = "login";
        

        private readonly IMongoCollection<UserRegistrationModel> _userRegistration;

        private readonly IMongoCollection<LoginModel> _login;
     

        public UserService(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);

            _userRegistration = database.GetCollection<UserRegistrationModel>(CollectionNamespace);
            _login = database.GetCollection<LoginModel>(LoginCollectionNamespace);
          
        }

        public IList<UserRegistrationModel> GetAllUser()
        {
            try
            {
                return _userRegistration.Find(reg => true).ToList();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in GetAllUser" + ex.Message);
            }
        }

        public UserRegistrationModel GetUser(string email)
        {
            try
            {
                return _userRegistration.Find(reg => reg.Email == email).SingleOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in GetUser" + ex.Message);
            }
        }

        public UserRegistrationModel RegisterUser(UserRegistrationModel userRegistrationModel, LoginModel loginModel, UserProfileModel userProfileModel)
        {
            try
            {
                var userExist = _userRegistration.Find(reg => reg.Email == userRegistrationModel.Email &&
                reg.Phone == userRegistrationModel.Email &&
                reg.RegistrationType == userRegistrationModel.RegistrationType);

                if (userExist.CountDocuments() == 0)
                {
                    _login.InsertOne(loginModel);
                    _userRegistration.InsertOne(userRegistrationModel);
                 
                }
                return userRegistrationModel;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in RegisterUser" + ex.Message);
            }
        }

        public UserRegistrationModel SearchUser(LoginModel loginModel)
        {
            try
            {
                var filter = Builders<UserRegistrationModel>.Filter.Eq(x => x.Email, loginModel.Email);
                return _userRegistration.Find(filter).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in SearchUser" + ex.Message);
            }
        }

       

        public void Update(UserRegistrationModel userRegistrationModel, LoginModel loginModel)
        {
            try
            {
                _userRegistration.ReplaceOne(reg => reg.Email == userRegistrationModel.Email, userRegistrationModel);
                _login.ReplaceOne(reg => reg.Email == loginModel.Email, loginModel);
            }
            catch(Exception ex)
            {
                throw new Exception("Error in Update" + ex.Message);
            }
        }

        

        public void Delete(string id)
        {
            try
            {
                _userRegistration.DeleteOne(reg => reg.Id == id);
            }
            catch(Exception ex)
            {
                throw new Exception("Error in Delete" + ex.Message);
            }
        }

        public UserRegistrationModel RegisterUser(UserRegistrationModel userRegistrationModel, LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
