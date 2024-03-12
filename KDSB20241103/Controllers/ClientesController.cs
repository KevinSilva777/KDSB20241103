using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KDSB20241103.Models;

namespace KDSB20241103.Controllers
{
    public class ClientesController : Controller
    {
        private readonly KDSB20241103DBContext _context;

        public ClientesController(KDSB20241103DBContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Clientes != null ? 
                          View(await _context.Clientes.ToListAsync()) :
                          Problem("Entity set 'KDSB20241103DBContext.Clientes'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(s => s.TelefonoCliente)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            var cliente = new Cliente();
            cliente.FechaRegistro = DateTime.Now;
            cliente.TelefonoCliente = new List<TelefonoCliente>();
            cliente.TelefonoCliente.Add(new TelefonoCliente
            {

            });
            ViewBag.Accion = "Create";
            return View(cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,NombreCliente,FechaRegistro,TelefonoCliente")] Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult AgregarDetalles([Bind("IdCliente,NombreCliente,FechaRegistro,TelefonoCliente")] Cliente cliente, string accion)
        {
            cliente.TelefonoCliente.Add(new TelefonoCliente { });
            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        public ActionResult EliminarDetalles([Bind("IdCliente,NombreCliente,FechaRegistro,TelefonoCliente")] Cliente cliente, int index, string accion)
        {
            var det = cliente.TelefonoCliente[index];
            if (accion == "Edit" && det.IdCliente > 0)
            {
                det.IdCliente = det.IdCliente * -1;
            }
            else
            {
                cliente.TelefonoCliente.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(s => s.TelefonoCliente)
                .FirstAsync(s => s.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,NombreCliente,FechaRegistro,TelefonoCliente")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            try
            {
                // Obtener los datos de la base de datos que van a ser modificados
                var facturaUpdate = await _context.Clientes
                        .Include(s => s.TelefonoCliente)
                        .FirstAsync(s => s.IdCliente == cliente.IdCliente);
                facturaUpdate.NombreCliente = cliente.NombreCliente;
                facturaUpdate.FechaRegistro = cliente.FechaRegistro;
                
                //facturaUpdate.Estado = proyecto.Estado;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = cliente.TelefonoCliente.Where(s => s.IdCliente == 0);
                foreach (var d in detNew)
                {
                    facturaUpdate.TelefonoCliente.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = cliente.TelefonoCliente.Where(s => s.IdCliente > 0);
                foreach (var d in detUpdate)
                {
                    var det = facturaUpdate.TelefonoCliente.FirstOrDefault(s => s.IdCliente == d.IdCliente);
                    det.NumeroTelefono = d.NumeroTelefono;
                    
                    
                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = cliente.TelefonoCliente.Where(s => s.IdCliente < 0).ToList();
                if (delDet != null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.IdCliente = d.IdCliente * -1;
                        var det = facturaUpdate.TelefonoCliente.FirstOrDefault(s => s.IdCliente == d.IdCliente);
                        _context.Remove(det);
                        // facturaUpdate.DetFacturaVenta.Remove(det);
                    }
                }
                // Aplicar esos cambios a la base de datos


                _context.Update(facturaUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.IdCliente))
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

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(s => s.TelefonoCliente)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Delete";
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'KDSB20241103DBContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
