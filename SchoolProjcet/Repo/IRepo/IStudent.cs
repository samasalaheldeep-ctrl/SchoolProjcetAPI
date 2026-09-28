using School.Models;

namespace SchoolProjcet.Repo.IRepo
{
    public interface IStudent
    {
        List<Student> GetAll();

        Student GetById(int id);

        void Add(Student student);

        void Update(Student student);

        void Delete(Student student);

        void Save();
    }
}
