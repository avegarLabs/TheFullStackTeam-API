namespace TheFullStackTeam.Application.Users.Results
{
    public class DelatePaymentMethodUserCommandResult : AppResult<bool>
    {
        public DelatePaymentMethodUserCommandResult(bool success) : base(success) { }
    }
}
