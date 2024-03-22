using AutoMapper;
using QuizApi.Models;
using QuizData;


namespace QuizApi.Helpers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserData>();
            CreateMap<UserData, User>();

            CreateMap<UserData, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Username));

            CreateMap<User, UserLoginData>();
            CreateMap<UserLoginData, User>();
            
            CreateMap<UserRoleData, UserRoleSummary>();
            CreateMap<UserRoleSummary, UserRoleData>();
            
            CreateMap<UserRightData, UserRightSummary>();
            CreateMap<UserRightSummary, UserRightData>();
            
            CreateMap<RoleRightData, RoleRightSummary>();
            CreateMap<RoleRightSummary, RoleRightData>();

            CreateMap<Models.QuizData, Quiz>();
            CreateMap<Quiz, Models.QuizData>();

            CreateMap<QuizThemeData, QuizThemeSummary>();
            CreateMap<QuizThemeSummary, QuizThemeData>();
            
            CreateMap<QuestionTypeData, QuestionTypeSummary>();
            CreateMap<QuestionTypeSummary, QuestionTypeData>();
            
            CreateMap<AnswerTypeData, AnswerTypeSummary>();
            CreateMap<AnswerTypeSummary, AnswerTypeData>();
            
            CreateMap<QuestionData, QuestionSummary>();
            CreateMap<QuestionSummary, QuestionData>();
            
            CreateMap<AnswerData, AnswerSummary>();
            CreateMap<AnswerSummary, AnswerData>();

            CreateMap<ExamTypeData, ExamType>();
            CreateMap<ExamType, ExamTypeData>();
        }
    }
}