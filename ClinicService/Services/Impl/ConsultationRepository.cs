using ClinicService.Models;
using System.Data.SQLite;

namespace ClinicService.Services.Impl
{
    public class ConsultationRepository : IConsultationRepository
    {
        const string connectionString = "Data Source = clinic.db; Version = 3; Pooling = true; Max Pool Size = 100;";
        public int Create(Consultation item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO consultations (ClientId, PetId, ConsultationDate, Description) VALUES (@ClientId, @PetId, @ConsultationDate, @Description)";
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@PetId", item.PetId);
            command.Parameters.AddWithValue("@ConsultationDate", item.ConsultationDate.Ticks);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Update (Consultation item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "UPDATE consultations SET ClientId = @ClientId, PetId = @PetId, ConsultationDate = @ConsultationDate, Description = @Description WHERE ConsultationId = @ConsultationId";
            command.Parameters.AddWithValue("@ConsultationId", item.ConsultationId);
            command.Parameters.AddWithValue("@ClientId", item.ClientId);
            command.Parameters.AddWithValue("@PetId", item.PetId);
            command.Parameters.AddWithValue("@ConsultationDate", item.ConsultationDate.Ticks);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public int Delete (int item)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "DELETE FROM consultations WHERE ConsultationId = @ConsultationId";
            command.Parameters.AddWithValue("@ConsultationId", item);
            command.Prepare();
            int res = command.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public IList<Consultation> GetAll()
        {
            List<Consultation> consultations = new List<Consultation>();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM consultations ORDER BY ConsultationDate";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            { 
                Consultation consultation= new Consultation();
                consultation.ConsultationId = reader.GetInt32(0);
                consultation.ClientId = reader.GetInt32(1);
                consultation.PetId = reader.GetInt32(2);
                consultation.ConsultationDate = new DateTime(reader.GetInt64(3));
                consultation.Description= reader.GetString(4);
                consultations.Add(consultation);
            }   
            connection.Close();
            return consultations;
        }

        public IList<Consultation> GetAllByPetIdForPeriod(int petId, DateTime dateFrom, DateTime dateTo)
        {
            List<Consultation> consultations = new List<Consultation>();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM consultations WHERE PetId = @PetID AND ConsultationDate > @dateFrom AND ConsultationDate < @dateTo";
            command.Parameters.AddWithValue("@PetId", petId);
            command.Parameters.AddWithValue("@dateFrom",dateFrom.Ticks);
            command.Parameters.AddWithValue("@dateTo",dateTo.Ticks);
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Consultation consultation = new Consultation();
                consultation.ConsultationId = reader.GetInt32(0);
                consultation.ClientId = reader.GetInt32(1);
                consultation.PetId = reader.GetInt32(2);
                consultation.ConsultationDate = new DateTime(reader.GetInt64(3));
                consultation.Description = reader.GetString(4);
                consultations.Add(consultation);
            }
            connection.Close();
            return consultations;
        }

        public Consultation GetById (int id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "SELECT * FROM consultations WHERE ConsultationId=@ConsultationId";
            command.Parameters.AddWithValue("@ConsultationId", id);
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Consultation consultation = new Consultation();
                consultation.ConsultationId = reader.GetInt32(0);
                consultation.ClientId = reader.GetInt32(1);
                consultation.PetId = reader.GetInt32(2);
                consultation.ConsultationDate = new DateTime(reader.GetInt64(3));
                consultation.Description = reader.GetString (4);
                
                connection.Close();
                return consultation;
            }
            else
            {
                return null;
            }
        }
    }
}
