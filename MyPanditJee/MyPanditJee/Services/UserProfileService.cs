using MongoDB.Driver;
using MyPanditJee.Models;
using MyPanditJee.Service.Interface;
using System;



namespace MyPanditJee.Service
{

    public class UserProfileService : IUserProfileService
    {
        private const string UserprofileCollectionNamespace = "userProfile";

        private const string BookingCollectionNamespace = "bookPandit";


        private readonly IMongoCollection<UserProfileModel> _userProfileModel;
            private readonly IMongoCollection<BookPanditModel> _bookPanditModel;

        public UserProfileService(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);
            _userProfileModel = database.GetCollection<UserProfileModel>(UserprofileCollectionNamespace);
            _bookPanditModel = database.GetCollection<BookPanditModel>(BookingCollectionNamespace);
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
        public void Update (UserProfileModel userProfileModel)
        {
            try
            {
                _userProfileModel.ReplaceOne(reg => reg.Email == userProfileModel.Email, userProfileModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Update" + ex.Message);
            }
        }

        public void BookPandit(BookPanditModel bookPanditModel)
        {
            try
            {
                _bookPanditModel.InsertOne(bookPanditModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Delete" + ex.Message);
            }
        }

    }
}
