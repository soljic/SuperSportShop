using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Interfaces.Repositories.User;
using Domain.Models;

namespace Persistence.Repositories.User
{
    public class UserRepository :  IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public async Task<List<ApplicationUser>> GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("GetAllUsers", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ApplicationUser user = new ApplicationUser();
                            user.Id = reader["Id"].ToString();
                            user.UserName = reader["UserName"].ToString();
                            user.Email = reader["Email"].ToString();
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while getting users: " + ex.Message);
            }
            return users;
        }

        public async Task<int> GetUsersCount()
        {
            int count = 0;
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("GetUsersCount", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    count = (int)await command.ExecuteScalarAsync();
                }
            }
            catch (Exception ex)
            {
                // Obrada iznimke prilikom dohvaćanja broja korisnika
                Console.WriteLine("Greška prilikom dohvaćanja broja korisnika: " + ex.Message);
            }
            return count;
        }


        public Task<ApplicationUser> GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> GetUserWithCredentials(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> GetUserApplication(int applicationId)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertUser(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUser(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}