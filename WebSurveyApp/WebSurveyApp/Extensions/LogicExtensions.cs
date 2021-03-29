using System.Linq;
using WebLogic;

namespace WebSurveyApp
{
    public static class LogicExtension
    {

        public static User ToServiceModel(this UserBindingModel model)
        {
            var userModel = new User
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                Surveys = model.Surveys?.Select(s => s.ToServiceModel()).ToList()
            };

            return userModel;
        }

        public static Survey ToServiceModel(this SurveyBindingModel model)
        {
            var surveyModel = new Survey
            {
                Id = model.Id,
                Title = model.Title,
                Modified = model.Modified,
                Questions = model.Questions?.Select(q => q.ToServiceModel()).ToList(),
                Reports = model.Reports?.Select(r => r.ToServiceModel()).ToList(),
                UserId = model.UserId

            };

            return surveyModel;
        }

        public static Question ToServiceModel(this QuestionBindingModel model)
        {
            var questionModel = new Question
            {
                Id = model.Id,
                Text = model.Text,
                Options = model.Options?.Select(op => op.ToServiceModel()).ToList(),
                SurveyId = model.SurveyId
            };

            return questionModel;
        }

        public static Option ToServiceModel(this OptionBindingModel model)
        {
            var optionModel = new Option
            {
                Id = model.Id,
                Text = model.Text,
                QuestionId = model.QuestionId,
            };

            return optionModel;
        }

        public static Response ToServiceModel(this ResponseBindingModel model)
        {
            var responseModel = new Response
            {
                Id = model.Id,
                OptionId = model.OptionId,
                QuestionId = model.QuestionId,
                ReportId = model.ReportId
            };

            return responseModel;
        }

        public static Report ToServiceModel(this ReportBindingModel model)
        {
            var reportModel = new Report
            {
                Id = model.Id,
                Created = model.Created,
                SurveyId = model.SurveyId,
                Responses = model.Responses?.Select(r => r.ToServiceModel()).ToList()
            };

            return reportModel;
        }

        public static LoginModel ToServiceModel(this LoginBindingModel model)
        {
            var logicModel = new LoginModel
            {
                Email = model.Email,
                Password = model.Password
            };

            return logicModel;
        }

        public static RegisterModel ToServiceModel(this RegisterBindingModel model)
        {
            var registerModel = new RegisterModel
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            return registerModel;
        }
    }

}
