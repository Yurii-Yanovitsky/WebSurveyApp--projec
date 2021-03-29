namespace WebLogic
{
    public class Response
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public Question Question { get; set; }
        public int? OptionId { get; set; }
        public Option Option { get; set; }
        public int? ReportId { get; set; }
        public Report Report { get; set; }
    }
}
