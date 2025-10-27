using System;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        string connDb = "server=localhost;user=root;password=root;database=mydb1";
        using (MySqlConnection conn = new MySqlConnection(connDb))
        {
            try
            {
                conn.Open();
                Console.WriteLine("Connessione riuscita");

                string query = "Select messaggio from saluto limit 1";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["messaggio"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}