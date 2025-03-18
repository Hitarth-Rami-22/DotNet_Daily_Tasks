using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EF_Task_5.Data;
using EF_Task_5.Models;

namespace EF_Task_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
        }

        // Get department details with employees
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDepartment(int id)
        //{
        //    var department = await _context.Departments
        //        .Include(d => d.Employees) // Eager loading of employees
        //        .FirstOrDefaultAsync(d => d.DepartmentId == id);

        //    if (department == null)
        //        return NotFound();

        //    return Ok(department);
        //}
        //=====================================================================================
        //[HttpGet]
        //public async Task<IActionResult> GetDepartment(int pageNumber = 1, int pageSize = 5)
        //{
        //    var departments = await _context.Departments
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return Ok(departments);
        //}

        [HttpGet("{id}")]
        //here GetDepartmentById
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Employees) // Load employees along with department
                .FirstOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }


        // Update department details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
                return BadRequest();

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a department
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return NotFound();

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
