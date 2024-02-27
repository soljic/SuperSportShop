namespace Web.Models
{
    using System;

    public class UsersViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedOnDate { get; set; }
    }

}