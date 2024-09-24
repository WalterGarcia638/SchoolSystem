using SchoolSystemApi.Model;

namespace SchoolSystemApi.Repository.IRepository
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudentByName(string name);
        bool ExistStudent(string name);
        bool ExistStudent(int id);
        bool CreateStudent(Student Student);
        bool UpdateStudent(Student Student);
        bool DeleteStudent(Student Student);
        bool Save();
    }
}
