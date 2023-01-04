using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AlroufReporter.Core.Models
{
    public class CandidateModel
    {
        // In a real world App, it is best to clear up the data and follow common code conventions, in this case properties should be PascalCase
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string school { get; set; }
        public string major { get; set; }
        public string gpa { get; set; }
        public string totalGpa { get; set; }
        public bool doneVolunteerWork { get; set; }
        public ResumeFile ResumeFile { get; set; }
        public int __v { get; set; }
    }
}
