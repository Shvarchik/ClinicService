using ClinicService.Models;
using System.Data.SQLite;

namespace ClinicService.Services.Impl
{
    public class ClientRepository : IClientRepository
    {
        const string connectionString = "Data Source = clinic.db; Version = 3; Pooling = true; Max Pool Size = 100;";

        public int Create(Client item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO clients(Document, SurName, FirstName, Patronymic, Birthday) VALUES(@Document, @SurName, @FirstName, @Patronymic, @Birthday)";
            command.Parameters.AddWithValue("@Document", item.Document);
            command.Parameters.AddWithValue("@SurName", item.SurName);
            command.Parameters.AddWithValue("@FirstName", item.FirstName);
            command.Parameters.AddWithValue("@Patronymic", item.Patronymic);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Update(Client item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE clients SET Document = @Document, FirstName = @FirstName, SurName = @SurName, Patronymic = @Patronymic, Birthday = @Birthday WHERE ClientId=@ClientId";
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@Document", item.Document);
            command.Parameters.AddWithValue("@SurName", item.SurName);
            command.Parameters.AddWithValue("@FirstName", item.FirstName);
            command.Parameters.AddWithValue("@Patronymic", item.Patronymic);
            command.Parameters.AddWithValue("@Birthday", item.Birthday.Ticks);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Delete(int item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "DELETE FROM clients WHERE ClientId=@ClientId";
            command.Parameters.AddWithValue("@ClientId", item);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public IList<Client> GetAll()
        {
            List<Client> list = new List<Client>(); 
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM clients";
            SQLiteDataReader sQLiteDataReader = command.ExecuteReader();
            while (sQLiteDataReader.Read())
            {
                Client client = new Client();
                client.ClientId = sQLiteDataReader.GetInt32(0);
                client.Document = sQLiteDataReader.GetString(1);
                client.SurName = sQLiteDataReader.GetString(2);
                client.FirstName = sQLiteDataReader.GetString(3);
                client.Patronymic = sQLiteDataReader.GetString(4);
                client.Birthday =  new DateTime(sQLiteDataReader.GetInt64(5));
                list.Add(client);
            }
            connection.Close();
            return list;
        }

        public Client GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM clients WHERE ClientId=@ClientId";
            command.Parameters.AddWithValue("@ClientId", id);
            command.Prepare();
            SQLiteDataReader sQLiteDataReader = command.ExecuteReader();
            if (sQLiteDataReader.Read())
            {
                Client client = new Client();
                client.ClientId = sQLiteDataReader.GetInt32(0);
                client.Document = sQLiteDataReader.GetString(1);
                client.SurName = sQLiteDataReader.GetString(2);
                client.FirstName = sQLiteDataReader.GetString(3);
                client.Patronymic = sQLiteDataReader.GetString(4);
                client.Birthday = new DateTime(sQLiteDataReader.GetInt64(5));
                connection.Close( );
                return client;
            }
            else
            {
                return null;
            }
        }

    }
}
