using MongoDB.Driver;
using MyPanditJee.Models;
using MyPanditJee.Service.Interface;
using MyPanditJee.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPanditJee.Services
{
    public class PanditJeeProfileServices : IPanditJeeProfileServices
    {
        private const string UserprofileCollectionNamespace = "panditProfile";


        private readonly IMongoCollection<PanditJeeRegistrationModel> _panditProfileModel;

        public PanditJeeProfileServices(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);
            _panditProfileModel = database.GetCollection<PanditJeeRegistrationModel>(UserprofileCollectionNamespace);
        }

        public PanditJeeRegistrationModel createEmployerProfile(PanditJeeRegistrationModel panditProfileModel)
        {
            try
            {
                _panditProfileModel.InsertOne(panditProfileModel);
                return panditProfileModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CreateUserProfile" + ex.Message);
            }
        }

        public PanditJeeRegistrationModel GetPandit(string email)
        {
            try
            {
                var filter = Builders<PanditJeeRegistrationModel>.Filter.Eq(x => x.Email, email);
                var user = _panditProfileModel.Find(filter).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUser" + ex.Message);
            }
        }
    }
}
