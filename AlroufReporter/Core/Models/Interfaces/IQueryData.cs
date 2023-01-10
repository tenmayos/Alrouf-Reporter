using System.Collections.Generic;

namespace AlroufReporter.Core.Models.Interfaces
{
    public interface IQueryData
    {
        public SortingTypes Criteria { get; }
        public List<CandidateModel> AllCandidates { get; }
        public List<string> CandidatesNames { get; }
        public Dictionary<string, string> CandidatesPhones { get; }
        public Dictionary<string, string> CandidatesEmails { get; }
        public Dictionary<string, string> CandidatesGpa { get; }
        public Dictionary<string, string> CandidatesUniversity { get; }
        public Dictionary<string, bool> CandidatesVolunteerActivities { get; }
        public bool ShouldAbort { get; }
    }
}
