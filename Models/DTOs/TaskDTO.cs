namespace TaskBackendAPI.Models.DTOs
{
    public class TaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int? UserAsignedId { get; set; }

    }
}
