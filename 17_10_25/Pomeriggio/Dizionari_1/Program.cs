public class Program
{
    public static void Main(string[] args)
    {

        //RUBRICA
        Dictionary<string, string> rubrica = new Dictionary<string, string>();
        string nome, numero;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                Console.Write("Inserisci nome: ");
                nome = Console.ReadLine();
                if ((rubrica.ContainsKey(nome.ToLower())) || string.IsNullOrEmpty(nome))
                {
                    Console.WriteLine("Nome già esistente o vuoto!");
                }
            } while ((rubrica.ContainsKey(nome.ToLower())) || string.IsNullOrEmpty(nome));
            do
            {
                Console.Write("Inserisci numero: ");
                numero = Console.ReadLine();
                if ((rubrica.ContainsValue(numero)) || string.IsNullOrEmpty(numero))
                {
                    Console.WriteLine("Numero già esistente o vuoto!");
                }
            } while ((rubrica.ContainsKey(numero.ToLower())) || string.IsNullOrEmpty(numero));
            rubrica.Add(nome.ToLower(), numero);
        }
        foreach (var kvp in rubrica)
            Console.WriteLine($"{kvp.Key} - {kvp.Value}");

        //Conteggio parole
        Console.WriteLine("Inserisci una frase: ");
        string frase = Console.ReadLine();
        string[] parole = frase.Split(' ');
        Dictionary<string, int> conteggioParole = new Dictionary<string, int>();
        foreach (var parola in parole)
        {
                if (conteggioParole.ContainsKey(parola))
                {
                    conteggioParole[parola]++;
                }
                else
                {
                    conteggioParole.Add(parola, 1);
                }
        }
        foreach (var kvp in conteggioParole)
            Console.WriteLine($"{kvp.Key} - {kvp.Value}");

        //gestione prodotti
        Dictionary<string, int> prodotti = new Dictionary<string, int>();
        string nome1;
        while (true)
        {
            Console.WriteLine("\n=== MENU INVENTARIO ===");
            Console.WriteLine("1. Aggiungi un prodotto (o aumenta la quantità se esiste già)");
            Console.WriteLine("2. Rimuovi un prodotto");
            Console.WriteLine("3. Cerca un prodotto e mostra quantità");
            Console.WriteLine("4. Stampa l'inventario");
            Console.WriteLine("5. Esci");
            Console.Write("Scelta: ");
            
            string scelta = Console.ReadLine() ?? "";

            switch (scelta)
            {
                case "1":
                    Console.Write("Nome del prodotto: ");
                    nome1 = Console.ReadLine() ?? "";
                    Console.Write("Quantità da aggiungere: ");
                    if (int.TryParse(Console.ReadLine(), out int qta))
                    {
                        if (prodotti.ContainsKey(nome1))
                        {
                            prodotti[nome1] += qta;
                        }
                        else
                        {
                            prodotti.Add(nome1, qta);
                        }
                        Console.WriteLine($"Prodotto '{nome1}' aggiornato. Quantità totale: {prodotti[nome1]}");
                    }
                    else
                    {
                        Console.WriteLine("Quantità non valida!");
                    }
                    break;

                case "2":
                    Console.Write("Nome del prodotto da rimuovere: ");
                    string daRimuovere = Console.ReadLine() ?? "";
                    if (prodotti.Remove(daRimuovere))
                        Console.WriteLine($"Prodotto '{daRimuovere}' rimosso.");
                    else
                        Console.WriteLine("Prodotto non trovato.");
                    break;

                case "3":
                    Console.Write("Nome del prodotto da cercare: ");
                    string daCercare = Console.ReadLine() ?? "";
                    if (prodotti.TryGetValue(daCercare, out int quantita))
                        Console.WriteLine($"Prodotto '{daCercare}' - Quantità: {quantita}");
                    else
                        Console.WriteLine("Prodotto non trovato.");
                    break;

                case "4":
                    Console.WriteLine("\n=== INVENTARIO COMPLETO ===");
                    if (prodotti.Count == 0)
                        Console.WriteLine("L'inventario è vuoto.");
                    else
                    {
                        foreach (var key in prodotti.Keys)
                        {
                            if (prodotti.TryGetValue(key, out int val))
                                Console.WriteLine($"Prodotto: {key}, Quantità: {val}");
                        }
                    }
                    break;

                case "5":
                    Console.WriteLine("Uscita dal programma...");
                    return;

                default:
                    Console.WriteLine("Scelta non valida!");
                    break;
            }
        }
    }
}

