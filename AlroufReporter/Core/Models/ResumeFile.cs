namespace AlroufReporter.Core.Models
{
    public class ResumeFile
    {
        public string fieldname { get; set; }
        public string originalname { get; set; }
        public string encoding { get; set; }
        public string mimetype { get; set; }
        public byte[] buffer { get; set; }
        public int size { get; set; }
    }
}
