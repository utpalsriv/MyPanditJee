using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MyPanditJee.Models;
using MyPanditJee.Service.Interface;


namespace MyPanditJee.Service
{
    public class DropDownService : IDropDownService
    {
  
        private const string CountryCollectionNamespace = "Country";
        private const string StateCollectionNamespace = "States";
      
        private const string CityCollectionNamespace = "Cities";

       
        private readonly IMongoCollection<Country> _countryModel;
        private readonly IMongoCollection<States> _stateModel;
        private readonly IMongoCollection<Cities> _cityModel;



        public DropDownService(IDataConnection dataConnection)
        {
            var client = new MongoClient(dataConnection.ConnectionString);
            var database = client.GetDatabase(dataConnection.DatabaseName);    
            
            _countryModel = database.GetCollection<Country>(CountryCollectionNamespace);
            _stateModel = database.GetCollection<States>(StateCollectionNamespace);
            _cityModel = database.GetCollection<Cities>(CityCollectionNamespace);

        }

       

        public List<string> GetCountryList()
        {

            try
            {
                var builder = new FilterDefinitionBuilder<Country>();
                var filter = builder.Empty;
                var list = _countryModel.Find(filter).ToList();
                List<string> countryrlist = new List<string>();
                foreach (var s in list)
                {
                    countryrlist.Add(s.CountryName);
                }
                return countryrlist;

            }

            catch (Exception ex)
            {
                throw new Exception("Error in getAllUser" + ex.Message);

            }
        }

        public List<string> GetStateList()
        {
            try
            {
                var builder = new FilterDefinitionBuilder<States>();
                var filter = builder.Empty;
                var list = _stateModel.Find(filter).ToList();
                List<string> statelist = new List<string>();
                foreach (var s in list)
                {
                    statelist.Add(s.StateName);
                }
                return statelist;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in getAllUser" + ex.Message);

            }
        }

        

        public List<string> GetCityList()
        {
            try
            {
                var builder = new FilterDefinitionBuilder<Cities>();
                var filter = builder.Empty;
                var list = _cityModel.Find(filter).ToList();
                List<string> citylist = new List<string>();
                foreach (var s in list)
                {
                    citylist.Add(s.CityName);
                }
                return citylist;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in getAllUser" + ex.Message);

            }
        }
    }
}