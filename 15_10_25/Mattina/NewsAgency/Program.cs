namespace News
{
    public class Program
    {
        static void Main(string[] args)
        {
            var agency = NewsAgency.GetIstanza();

            var mobileApp = new MobileApp("Gmail");
            var emailClient = new EmailClient("Rossi@gmail.com");

            agency.Attach(mobileApp);
            agency.Attach(emailClient);

            Console.WriteLine("Inserisci messaggio nell'email: ");
            var message = Console.ReadLine();
            if (message != null)
            {
                agency.Message = message;
            }
            Console.WriteLine("\nVerifica singleton: ");
            var agency2 = NewsAgency.GetIstanza();
            var MobileApp2 = new MobileApp("Yahoo");
            agency2.Attach(MobileApp2);
            var emailClient2 = new EmailClient("Verdi@gmail.com");
            agency2.Attach(emailClient2);
            Console.WriteLine("Inserisci messaggio nell'email: ");
            var message2 = Console.ReadLine();
            if (message2 != null)
            {
                agency2.Message = message2;
            }

        }
    }
}