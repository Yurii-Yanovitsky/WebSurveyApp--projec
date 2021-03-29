using System.Linq;
using WebLogic;
using WebSurveyApp.Models;

namespace WebSurveyApp
{
    public static class ViewExtension
    {
        public static UserBindingModel ToViewModel(this User model)
        {
            var userModel = new UserBindingModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                Surveys = model.Surveys.Select(s => s.ToViewModel()).AsParallel().ToList()
            };

            return userModel;
        }

        public static SurveyBindingModel ToViewModel(this Survey model)
        {
            var surveyModel = new SurveyBindingModel
            {
                Id = model.Id,
                Title = model.Title,
                Modified = model.Modified,
                Questions = model.Questions?.Select(q => q.ToViewModel()).AsParallel().ToList(),
                Reports = model.Reports?.Select(r => r.ToViewModel()).AsParallel().ToList(),
                UserId = model.UserId

            };

            return surveyModel;
        }

        public static QuestionBindingModel ToViewModel(this Question model)
        {
            var questionModel = new QuestionBindingModel
            {
                Id = model.Id,
                Text = model.Text,
                Options = model.Options.Select(op => op.ToViewModel()).ToList(),
                SurveyId = model.SurveyId
            };

            return questionModel;
        }

        public static OptionBindingModel ToViewModel(this Option model)
        {
            var optionModel = new OptionBindingModel
            {
                Id = model.Id,
                Text = model.Text,
                QuestionId = model.QuestionId,
            };

            return optionModel;
        }

        public static ResponseBindingModel ToViewModel(this Response model)
        {
            var responseModel = new ResponseBindingModel
            {
                Id = model.Id,
                Option = model.Option?.ToViewModel(),
                OptionId = model.OptionId,
                Question = model.Question?.ToViewModel(),
                QuestionId = model.QuestionId,
                ReportId = model.ReportId
            };

            return responseModel;
        }

        public static ReportBindingModel ToViewModel(this Report model)
        {
            var reportModel = new ReportBindingModel
            {
                Id = model.Id,
                Created = model.Created,
                SurveyId = model.SurveyId,
                Responses = model.Responses.Select(r => r.ToViewModel()).ToList()
            };

            return reportModel;
        }

        public static LoginBindingModel ToServiceModel(this LoginModel model)
        {
            var logicModel = new LoginBindingModel
            {
                Email = model.Email,
                Password = model.Password
            };

            return logicModel;
        }

        public static RegisterBindingModel ToServiceModel(this RegisterModel model)
        {
            var registerModel = new RegisterBindingModel
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword
            };

            return registerModel;
        }

        public static ChangePasswordViewModel ToChangePasswordViewModel(this User model)
        {
            var changePasswordViewModel = new ChangePasswordViewModel
            {
                UserId = model.Id,
                OldPassword = model.PasswordHash
            };

            return changePasswordViewModel;
        }
    }
}
