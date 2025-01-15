using System.Text.Json.Serialization;

namespace TaskBackendAPI.Models.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public Status Status { get; set; }
        [JsonIgnore]
        public int? UserAssignedId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public UserTaskDTO UserAssigned { get; set; }

    }
}
