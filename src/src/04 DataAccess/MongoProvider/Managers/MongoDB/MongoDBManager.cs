using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MyDiary.MongoProvider.Connection;
using MongoDB.Driver.Linq;

namespace MyDiary.MongoProvider.Managers
{
    public class MongoDBManager
    {
        #region PUBLIC METHODS

        /// <summary>
        /// To get all documents of the specified collection name.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The specified collection name.</param>
        /// <returns>IQueryable<T></returns>
        public IQueryable<T> GetAll<T>(string collectionName)
        {
            MongoCollection colection = this.GetMongoDBCollection<T>(collectionName);
            return colection.AsQueryable<T>();
        }

        /// <summary>
        /// To get all documents of the specified collection by mongoQuery.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The name of the collection. </param>
        /// <param name="query">The mongo query to execute.</param>
        /// <returns>Mongo Cursor </returns>
        public MongoCursor<T> Find<T>(string collectionName, MongoDB.Driver.IMongoQuery query)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);
            return collection.FindAs<T>(query);
        }

        /// <summary>
        /// Add an document to the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The specified collection name.</param>
        /// <param name="document">The document object to be added.</param>
        /// <returns>WriteConcernResult ,whether insert failed or not. </returns>
        public WriteConcernResult Add<T>(string collectionName,T document)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);           
           return collection.Insert<T>(document);

        }

        /// <summary>
        /// Add list of documents to the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The specified collection name.</param>
        /// <param name="document">The document list to be added.</param>
        /// <returns>IEnumerable WriteConcernResult ,whether insert failed or not for each document. </returns>
        public IEnumerable<WriteConcernResult> Add<T>(string collectionName, IList<T> documents)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);
            return collection.InsertBatch<T>(documents);

        }

        public FindAndModifyResult Update<T>(string collectionName, MongoDB.Driver.IMongoQuery query, IMongoUpdate updateStatements)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);
            return collection.FindAndModify(query, MongoDB.Driver.Builders.SortBy.Null, updateStatements);

        }
        /// <summary>
        /// Remove a document from the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The collection name.</param>
        /// <param name="query">MongoQuery to remove the document.</param>
        /// <returns>WriteConcernResult whether Remove failed or not.</returns>
        public WriteConcernResult Remove<T>(string collectionName, MongoDB.Driver.IMongoQuery query)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);
            return collection.Remove(query);

        }

        /// <summary>
        /// Remove all document from the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="collectionName">The collection name.</param>
        /// <param name="query">MongoQuery to remove documents.</param>
        /// <returns>WriteConcernResult whether Remove failed or not.</returns>
        public WriteConcernResult RemoveAll<T>(string collectionName)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);
            return collection.RemoveAll();

        }

        /// <summary>
        /// To get the count of documents.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="collectionName">The collection name.</param>
        /// <param name="query">The MongoQuery to get the count.</param>
        /// <returns>count as integer value.</returns>
        public int GetCountByQuery<T>(string collectionName, MongoDB.Driver.IMongoQuery query)
        {
            MongoCollection collection = this.GetMongoDBCollection<T>(collectionName);
            return int.Parse(collection.Count(query).ToString());
        }
        
        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// To get the collection by name.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="collectionName">The collection name.</param>
        /// <returns>A Mongo collection </returns>
        private MongoCollection GetMongoDBCollection<T>(string collectionName)
        {
            MongoDatabase mdb = this.GetMyDiaryMongoDbConnection("MyDIARY");
            return mdb.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// To get the mongo db connection 
        /// </summary>
        /// <param name="dataBaseName">The database name.</param>
        /// <returns>A Mongo Database</returns>
        private MongoDatabase GetMyDiaryMongoDbConnection(string dataBaseName)
        {
            return MongoDBConnection.Get("mongodb://localhost", dataBaseName);
        }

        
        #endregion
    }
}
