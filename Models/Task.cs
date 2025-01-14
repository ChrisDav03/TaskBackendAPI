using System.Text.Json.Serialization;

namespace TaskBackendAPI.Models
{
    public enum Status
    {
        pending,
        inProgress,
        completed
    }
    public class Task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Status status { get; set; }
        public int? userAsignedId { get; set; }
        public User userAssigned { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        
    }
}
