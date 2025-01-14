namespace TaskBackendAPI.Models
{
    public class Task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string userAsignedId { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
