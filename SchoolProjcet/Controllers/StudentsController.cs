using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTO.StudentDTOs;
using SchoolProjcet.Mapping;
using SchoolProjcet.Repo.IRepo;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _repo;
        private readonly IMapper _mapper;

        public StudentsController(IStudent repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _repo.GetAll();

            var studentDTOs =
                _mapper.Map<List<StudentDTO>>(students);

            return Ok(studentDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _repo.GetById(id);

            if (student == null)
            {
                return NotFound(
                    $"Student with id {id} not found."
                );
            }

            var studentDTO =
                _mapper.Map<StudentDTO>(student);

            return Ok(studentDTO);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateStudentDTO studentDTO)
        {
            if (studentDTO == null)
            {
                return BadRequest(
                    "Student data cannot be null."
                );
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student =
                _mapper.Map<Student>(studentDTO);

            _repo.Add(student);
            _repo.Save();

            var result =
                _mapper.Map<StudentDTO>(student);

            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(
            int id,
            UpdateStudentDTO studentDTO)
        {
            if (studentDTO == null)
            {
                return BadRequest(
                    "Student data cannot be null."
                );
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingStudent = _repo.GetById(id);

            if (existingStudent == null)
            {
                return NotFound(
                    $"Student with id {id} not found."
                );
            }

            _mapper.Map(studentDTO, existingStudent);

            _repo.Update(existingStudent);
            _repo.Save();

            var result =
                _mapper.Map<StudentDTO>(existingStudent);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var existingStudent = _repo.GetById(id);

            if (existingStudent == null)
            {
                return NotFound(
                    $"Student with id {id} not found."
                );
            }

            var result =
                _mapper.Map<StudentDTO>(existingStudent);

            _repo.Delete(existingStudent);
            _repo.Save();

            return Ok(result);
        }
    }
}
