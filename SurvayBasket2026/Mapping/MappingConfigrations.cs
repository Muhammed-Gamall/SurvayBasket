namespace SurvayBasket2026.Mapping
{
    public class MappingConfigrations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<QuestionRequest, Question>()
                  .Map(dest=>dest.Answers, src=>src.Answers.Select(answer => new Answer { Content = answer }));
        }
    }
}
