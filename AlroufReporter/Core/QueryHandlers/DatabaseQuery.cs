using AlroufReporter.Core.Models;
using AlroufReporter.Core.Models.Interfaces;
using DJPaissa.HelperMethods;
using System.Collections.Generic;

namespace AlroufReporter.Core.QueryHandlers
{
    /// <summary>
    /// Prepares data for generating an excell report in the case of database selection.
    /// </summary>
    public class DatabaseQuery : IQueryData
    {
        public SortingTypes Criteria { get; }
        public List<CandidateModel> AllCandidates { get; }
        public List<string> CandidatesNames { get; }
        public Dictionary<string, string> CandidatesPhones { get; }
        public Dictionary<string, string> CandidatesEmails { get; }
        public Dictionary<string, string> CandidatesGpa { get; }
        public Dictionary<string, string> CandidatesUniversity { get; }
        public Dictionary<string, bool> CandidatesVolunteerActivities { get; }
        public bool ShouldAbort { get; set; }
        public DatabaseQuery(SortingTypes criteria)
        {
            Criteria = criteria;
            CandidatesNames = new List<string>();
            CandidatesPhones = new Dictionary<string, string>();
            CandidatesEmails = new Dictionary<string, string>();
            CandidatesGpa = new Dictionary<string, string>();
            CandidatesUniversity = new Dictionary<string, string>();
            CandidatesVolunteerActivities = new Dictionary<string, bool>();

            AllCandidates = MongoCRUD.LoadAllRecords<CandidateModel>();

            if (AllCandidates.Count == 0)
                ShouldAbort = true;

            FillAndSortNames();
            FillDictionaries();
        }

        public void FillAndSortNames()
        {
            foreach (var record in AllCandidates)
            {
                CandidatesNames.Add(record.fullName);
            }
            CandidatesNames.Sort((x, y) => string.Compare(x, y));
        }

        private void FillDictionaries()
        {
            foreach (var record in AllCandidates)
            {
                string name = record.fullName;
                CandidatesPhones.Add(name, record.phone);
                CandidatesEmails.Add(name, record.email);
                CandidatesGpa.Add(name, $"{record.gpa} out of {record.totalGpa}");
                CandidatesUniversity.Add(name, record.school);
                CandidatesVolunteerActivities.Add(name, record.doneVolunteerWork);
            }

        }
    }
}
