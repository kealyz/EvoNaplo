using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EvoNaplo.DataAccessLayer;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using EvoNaplo.Services;
using System;
using System.Net.Http;
using EvoNaplo.Models;
using System.Text;
using System.Text.Json;
using EvoNaplo.Models.DTO;
using System.Threading.Tasks;

namespace EvoNaplo.IntegrationTests
{
    [TestClass]
    public class MentorControllerTest
    {
        private SqliteConnection sqliteConnection;
        private ServiceProvider serviceProvider;
        private HttpClient client;

        [TestInitialize]
        public void TestInitializer()
        {
            var builder = new WebHostBuilder().ConfigureServices((IServiceCollection services) =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EvoNaploContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                sqliteConnection = new SqliteConnection("DataSource=:memory:");
                sqliteConnection.Open();

                services.AddDbContext<EvoNaploContext>((options, context) =>
                {

                    context.UseSqlite(sqliteConnection);
                }
                );

                serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<MentorService>>();
                    var context = scope.ServiceProvider.GetRequiredService<EvoNaploContext>();
                    context.Database.EnsureCreated();
                    try
                    {
                        SeedDb(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Seeding failed");

                    }
                }

            }
            ).UseStartup<Startup>().UseEnvironment("production");

            var server = new TestServer(builder);

            client = server.CreateClient();

        }

        [TestCleanup]
        public void TestCleanup()
        {

            sqliteConnection.Close();

        }

        private void SeedDb(EvoNaploContext context)
        {
            context.Users2.Add(new User("kiss.jozsi@gmail.com", "password1", "Kiss", "József", "06301561234", Role.Mentor));
            context.Users2.Add(new User("koaxk.abel@gmail.com", "password2", "Koaxk", "Ábel", Role.Mentor));
            context.Users2.Add(new User("nagy.agoston@gmail.com", "password3", "Nagy", "Ágoston", Role.Mentor));
            context.SaveChanges();        
        }

        [TestMethod]
        public async Task PostAddMentor_expectsMentorDto_shouldAddNewMentorToDataBase()
        {
            //Arrange
            string requestUrl = "api/Mentor";
            string jsonContent = JsonSerializer.Serialize(new MentorDto("laudon.andrea@gmail.com", "passwordNew", "Laudon", "Andrea", "36301239876"));
            StringContent requestBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(requestUrl, requestBody);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task GetMentors_noArguments_shouldListAllMentors()
        {
            //Arrange
            var request = "api/Mentor";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task PutEditMentors_expectsMentorIdAndMentorDto_shouldEditMentorThenReturnAll()
        {
            //Arrange
            int idOfMentorToEdit = 1;
            string requestUrl = $"api/Mentor/edit?id={idOfMentorToEdit}";
            string jsonContent = JsonSerializer.Serialize(new MentorDto("laudon.andrea@gmail.com", "passwordNew", "Laudon", "Andrea", "36301239876"));
            StringContent requestBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(requestUrl, requestBody);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task PutInactivateMentor_expectsMentorId_shouldSetMentorWithGivenIdToNotActiveThenReturnAll()
        {
            //Arrange
            int idOfMentorToInactivate = 1;
            string requestUrl = $"api/Mentor/inactivate?id={idOfMentorToInactivate}";
            StringContent requestBody = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(requestUrl, requestBody);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task DeleteMentor_expectsMentorId_shouldDeleteMentorFromDataBase()
        {
            //Arrange
            int idOfMentorToEdit = 1;
            string requestUrl = $"api/Mentor/delete?id={idOfMentorToEdit}";

            //Act
            var response = await client.DeleteAsync(requestUrl);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
