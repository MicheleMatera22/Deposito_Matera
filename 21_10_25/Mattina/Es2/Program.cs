#region interface
public interface IStorageService
{
    void Carica(string metodo);
}
#endregion

#region implementazione
public class DiskService : IStorageService
{
    public void Carica(string metodo)
    {
        Console.WriteLine($"File caricato su {metodo}");
    }
}

public class MemoryService : IStorageService
{
    public void Carica(string metodo)
    {
        Console.WriteLine($"File caricato su {metodo}");
    }
}
#endregion

#region File uploader
public class FileUploader
{
    public IStorageService? StorageService { get; set; }
    public void SalvaFile(string nomeFile, string metodo)
    {
        if (StorageService == null)
        {
            Console.WriteLine("Failed");
        }
        else
        {
            Console.WriteLine($"{nomeFile}: ");
            StorageService.Carica(metodo);
        }
    }
}
#endregion

#region main
public class Program
{
    public static void Main(string[] args)
    {
        var uploader = new FileUploader();
        
        while(true)
        {
            Console.WriteLine("Inserisci nome del file (exit per uscire): ");
            string? nomeFile = Console.ReadLine();
            if (nomeFile.Trim().ToLower() == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Inserisci metodo di salvataggio (disk/memory): ");
                var metodo = Console.ReadLine();
                uploader.StorageService = metodo.Trim().ToLower() == "disk" ? new DiskService() : new MemoryService();
                uploader.SalvaFile(nomeFile, metodo);
                Console.WriteLine("\n");
            }
        }

    }
}
#endregion