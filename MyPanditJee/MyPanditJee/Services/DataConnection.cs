using MyPanditJee.Service.Interface;


namespace MyPanditJee.Service
{
    public class DataConnection : IDataConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
