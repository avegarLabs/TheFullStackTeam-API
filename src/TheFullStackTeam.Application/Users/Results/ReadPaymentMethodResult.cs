using TheFullStackTeam.Application.Model.ListItem;

namespace TheFullStackTeam.Application.Users.Results
{
    public class ReadPaymentMethodResult : AppResult<IEnumerable<PaymentMethodListItem>>
    {
        public ReadPaymentMethodResult(IEnumerable<PaymentMethodListItem> paymentsMethods) : base(paymentsMethods)
        {
        }
    }
}
