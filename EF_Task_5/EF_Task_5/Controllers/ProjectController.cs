using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EF_Task_5.Data;
using EF_Task_5.Models;


namespace EF_Task_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // Create a new project
        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProject), new { id = project.ProjectId }, project);
        }

        //Get project details with employees
       //[HttpGet("{id}")]
       // public async Task<IActionResult> GetProject(int id)
       // {
       //     var project = await _context.Projects
       //         .Include(p => p.EmployeeProjects) // Include EmployeeProjects
       //             .ThenInclude(ep => ep.Employee)
       //         .FirstOrDefaultAsync(p => p.ProjectId == id);

       //     if (project == null)
       //         return NotFound();

       //     return Ok(project);
       // }
       // //==============================================================================================
       // [HttpGet]
       // public async Task<IActionResult> GetProject(int pageNumber = 1, int pageSize = 5)
       // {
       //     var projects = await _context.Projects
       //         .Skip((pageNumber - 1) * pageSize)
       //         .Take(pageSize)
       //         .ToListAsync();

       //     return Ok(projects);
       // }

        [HttpGet("{id}")]
        //here GetProjectById
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.EmployeeProjects) // Load the join table
                .ThenInclude(ep => ep.Employee)  // Load employees from join table
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }



        // Update project details
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, Project project)
        {
            if (id != project.ProjectId)
                return BadRequest();

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a project
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
