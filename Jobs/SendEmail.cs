using AppoinmentScheduler.Data;
using Quartz;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AppoinmentScheduler.Jobs
{
    public class SendEmail: IJob
    {

        private readonly ApplicationDBContext _dbContext;
        public SendEmail(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public  Task Execute(IJobExecutionContext context)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from_email = new EmailAddress("riyazurrazakn.19ece@kongu.edu", "Appoinmnet Scheduler bot");
            var slotsToday = _dbContext.Slots.Where(slot => slot.SlotTime.Date.Equals(DateTime.Now.Date)).ToList();
            slotsToday.ForEach(slot =>
            {
                var subject = "Reminder!!";
                var to_email = new EmailAddress(slot.SlotUserBy.EmailAddress, slot.SlotUserBy.Name);
                var plainTextContent = "Reminder! You have an appoinment today";
                var htmlContent = $"<strongAppoinment Scheduled For You Today & {slot.SlotTime} </strong>";
                var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg).ConfigureAwait(false);

            });
         
            return Task.FromResult(true);
        }

    }
}
