using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EF_Task_5.Data;
using EF_Task_5.Models;

namespace EF_Task_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employee);
        }

        // Get employee details by ID
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetEmployee(int id)
        //{
        //    var employee = await _context.Employees
        //        .Include(e => e.Department) // Include department
        //        .Include(e => e.EmployeeProjects) // Include projects
        //            .ThenInclude(ep => ep.Project)
        //        .FirstOrDefaultAsync(e => e.EmployeeId == id);

        //    if (employee == null)
        //        return NotFound();

        //    return Ok(employee);
        //}
        [HttpGet]
        public async Task<IActionResult> GetEmployee(int pageNumber = 1, int pageSize = 5)
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            // Explicitly load related Department
            await _context.Entry(employee).Reference(e => e.Department).LoadAsync();

            return Ok(employee);
        }


        // Update employee details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete an employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
