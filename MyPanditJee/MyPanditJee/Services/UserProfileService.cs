using MongoDB.Driver;
using MyPanditJee.Models;
using MyPanditJee.Service.Interface;
using System;



namespace MyPanditJee.Service
{

    public class UserProfileService : IUserProfileService
    {
        private const string UserprofileCollectionNamespace = "userProfile";
        

        private readonly IMongoCollection<UserProfileModel> _userProfileModel;
       

        public UserProfileService(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);   
            _userProfileModel = database.GetCollection<UserProfileModel>(UserprofileCollectionNamespace);
           
        }

        public UserProfileModel CreateUserProfile(UserProfileModel userProfileModel)
        {
            try
            {
                _userProfileModel.InsertOne(userProfileModel);
                return userProfileModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CreateUserProfile" + ex.Message);
            }
        }

        public UserProfileModel GetUser(string email)
        {
            try
            {
                var filter = Builders<UserProfileModel>.Filter.Eq(x => x.Email, email);
                var user = _userProfileModel.Find(filter).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUser" + ex.Message);
            }
        }

    }
}
