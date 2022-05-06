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
        private const string EmployerProfileCollectionNamespace = "panditProfile";

        private readonly IMongoCollection<PanditJeeRegistrationModel> _panditjeeRegistration;
        private readonly IMongoCollection<LoginModel> _login;
        private readonly IMongoCollection<PanditJeeProfileModel> _panditjeeProfile;



        public PanditJeeServices(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);

            _panditjeeRegistration = database.GetCollection<PanditJeeRegistrationModel>(CollectionNamespace);
            _login = database.GetCollection<LoginModel>(LoginCollectionNamespace);
            _panditjeeProfile = database.GetCollection<PanditJeeProfileModel>(EmployerProfileCollectionNamespace);
        }
        public PanditJeeRegistrationModel registerPandit(PanditJeeRegistrationModel panditRegistrationModel, LoginModel loginModel, PanditJeeProfileModel panditProfileModel)
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
                throw new Exception("Error in registerEmployer" + ex.Message);
            }
        }
        public PanditJeeRegistrationModel GetUser(string email)
        {
            try
            {
                 return _panditjeeRegistration.Find(reg => reg.Email == email).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetUser" + ex.Message);
            }
        }
    }
}
