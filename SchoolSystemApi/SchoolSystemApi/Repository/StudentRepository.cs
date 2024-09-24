using SchoolSystemApi.Data;
using SchoolSystemApi.Model;
using SchoolSystemApi.Repository.IRepository;

namespace SchoolSystemApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public ICollection<Student> GetStudents()
        {
            return _db.Student.OrderBy(b => b.Id).ToList();
        }

        public bool CreateStudent(Student Student)
        {
            _db.Student.Add(Student);
            return Save();
        }

        public bool DeleteStudent(Student Student)
        {
            _db.Student.Remove(Student);
            return Save();
        }

        public bool ExistStudent(string name)
        {
            bool value = _db.Student.Any(b => b.FirstName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistStudent(int id)
        {
            return _db.Student.Any(brand => brand.Id == id);
        }

        public Student GetStudent(int id)
        {
            return _db.Student.FirstOrDefault(b => b.Id == id);
        }

        public Student GetStudentByName(string firstName)
        {
            return _db.Student.FirstOrDefault(b => b.FirstName == firstName);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateStudent(Student Student)
        {
            _db.Student.Update(Student);
            return Save();
        }
    }
}
