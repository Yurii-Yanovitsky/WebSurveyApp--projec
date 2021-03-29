namespace WebSurveyApp
{
    public class ResponseBindingModel
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public QuestionBindingModel Question { get; set; }
        public int? OptionId { get; set; }
        public OptionBindingModel Option { get; set; }
        public int? ReportId { get; set; }
        public ReportBindingModel Report { get; set; }
    }
}
