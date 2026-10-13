using MySql.Data.MySqlClient;
using WebApiStart.DTOs;
using WebApiStart.Models;

namespace WebApiStart.Services
{
    public class GyogyszerService
    {
        private readonly string connectionString =
            "SERVER=localhost;" +
            "DATABASE=patika;" +
            "UID=root;" +
            "PASSWORD=;";

        public List<Gyogyszer> GetAllGyogyszer()
        {
            List<Gyogyszer> gyogyszerek = new List<Gyogyszer>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM gyogyszer";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Gyogyszer gyogyszer = new Gyogyszer
                            {
                                Id = reader.GetInt32("Id"),
                                Nev = reader.GetString("Nev"),
                                Hatoanyag = reader.GetString("Hatoanyag"),
                                Venykoteles = reader.GetBoolean("Venykoteles"),
                                Kepnev = reader.GetString("Kepnev")
                            };

                            gyogyszerek.Add(gyogyszer);
                        }
                    }
                }

                return gyogyszerek;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba! " + ex.Message);
                throw;
            }
        }

        public Gyogyszer GetById(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM gyogyszer WHERE Id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Gyogyszer gyogyszer = new Gyogyszer
                                {
                                    Id = reader.GetInt32("Id"),
                                    Nev = reader.GetString("Nev"),
                                    Hatoanyag = reader.GetString("Hatoanyag"),
                                    Venykoteles = reader.GetBoolean("Venykoteles"),
                                    Kepnev = reader.GetString("Kepnev")
                                };

                                return gyogyszer;
                            }

                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba! " + ex.Message);
                throw;
            }
        }

       
    }
}