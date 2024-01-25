namespace TheFullStackTeam.Communications.EmailTemplates
{
    public interface IEmailViewRenderer
    {
        Task<string> RenderViewAsync<TModel>(string viewName, TModel model);
    }
}