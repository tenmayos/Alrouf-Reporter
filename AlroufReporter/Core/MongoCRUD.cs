using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace DJPaissa.HelperMethods
{
    public static class MongoCRUD
    {
        private static MongoClient db = new MongoClient(new MongoClientSettings { ConnectTimeout = TimeSpan.FromSeconds(10) });

        /// <summary>
        /// Loads all the records found in the specified database (applicantsDB) and returns a list of type <T>
        /// </summary>
        /// <typeparam name="T">Datatype to extract the data into</typeparam>
        /// <param name="table">Name of the database collection</param>
        /// <returns></returns>
        public static List<T> LoadAllRecords<T>(string table = "applicants")
        {
            try
            {
                var collection = db.GetDatabase("applicantsDB").GetCollection<T>(table).FindSync(val => true);
                return collection.ToList();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("عفوا لم يتم العثور على قاعدة البيانات");
                return new List<T>();
            }
            
        }

    }
}