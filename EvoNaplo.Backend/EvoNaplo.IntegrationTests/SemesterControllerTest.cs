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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace EvoNaplo.IntegrationTests
{
    [TestClass]
    public class SemesterControllerTest
    {

        private SqliteConnection sqliteConnection;
        private ServiceProvider serviceProvider;
        private HttpClient client;

        [TestInitialize]
        public void TestInitializer()
        {
            var builder = new WebHostBuilder().ConfigureServices((IServiceCollection services)=> 
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EvoNaploContext>));
                if(descriptor!=null)
                {
                    services.Remove(descriptor);
                }

                sqliteConnection = new SqliteConnection("DataSource=:memory:");
                sqliteConnection.Open();

                services.AddDbContext<EvoNaploContext>((options,context) =>
                {
                    
                    context.UseSqlite(sqliteConnection);
                }
                );

                serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<SemesterService>>();
                    var context = scope.ServiceProvider.GetRequiredService<EvoNaploContext>();
                    context.Database.EnsureCreated();
                    try
                    {
                        SeedDb(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex,"Seeding failed");
                        
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
            context.Semesters.Add(new Semester(new DateTime(2020,6,3), new DateTime(2020,10,15), new DateTime(2020,10,16)));
            context.Semesters.Add(new Semester(new DateTime(2019, 10, 15), new DateTime(2020,1,5),new DateTime(2020,1,6)));
            context.Semesters.Add(new Semester(new DateTime(2020,1,10), new DateTime(2020,5,10), new DateTime(2020,05,11)));
            context.SaveChanges();
        }

       

        [TestMethod]
        public async Task PostAddSemester_ExpectsSemesterDto_shouldAddSemesterToDatabase()
        {
            //Arrange
            string requestUrl = "api/Semester";
            string jsonContent=JsonSerializer.Serialize(new SemesterDto() 
            {
                StartDate = new DateTime(2020, 01, 13),
                EndDate = new DateTime(2020, 06, 13),
                DemoDate = new DateTime(2020, 07, 13),
            }
            );
            StringContent requestBody = new StringContent(jsonContent ,Encoding.UTF8, "application/json");

            //Act
            var response = await client.PostAsync(requestUrl, requestBody);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task GetSemesters_noParameters_shouldReturnAllSemester()
        {
            //Arrange
            var request = "api/Semester";

            //Act
            var response = await client.GetAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task PutDeleteSemester_expectsSemesterId_shouldChangeSemesterStatusToFalse()
        {
            //Arrange
            int idOfSemesterToDeactivate = 1;
            var requestUrl = $"api/Semester?id={idOfSemesterToDeactivate}";

            StringContent requestBody = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync(requestUrl, requestBody);

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task PutEditSemester_expectsIdAndSemesterDto_shouldChangeSemesterDataToGivenDtosData()
        {
            //Arrange
            int idOfSemesterToEdit = 1;
            string jsonContent = JsonSerializer.Serialize(new SemesterDto()
            {
                StartDate = new DateTime(2020, 03, 13),
                EndDate = new DateTime(2020, 09, 13),
                DemoDate = new DateTime(2020, 10, 13),
            }
            );
            StringContent requestBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            //Act
            var response = await client.PutAsync($"api/Semester/edit?id={idOfSemesterToEdit}", requestBody);
            
            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
