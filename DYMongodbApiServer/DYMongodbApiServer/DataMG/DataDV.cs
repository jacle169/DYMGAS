using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DYMongodbApiServer
{
    public class DataDV
    {
        static DataDV dv;
        public static DataDV GetDV()
        {
            if (dv == null)
            {
                dv = new DataDV();
            }
            return dv;
        }

        public MongoDatabase getCol()
        {
            var connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            //server.DropDatabase("Cooldb");
            return server.GetDatabase("ApiDB");
        }
    }
}