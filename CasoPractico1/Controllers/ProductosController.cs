
using CasoPractico1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CasoPractico1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaContext _context;

        public ProductosController(TiendaContext context)
        {
            _context = context;
        }

        // Listar productos
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.UnidadMedida)
                .ToListAsync();

            return View(productos);
        }

        // Mostrar formulario para registrar un producto
        public async Task<IActionResult> Create()
        {
            await CargarUnidadesMedida();
            return View();
        }

        // Guardar el producto en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            var unidad = await _context.UnidadesMedida
                .FirstOrDefaultAsync(u => u.Id == producto.UnidadMedidaId);

            if (unidad?.Abreviatura == "und" &&
                producto.Existencia != decimal.Truncate(producto.Existencia))
            {
                ModelState.AddModelError(
                    nameof(producto.Existencia),
                    "La existencia debe ser un número entero para la unidad seleccionada."
                );
            }

            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            await CargarUnidadesMedida();

            return View(producto);
        }

        // Mostrar formulario para editar un producto
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            await CargarUnidadesMedida();

            return View(producto);
        }

        // Guardar los cambios del producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            var unidad = await _context.UnidadesMedida
                .FirstOrDefaultAsync(u => u.Id == producto.UnidadMedidaId);

            if (unidad?.Abreviatura == "und" &&
                producto.Existencia != decimal.Truncate(producto.Existencia))
            {
                ModelState.AddModelError(
                    nameof(producto.Existencia),
                    "La existencia debe ser un número entero para la unidad seleccionada."
                );
            }

            if (ModelState.IsValid)
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await CargarUnidadesMedida();

            return View(producto);
        }

        // Mostrar confirmación antes de eliminar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // Eliminar el producto confirmado
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.UnidadMedida)
                .FirstOrDefaultAsync(p => p.Id == id);

            return producto == null ? NotFound() : View(producto);
        }


        private async Task CargarUnidadesMedida()
        {
            ViewBag.UnidadesMedida = new SelectList(
                await _context.UnidadesMedida
                    .OrderBy(u => u.Nombre)
                    .ToListAsync(),"Id","Nombre");
        }



    }


}
