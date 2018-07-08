using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MyDiary.MongoProvider.Connection
{
    public static class MongoDBConnection
    {

        public static MongoDatabase Get(string connectionString, string databaseName)
        {
           
            MongoClient client = new MongoClient(connectionString);
            MongoServer server = client.GetServer();
            MongoDatabase mdb = server.GetDatabase(databaseName);
            return mdb;

        }

    }
}
