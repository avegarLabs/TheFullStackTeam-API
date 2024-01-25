namespace TheFullStackTeam.Application;

/// <summary>
/// App command result 
/// </summary>
/// <typeparam name="TModel"></typeparam>
public abstract class AppResult<TModel>
{
    public bool Success { get; set; }
    public TModel? Data { get; set; }

    protected AppResult()
    {
    }

    protected AppResult(TModel model)
    {
        Data = model;
        Success = true;
    }
}