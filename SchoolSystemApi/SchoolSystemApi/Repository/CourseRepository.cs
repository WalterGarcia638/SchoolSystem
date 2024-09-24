using SchoolSystemApi.Data;
using SchoolSystemApi.Model;
using SchoolSystemApi.Repository.IRepository;

namespace SchoolSystemApi.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public ICollection<Course> GetCourses()
        {
            return _db.Course.OrderBy(b => b.Id).ToList();
        }

        public bool CreateCourse(Course Course)
        {
            _db.Course.Add(Course);
            return Save();
        }

        public bool DeleteCourse(Course Course)
        {
            _db.Course.Remove(Course);
            return Save();
        }

        public bool ExistCourse(string name)
        {
            bool value = _db.Course.Any(b => b.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool ExistCourse(int id)
        {
            return _db.Course.Any(brand => brand.Id == id);
        }

        public Course GetCourse(int id)
        {
            return _db.Course.FirstOrDefault(b => b.Id == id);
        }

        public Course GetCourseByName(string name)
        {
            return _db.Course.FirstOrDefault(b => b.Name == name);
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCourse(Course Course)
        {
            _db.Course.Update(Course);
            return Save();
        }
    }
}
