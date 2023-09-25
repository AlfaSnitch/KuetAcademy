namespace KuetAcademy.Web.Models.Domain
{
    public class CoursePostComment
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid CourseId { get; set; }

        public Guid UserId { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
