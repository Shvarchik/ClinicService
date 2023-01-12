using ClinicService.Services;
using ClinicService.Services.Impl;
using System.Data.SQLite;

namespace ClinicService
{

    /// <summary>
    /// SQLite
    /// https://sqlitestudio.pl/
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            //ConfigureSQLiteConnection();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IPetRepository, PetRepository>();
            builder.Services.AddScoped<IConsultationRepository, ConsultationRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureSQLiteConnection()
        {
            const string connectionString = "Data Source = clinic.db; Version = 3; Pooling = true; Max Pool Size = 100;";
            SQLiteConnection sQLiteConnection = new SQLiteConnection(connectionString);
            sQLiteConnection.Open();
            PrepareSchema(sQLiteConnection);

        }

        private static void PrepareSchema(SQLiteConnection sQLiteConnection)
        {
            SQLiteCommand sQLiteCommand = new SQLiteCommand(sQLiteConnection);

            sQLiteCommand.CommandText = "DROP TABLE IF EXISTS consultations";
            sQLiteCommand.ExecuteNonQuery();
            sQLiteCommand.CommandText = "DROP TABLE IF EXISTS pets";
            sQLiteCommand.ExecuteNonQuery();
            sQLiteCommand.CommandText = "DROP TABLE IF EXISTS clients";
            sQLiteCommand.ExecuteNonQuery();

            sQLiteCommand.CommandText =
                    @"CREATE TABLE Clients(
                    ClientId INTEGER PRIMARY KEY,
                    Document TEXT,
                    SurName TEXT,
                    FirstName TEXT,
                    Patronymic TEXT,
                    Birthday INTEGER)"; // date.Ticks
            sQLiteCommand.ExecuteNonQuery();
            sQLiteCommand.CommandText =
                    @"CREATE TABLE Pets(
                    PetId INTEGER PRIMARY KEY,
                    ClientId INTEGER,
                    Name TEXT,
                    Birthday INTEGER)";
            sQLiteCommand.ExecuteNonQuery();
            sQLiteCommand.CommandText =
                @"CREATE TABLE Consultations(
                    ConsultationId INTEGER PRIMARY KEY,
                    ClientId INTEGER,
                    PetId INTEGER,
                    ConsultationDate INTEGER,
                    Description TEXT)";
            sQLiteCommand.ExecuteNonQuery();
        }
    }
}