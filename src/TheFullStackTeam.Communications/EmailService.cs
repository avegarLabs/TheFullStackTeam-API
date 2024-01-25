//using System.Net.Mail;
//using System.Text;
//using TheFullStackTeam.Application.Model.EntityModel;
//using TheFullStackTeam.Application.Services.Abstract;

//namespace TheFullStackTeam.Application.Services
//{
//    public class EmailService : ISendEmailService
//    {
//        public async Task<bool> SendEmail(EmailModel email)
//        {
//            String userName = "alfredo@jgcarmona.com";
//            String password = "Lukas#Luna15";
//            MailMessage msg = new MailMessage(email.FromEmail, "vegaramirezalfredo@gmail.com");
//            msg.Subject = email.Subject;
//            StringBuilder sb = new StringBuilder();
//            sb.AppendLine("User: " + email.Name);
//            sb.AppendLine("Email:" + email.FromEmail);
//            sb.AppendLine(email.Body);
//            msg.Body = sb.ToString();
//            SmtpClient SmtpClient = new SmtpClient();
//            SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
//            SmtpClient.Host = "smtp.office365.com";
//            SmtpClient.Port = 587;
//            SmtpClient.EnableSsl = true;
//            SmtpClient.Send(msg);

//            return true;
//        }
//    }
//}
