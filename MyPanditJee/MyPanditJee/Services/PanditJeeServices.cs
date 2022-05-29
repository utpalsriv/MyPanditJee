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
    public class PanditJeeServices : IPanditJeeServices
    {

        private const string CollectionNamespace = "panditjeeRegistration";
        private const string LoginCollectionNamespace = "login";
        private const string panditProfileCollectionNamespace = "panditProfile";

        private readonly IMongoCollection<PanditRegistrationModel> _panditjeeRegistration;
        private readonly IMongoCollection<LoginModel> _login;
        private readonly IMongoCollection<PanditProfileModel> _panditjeeProfile;



        public PanditJeeServices(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);

            _panditjeeRegistration = database.GetCollection<PanditRegistrationModel>(CollectionNamespace);
            _login = database.GetCollection<LoginModel>(LoginCollectionNamespace);
            _panditjeeProfile = database.GetCollection<PanditProfileModel>(panditProfileCollectionNamespace);
        }
        public PanditRegistrationModel registerPandit(PanditRegistrationModel panditRegistrationModel, LoginModel loginModel, PanditProfileModel panditProfileModel)
        {
            try
            {
                //var userExist = _panditjeeRegistration.Find(reg => reg.Email == panditrRegistrationModel.Email &&
                //reg.Phone == employerRegistrationModel.Email &&
                //reg.RegistrationType == employerRegistrationModel.RegistrationType);

                //if (userExist.CountDocuments() == 0)
                //{
                    _login.InsertOne(loginModel);
                _panditjeeRegistration.InsertOne(panditRegistrationModel);
                _panditjeeProfile.InsertOne(panditProfileModel);
                //}
                return panditRegistrationModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in registerPandit" + ex.Message);
            }
        }
        public PanditRegistrationModel GetPandit(string email)
        {
            try
            {
                 return _panditjeeRegistration.Find(reg => reg.Email == email).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetPandit" + ex.Message);
            }
        }

        public IEnumerable<PanditRegistrationModel> GetAllPandit()
        {
            try
            {
                return _panditjeeRegistration.Find(reg => true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetPandit" + ex.Message);
            }
        }

        public void Update(PanditProfileModel panditProfileModel)
        {
            try
            {
                _panditjeeProfile.ReplaceOne(reg => reg.Email == panditProfileModel.Email, panditProfileModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Update" + ex.Message);
            }
        }

    }
}
