using MongoDB.Driver;
using System.Collections.Generic;

namespace DJPaissa.HelperMethods
{
    public static class MongoCRUD
    {
        private static IMongoDatabase db = new MongoClient().GetDatabase("applicantsDB");

        /// <summary>
        /// Loads all the records found in the specified database (applicantsDB) and returns a list of type <T>
        /// </summary>
        /// <typeparam name="T">Datatype to extract the data into</typeparam>
        /// <param name="table">Name of the database collection</param>
        /// <returns></returns>
        public static List<T> LoadAllRecords<T>(string table = "applicants")
        {
            var collection = db.GetCollection<T>(table).FindSync(val => true);
            return collection.ToList();
        }

    }
}