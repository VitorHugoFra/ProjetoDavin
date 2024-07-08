using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoDavin.Data;
using ProjetoDavin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDavin
{
  public class ContatosController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ContatosController(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      var contatos = await _context.Contatos.Include(c => c.Telefones).ToListAsync();
      return View(contatos);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contato contato, string telefone)
    {
      if (ModelState.IsValid)
      {
        _context.Add(contato);
        await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(telefone))
        {
          var novoTelefone = new Telefone
          {
            IdContato = contato.Id,
            Numero = telefone
          };
          _context.Add(novoTelefone);
          await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
      }
      return View(contato);
    }

    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contato = await _context.Contatos.Include(c => c.Telefones).FirstOrDefaultAsync(c => c.Id == id);
      if (contato == null)
      {
        return NotFound();
      }
      return View(contato);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Contato contato, string telefone)
    {
      if (id != contato.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(contato);
          if (!string.IsNullOrEmpty(telefone))
          {
            var novoTelefone = new Telefone
            {
              IdContato = contato.Id,
              Numero = telefone
            };
            _context.Add(novoTelefone);
          }
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ContatoExists(contato.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(contato);
    }

    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contato = await _context.Contatos.Include(c => c.Telefones).FirstOrDefaultAsync(m => m.Id == id);
      if (contato == null)
      {
        return NotFound();
      }

      return View(contato);
    }

    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var contato = await _context.Contatos.Include(c => c.Telefones).FirstOrDefaultAsync(m => m.Id == id);
      if (contato == null)
      {
        return NotFound();
      }

      return View(contato);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var contato = await _context.Contatos.FindAsync(id);
      _context.Contatos.Remove(contato);
      await _context.SaveChangesAsync();

      // Log de exclusão
      System.IO.File.AppendAllText("log.txt", $"Contato {contato.Nome} excluído em {DateTime.Now}\n");

      return RedirectToAction(nameof(Index));
    }

    private bool ContatoExists(int id)
    {
      return _context.Contatos.Any(e => e.Id == id);
    }

    [HttpPost]
    public async Task<IActionResult> Search(string nome, string numero)
    {
      var contatos = await _context.Contatos
        .Include(c => c.Telefones)
        .Where(c => c.Nome.Contains(nome) || c.Telefones.Any(t => t.Numero.Contains(numero)))
        .ToListAsync();

      return View("Index", contatos);
    }
  }
}
