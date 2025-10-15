namespace News
{
    public class EmailClient : IObserver
    {
        private readonly string IndirizzoEmail;
        private string _mail;

        public EmailClient(string IndEmail)
        {
            IndirizzoEmail = IndEmail;
        }

        public void Update(string message)
        {
            _mail = message;
            Console.WriteLine($"{IndirizzoEmail}: Email sent: : {_mail}\n");
        }
    }
}