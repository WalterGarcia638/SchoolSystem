namespace SchoolSystemApi.Model.DTO
{
    public class GetStudentsDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
