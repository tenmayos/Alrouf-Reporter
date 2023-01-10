using AlroufReporter.Core.Models;
using AlroufReporter.Core.Models.Interfaces;
using System.Collections.Generic;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace AlroufReporter.Core.QueryHandlers
{
    /// <summary>
    /// Prepares data for generating an excell report in the case of local folder selection.
    /// </summary>
    public class LocalQuery : IQueryData
    {
        public SortingTypes Criteria { get; }

        private string[] FilePaths;

        public List<CandidateModel> AllCandidates { get; }

        public List<string> CandidatesNames { get; }

        public Dictionary<string, string> CandidatesPhones { get; }

        public Dictionary<string, string> CandidatesEmails { get; }

        public Dictionary<string, string> CandidatesGpa { get; }

        public Dictionary<string, string> CandidatesUniversity { get; }

        public Dictionary<string, bool> CandidatesVolunteerActivities { get; }

        public bool ShouldAbort { get; }

        public LocalQuery(SortingTypes criteria, string[] filePaths)
        {
            Criteria = criteria;
            FilePaths = filePaths;
            AllCandidates = new List<CandidateModel>();
            CandidatesNames = new List<string>();
            CandidatesPhones = new Dictionary<string, string>();
            CandidatesEmails = new Dictionary<string, string>();
            CandidatesGpa = new Dictionary<string, string>();
            CandidatesUniversity = new Dictionary<string, string>();
            CandidatesVolunteerActivities = new Dictionary<string, bool>();
            PopulateAllCandidates();
            PopulateAllData();
        }

        private void PopulateAllCandidates()
        {
            foreach (var file in FilePaths)
            {
                var cand = new CandidateModel();
                bool foundEmail = false;
                cand.fullName = file.Substring(file.LastIndexOf("\\") + 1, file.LastIndexOf(".") - file.LastIndexOf("\\"));

                if (file.Contains("AlroufReport"))
                {
                    continue;
                }

                using (PdfDocument document = PdfDocument.Open(file))
                {
                    foreach (Page page in document.GetPages())
                    {
                        foreach (Word word in page.GetWords())
                        {
                            string lWord = word.Text.ToLower();
                            if (lWord.Contains("@") && !foundEmail)
                            {
                                foundEmail = true;
                                cand.email = word.Text;
                            }
                            else if (lWord.Contains("volunt"))
                            {
                                cand.doneVolunteerWork = true;
                            }
                        }
                    }
                }

                AllCandidates.Add(cand);
            }
            
        }
        private void PopulateAllData()
        {
            foreach (var cand in AllCandidates)
            {
                string name = cand.fullName;
                CandidatesNames.Add(name);
                CandidatesEmails.Add(name, cand.email);
                CandidatesVolunteerActivities.Add(name, cand.doneVolunteerWork);
            }
        }
    }
}
