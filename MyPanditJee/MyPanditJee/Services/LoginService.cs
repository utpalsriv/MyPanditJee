using MongoDB.Driver;
using MyPanditJee.Common;
using MyPanditJee.Models;
using MyPanditJee.Service.Interface;
using System;


namespace MyPanditJee.Service
{
    public class LoginService : ILoginService
    {
        private const string CollectionNamespace = "login";
       

        private readonly IMongoCollection<LoginModel> _login;
       

        public LoginService(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);
            _login = database.GetCollection<LoginModel>(CollectionNamespace);
         
        }

        public LoginModel validateCredential(LoginModel loginModel)
        {
            try
            {
                var filter = Builders<LoginModel>.Filter.Eq(x => x.Email, loginModel.Email);
                var result = _login.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    if (CommonCode.base64Decode(result.Password) == loginModel.Password)
                        result.LoginStatus = true;
                    else
                        result.LoginStatus = false;
                }
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in validateCredential" + ex.Message);
            }
        }

        public LoginModel FindMember(string email)
        {
            try
            {
                var filter = Builders<LoginModel>.Filter.Eq(x => x.Email, email);
                var user = _login.Find(filter).FirstOrDefault();
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in findMember" + ex.Message);
            }
        }
    }
}
