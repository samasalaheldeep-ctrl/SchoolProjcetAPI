using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;

namespace SchoolProjcet.Repo.Impelementation
{
    public class StudentImplement
    {
        private readonly AppDbContext _context;

        public StudentImplement(AppDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAll()
        {
            return _context.Students
                .Include(s => s.ClassRoom)
                .ToList();
        }

        public Student GetById(int id)
        {
            var student = _context.Students.Include(s => s.ClassRoom)
                .FirstOrDefault(s => s.Id == id);
            return student;
                
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
        }

        public void Delete(Student student)
        {
            _context.Students.Remove(student);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

