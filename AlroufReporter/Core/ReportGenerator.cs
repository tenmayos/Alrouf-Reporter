using AlroufReporter.Core.Models.Interfaces;
using IronXL;
using System.Collections.Generic;
using System.Linq;

namespace AlroufReporter.Core
{
    public class ReportGenerator
    {
        private IQueryData Data;
        public string ReportPath { get; }
        private WorkBook XlsxWorkbook;
        private WorkSheet XlsxSheet;

        public ReportGenerator(string reportPath, IQueryData data)
        {
            ReportPath = reportPath;
            Data = data;
            XlsxWorkbook = WorkBook.Create(ExcelFileFormat.XLSX);
            XlsxWorkbook.Metadata.Author = "AlroufReporter";
            XlsxSheet = XlsxWorkbook.CreateWorkSheet("new_sheet");
            Generate();
        }

        public void Generate()
        {
            switch (Data.Criteria)
            {
                case Models.SortingTypes.Name:
                    GenerateFromList(Data.CandidatesNames);
                    break;
                case Models.SortingTypes.Email:
                    GenerateFromStrDictionary(Data.CandidatesEmails);
                    break;
                case Models.SortingTypes.Phone:
                    GenerateFromStrDictionary(Data.CandidatesPhones);
                    break;
                case Models.SortingTypes.University:
                    GenerateFromStrDictionary(Data.CandidatesUniversity);
                    break;
                case Models.SortingTypes.Gpa:
                    GenerateFromStrDictionary(Data.CandidatesGpa);
                    break;
                case Models.SortingTypes.VoluntaryWork:
                    GenerateFromStrDictionary(Data.CandidatesVolunteerActivities);
                    break;
                default:
                    throw new System.Exception("The sort criteria is invalid, please contact the Author of this program");
            }
            Save();
        }

        public void GenerateFromList(List<string> dataList)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                XlsxSheet[$"A{i + 1}"].Value = dataList[i];
            }
        }

        public void GenerateFromStrDictionary(Dictionary<string, string> dataDic)
        {
            // Used a foreach here for ease, instead of a forloop since i'm dealing with a hashmap and not a list.
            // Note: I'm aware that a foreach loop is slower in terms of performance, however this was done considering a few ms difference is acceptable.
            int count = 1;

            foreach (var piece in dataDic.OrderBy(key => key.Value))
            {
                XlsxSheet[$"A{count}"].Value = piece.Key;
                XlsxSheet[$"B{count}"].Value = piece.Value;
                count++;
            }
        }
        public void GenerateFromStrDictionary(Dictionary<string, bool> dataDic)
        {
            int count = 1;

            foreach (var piece in dataDic.OrderBy(key => key.Value))
            {
                XlsxSheet[$"A{count}"].Value = piece.Key;
                XlsxSheet[$"B{count}"].Value = piece.Value;
                count++;
            }
        }

        public void Save()
        {
            try
            {
                if (!Data.ShouldAbort)
                {
                    XlsxWorkbook.SaveAs($"{ReportPath}\\AlroufReport.xlsx");
                    System.Windows.MessageBox.Show($"تم حفظ التقرير في - {ReportPath}");
                }
            }
            catch (System.Exception)
            {
                // Catching a method this way is incorrect as this exception is not precisely of the correct type and we are only relying on the message for clarification
                // Which may not infact be correct nor accurate.
                System.Windows.MessageBox.Show("عفوا يجب اغلاق التقرير السابق اولا");
            }
        }
    }
}
