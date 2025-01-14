using System.Text.Json.Serialization;

namespace TaskBackendAPI.Models
{
    public enum Role
    {
        admin,
        user
    }
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Role role { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
