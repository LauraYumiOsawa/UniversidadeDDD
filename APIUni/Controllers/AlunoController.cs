using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidade.Infra;
using Universidade.Domain;

namespace APIUni.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlunosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var alunos = await _context.Alunos.ToListAsync();
        return Ok(alunos);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Aluno aluno)
    {
        _context.Alunos.Add(aluno);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = aluno.Id }, aluno);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Aluno aluno)
    {
        if (aluno == null || id != aluno.Id)
        {
            return BadRequest("Dados não batem");
        }
        
        var alunoExistente = await _context.Alunos.FindAsync(id);
        if (alunoExistente == null)
        {
            return NotFound($"Aluno não encontrado - {id}");
        }
        
        alunoExistente.Name = aluno.Name;
        alunoExistente.Email = aluno.Email;
        alunoExistente.Cpf = aluno.Cpf;
        alunoExistente.Phone = aluno.Phone;
        alunoExistente.Enrollment = aluno.Enrollment;
        alunoExistente.Birthday = aluno.Birthday;

        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var aluno = await _context.Alunos.FindAsync(id);
        if (aluno == null )
        {
            return BadRequest("No tienes carara**(forma da laura dizer Aluno não encontrado)");
        }
        
        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}