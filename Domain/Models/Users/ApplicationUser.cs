using System;
using Microsoft.AspNet.Identity;

namespace Domain.Models
{
    public class ApplicationUser : IUser<string>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedOnDate { get; set; }

        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}