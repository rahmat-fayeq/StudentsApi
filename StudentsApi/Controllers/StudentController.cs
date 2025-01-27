using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Data;
using StudentsApi.DTO;
using StudentsApi.Model;

namespace StudentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public StudentController(ApplicationDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> Students()
        {
            var students = await db.Students.OrderByDescending(x => x.CreatedAt).ToListAsync();
            var studentsDTO = students.Select(x => new StudentDTO {
                Id = x.Id,
                Name = x.Name,
                TutionFee = x.TutionFee,
                Department = x.Department,
                Address = x.Address,
                Remarks = x.Remarks
            }).ToList();

            return Ok(new {data = studentsDTO });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> student(int id)
        {
            var student = await db.Students.FindAsync(id);

            if (student == null )
            {
                return NotFound();
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                TutionFee = student.TutionFee,
                Department = student.Department,
                Address = student.Address,
                Remarks = student.Remarks
            };

            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> StorStudent([FromBody]StudentDTO studentDTO)
        {
            if (studentDTO.Id > 0)
            {
                return BadRequest(new {messag="You can not specity the id, it generate by stystem."});
            }

            var student = new Student
            {
                Name = studentDTO.Name,
                Department = studentDTO.Department,
                TutionFee = studentDTO.TutionFee,
                Address = studentDTO.Address,
                Remarks = studentDTO.Remarks,
            };

            db.Students.Add(student);
            await db.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDTO>> UpdateStudent(int id,StudentDTO studentDTO)
        {
            if(id != studentDTO.Id)
            {
                return BadRequest();
            }

            var student = await db.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.Name = studentDTO.Name;
            student.Department = studentDTO.Department;
            student.Address = studentDTO.Address;
            student.Remarks = studentDTO.Remarks;
            student.TutionFee = studentDTO.TutionFee;

            await db.SaveChangesAsync();

            return Ok(new {message="Student Updated"});

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await db.Students.FindAsync(id);
            if (student == null) 
            { 
                return NotFound();
            }

            db.Students.Remove(student);
            await db.SaveChangesAsync();

            return NoContent();
        }
    }
}
