using SchoolSystemApi.Model;

namespace SchoolSystemApi.Repository.IRepository
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourses();
        Course GetCourse(int id);
        Course GetCourseByName(string name);
        bool ExistCourse(string name);
        bool ExistCourse(int id);
        bool CreateCourse(Course Course);
        bool UpdateCourse(Course Course);
        bool DeleteCourse(Course Course);
        bool Save();
    }
}
