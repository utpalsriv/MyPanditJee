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


        private readonly IMongoCollection<PanditProfileModel> _panditProfileModel;

        public PanditJeeProfileServices(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);
            _panditProfileModel = database.GetCollection<PanditProfileModel>(UserprofileCollectionNamespace);
        }

        public PanditProfileModel createPanditProfile(PanditProfileModel panditProfileModel)
        {
            try
            {
                _panditProfileModel.InsertOne(panditProfileModel);
                return panditProfileModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in CreatePanditProfile" + ex.Message);
            }
        }

        public PanditProfileModel GetPandit(string email)
        {
            try
            {
                var filter = Builders<PanditProfileModel>.Filter.Eq(x => x.Email, email);
                var user = _panditProfileModel.Find(filter).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetPandit" + ex.Message);
            }
        }
    }
}
