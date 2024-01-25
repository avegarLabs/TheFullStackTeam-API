using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Users.Results
{
    public class PaymentMethodResult : AppResult<PaymentMethodListItem>
    {
        public PaymentMethodResult(PaymentMethodListItem model) : base(model) { }
    }
}
