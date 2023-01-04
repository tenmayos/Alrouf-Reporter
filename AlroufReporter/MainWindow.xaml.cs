using AlroufReporter.Core;
using AlroufReporter.Core.Models;
using AlroufReporter.Core.QueryHandlers;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace AlroufReporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string[]? files;
        private static string? reportPath;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnChooseFolderClick(object sender, RoutedEventArgs e)
        {
            if(ShouldQueryFromDB.IsChecked.HasValue && ShouldQueryFromDB.IsChecked.Value)
            {
                System.Windows.MessageBox.Show("عفوا لا يمكن اختيار مجلد المرشحين في حالة اللجوء لقاعدة البيانات");
                return;
            }

            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            DialogResult result = openFolderDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var allFilesInFolder = Directory.GetFiles(openFolderDialog.SelectedPath);
                bool hasPdf = false;

                foreach (var file in allFilesInFolder)
                {
                    if (file.Contains(".pdf"))
                    {
                        hasPdf = true;
                    }
                }

                if (!hasPdf)
                {
                    System.Windows.MessageBox.Show("عفوا لايوجد ملفات من النوع المطلوب داخل هذا المجلد");
                }
                else
                {
                    files = Directory.GetFiles(openFolderDialog.SelectedPath);
                }
            }
        }

        private void OnChoosePathClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog chooseFolder = new FolderBrowserDialog();
            DialogResult result = chooseFolder.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = chooseFolder.SelectedPath;
            }
        }

        private void OnCreateReportClick(object sender, RoutedEventArgs e)
        {
            // Adding an initial value to get rid of the compiler warnings
            SortingTypes criteria = SortingTypes.Name;
            switch (SortCriteria.Text)
            {
                case "":
                    System.Windows.MessageBox.Show("عفوا يجب اختيار معيار للترتيب");
                    return;
                case "الاسماء":
                    criteria = SortingTypes.Name;
                        break;
                case "البريد الالكتروني":
                    criteria = SortingTypes.Email;
                    break;
                case "الهاتف":
                    criteria = SortingTypes.Phone;
                    break;
                case "الجامعة":
                    criteria = SortingTypes.University;
                    break;
                case "المعدل":
                    criteria = SortingTypes.Gpa;
                    break;
                case "الاعمال التطوعية":
                    criteria = SortingTypes.VoluntaryWork;
                    break;
                default:
                    System.Windows.MessageBox.Show("يبدو ان هناك خطاء ,كيف وصلت هنا؟؟؟؟");
                    break;
            }

            if (reportPath == null)
            {
                System.Windows.MessageBox.Show("عفوا يجب اختيار مكان حفظ التقرير");
                return;
            }

            if (ShouldQueryFromDB.IsChecked.HasValue && ShouldQueryFromDB.IsChecked.Value)
            {
                DatabaseQuery dbq = new DatabaseQuery(criteria);
                ReportGenerator gen = new ReportGenerator(reportPath, dbq);
                try
                {
                    gen.Generate();
                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                }
                
            }

            if (files != null)
            {
                if (criteria == SortingTypes.Name || criteria == SortingTypes.Email || criteria == SortingTypes.VoluntaryWork)
                {
                    ReportGenerator gen = new ReportGenerator(reportPath, new LocalQuery(criteria, files));
                    try
                    {
                        gen.Generate();
                    }
                    catch (Exception ex)
                    {

                        System.Windows.MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("عفوا للملفات المحلية يمكنك فقط استخدام الاسم او البريد او الاعمال التطوعية");
                    return;
                }

            }

            System.Windows.MessageBox.Show($"تم حفظ التقرير في - {reportPath}");
        }
    }
}
