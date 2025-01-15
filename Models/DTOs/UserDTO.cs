using System.Text.Json.Serialization;

namespace TaskBackendAPI.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public Role Role { get; set; }
        [JsonIgnore]
        public List<TaskDTO> Tasks { get; set; }
    }
}
