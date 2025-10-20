using System;

#region interfacce
public interface IPaymentGateway
{
    void Paga(string metodo, int importo);
}

public interface IBanca
{
    void RiceviPagamento(string metodo, int importo);
}
#endregion

#region classi
public class PaymentProcessor : IPaymentGateway
{
    private IBanca _banca;
    public PaymentProcessor(IBanca banca)
    {
        _banca = banca;
    }

    public void Paga(string metodo, int importo)
    {
        Console.WriteLine($"[GATEWAY] Elaborazione pagamento tramite {metodo}...");
        _banca.RiceviPagamento(metodo, importo);
    }
}
public class BancaIntesa : IBanca
{
    public void RiceviPagamento(string metodo, int importo)
    {
        Console.WriteLine($"[BANCA INTESA] Ricevuto pagamento di {importo}€ tramite {metodo}.");
    }
}

public class Unicredit : IBanca
{
    public void RiceviPagamento(string metodo, int importo)
    {
        Console.WriteLine($"[UNICREDIT] Ricevuto pagamento di {importo}€ tramite {metodo}.");
    }
}

public class PayPalGateway
{
    private IPaymentGateway _gateway;

    public PayPalGateway(IPaymentGateway gateway)
    {
        _gateway = gateway;
    }

    public void Stampa(string metodo, int importo)
    {
        _gateway.Paga(metodo, importo);
    }
}

public class StripeGateway
{
    private IPaymentGateway _gateway;

    public StripeGateway(IPaymentGateway gateway)
    {
        _gateway = gateway;
    }

    public void Stampa(string metodo, int importo)
    {
        _gateway.Paga(metodo, importo);
    }
}
#endregion

#region main
public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Scegli la banca (Intesa/Unicredit) vuoto per uscire:");
            string? bancaScelta = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bancaScelta))
                break;

            IBanca banca;
            if (bancaScelta.ToLower().Trim() == "intesa")
                banca = new BancaIntesa();
            else if (bancaScelta.ToLower().Trim() == "unicredit")
                banca = new Unicredit();
            else
            {
                Console.WriteLine("Banca non valida!");
                continue;
            }

            IPaymentGateway gateway = new PaymentProcessor(banca);

            Console.WriteLine("Inserisci metodo di pagamento (PayPal/Stripe):");
            string? metodo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(metodo))
                break;

            if (metodo.ToLower().Trim() == "paypal")
            {
                PayPalGateway paypal = new PayPalGateway(gateway);
                paypal.Stampa(metodo, 100);
            }
            else if (metodo.ToLower().Trim() == "stripe")
            {
                StripeGateway stripe = new StripeGateway(gateway);
                stripe.Stampa(metodo, 200);
            }
            else
            {
                Console.WriteLine("Metodo di pagamento non valido");
            }

            Console.WriteLine();
        }
    }
}
#endregion
