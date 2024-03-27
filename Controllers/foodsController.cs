using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class foodsController : Controller
    {
        private readonly foodContext _context;
        private readonly IWebHostEnvironment _enc;

        public foodsController(foodContext context, IWebHostEnvironment enc)
        {
            _context = context;
            _enc = enc;
        }
        
        // GET: foods
        public async Task<IActionResult> Index()
        {
            return View(await _context.foods.ToListAsync());
        }

        // GET: foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.foods.Include(i => i.Items)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: foods/Create
        public IActionResult Create()
        {
            return View(new food());
        }

        // POST: foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,foodName,foodDescription,foodCode,Date,ImagePath,ImageFile,Items")] food food, string command = "")
        {
            if (food.ImageFile != null)
            {



                food.ImagePath = "\\Images\\" + food.ImageFile.FileName;


                string serverPath = _enc.WebRootPath + food.ImagePath;


                using FileStream stream = new FileStream(serverPath, FileMode.Create);


                await food.ImageFile.CopyToAsync(stream);

                ModelState.Remove("ImagePath");
            }
            if (command == "Add")
            {
                food.Items.Add(new());
                return View(food);
            }
            else if (command.Contains("delete"))// delete-3-sdsd-5   ["delete", "3"]
            {
                int idx = int.Parse(command.Split('-')[1]);

                food.Items.RemoveAt(idx);
                ModelState.Clear();
                return View(food);
            }
            if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }

        // GET: foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.foods.Include(i => i.Items).FirstOrDefaultAsync(i => i.ID == id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,foodName,foodDescription,foodCode,Date, Items")] food food, string command = "")
        {
            if (command == "Add")
            {
                food.Items.Add(new());
                return View(food);
            }
            else if (command.Contains("delete"))// delete-3-sdsd-5   ["delete", "3"]
            {
                int idx = int.Parse(command.Split('-')[1]);

                food.Items.RemoveAt(idx);
                ModelState.Clear();
                return View(food);
            }
            if (id != food.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(food);
                    //await _context.SaveChangesAsync();

                    var itemsIdList = food.Items.Select(i => i.ItemId).ToList();

                    var delItems = await _context.foodItems.Where(i => i.foodID == id).Where(i => !itemsIdList.Contains(i.ItemId)).ToListAsync();


                    _context.foodItems.RemoveRange(delItems);


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!foodExists(food.ID))
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
            return View(food);
        }

        // GET: foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.foods.Include(food => food.Items)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var food = await _context.foods.Include(food => food.Items)
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (food != null)
            //{
            //    _context.foods.Remove(food);
            //}
            await _context.Database.ExecuteSqlAsync($"exec spDeletefood3 {id}");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool foodExists(int id)
        {
            return _context.foods.Any(e => e.ID == id);
        }
    }
}
