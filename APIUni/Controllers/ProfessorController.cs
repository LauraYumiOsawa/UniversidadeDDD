using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidade.Infra;
using Universidade.Domain;

namespace APIUni.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfessoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfessoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var professores = await _context.Professores.ToListAsync();
        return Ok(professores);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Professor professor)
    {
        _context.Professores.Add(professor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = professor.Id }, professor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Professor professor)
    {
        if (professor == null || id != professor.Id)
        {
            return BadRequest("No tienes cararajo");
        }

        var professorExistente = await _context.Professores.FindAsync(id);
        if (professorExistente == null)
        {
            return NotFound($"U INCOPETENTE, ALUNO NOT FOUND - {id}");
        }

        professorExistente.Name = professor.Name;
        professorExistente.Email = professor.Email;
        professorExistente.Cpf = professor.Cpf;


        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var professor = await _context.Professores.FindAsync(id);
        if (professor == null)
        {
            return BadRequest("No tienes cararajo");
        }

        _context.Professores.Remove(professor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}