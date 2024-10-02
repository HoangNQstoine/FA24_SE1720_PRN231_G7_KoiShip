using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShip_DB.Data.Models;
using KoiShip.Common;
using Newtonsoft.Json;
using KoiShip.Service.Base;
using Azure;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KoiShip.MVCWebApp.Controllers
{
    public class ShippingOrdersController : Controller
    {
        private readonly KoiShipDbContext _context;

        public ShippingOrdersController(KoiShipDbContext context)
        {
            _context = context;
        }

        // GET: ShippingOrders
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "ShippingOrders"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<ShippingOrder>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ShippingOrder>());
    }
        public async Task<IActionResult> Delete(int? id)
        {

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var respones = await httpClient.GetAsync(Const.API + "ShippingOrders/" + id))
                    {
                        if (respones.IsSuccessStatusCode)
                        {
                            var content = await respones.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<ShippingOrder>(result.Data.ToString());
                                return View(data); 

                            }
                          
                        }
                    }
                }
            }

            return View(new ShippingOrder());
        }

        // GET: ShippingOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingOrder = await _context.ShippingOrders
                .Include(s => s.Pricing)
                .Include(s => s.ShipMent)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingOrder == null)
            {
                return NotFound();
            }

            return View(shippingOrder);
        }

        // GET: ShippingOrders/Create
        public IActionResult Create()
        {
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Currency");
            ViewData["ShipMentId"] = new SelectList(_context.ShipMents, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address");
            return View();
        }

        // POST: ShippingOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,UserId,PricingId,ShipMentId,AdressTo,PhoneNumber,TotalPrice,Description,OrderDate,ShippingDate,EstimatedDeliveryDate,Status")]*/ ShippingOrder shippingOrder)
        {
            bool saveStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var respones = await httpClient.PostAsJsonAsync(Const.API + "ShippingOrders/" , shippingOrder))
                    {
                        if (respones.IsSuccessStatusCode)
                        {
                            var content = await respones.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                saveStatus = true;
                            }
                            else
                            {
                                saveStatus = false;
                            }
                        }
                    }
                }
            }

            if (saveStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Currency", shippingOrder.PricingId);
                ViewData["ShipMentId"] = new SelectList(_context.ShipMents, "Id", "Description", shippingOrder.ShipMentId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", shippingOrder.UserId);
                return View(shippingOrder);
            }
       
        }

        // GET: ShippingOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingOrder = await _context.ShippingOrders.FindAsync(id);
            if (shippingOrder == null)
            {
                return NotFound();
            }
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Currency", shippingOrder.PricingId);
            ViewData["ShipMentId"] = new SelectList(_context.ShipMents, "Id", "Description", shippingOrder.ShipMentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", shippingOrder.UserId);
            return View(shippingOrder);
        }

        // POST: ShippingOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PricingId,ShipMentId,AdressTo,PhoneNumber,TotalPrice,Description,OrderDate,ShippingDate,EstimatedDeliveryDate,Status")] ShippingOrder shippingOrder)
        {
            if (id != shippingOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingOrderExists(shippingOrder.Id))
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
            ViewData["PricingId"] = new SelectList(_context.Pricings, "Id", "Currency", shippingOrder.PricingId);
            ViewData["ShipMentId"] = new SelectList(_context.ShipMents, "Id", "Description", shippingOrder.ShipMentId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Address", shippingOrder.UserId);
            return View(shippingOrder);
        }
 

        // POST: ShippingOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    using (var respones = await httpClient.DeleteAsync(Const.API + "ShippingOrders/" + id))
                    {
                        if (respones.IsSuccessStatusCode)
                        {
                            var content = await respones.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                deleteStatus = true;
                            }
                            else
                            {
                                deleteStatus = false;
                            }
                        }
                    }
                }
            }

            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Delete));
            }
        }

        private bool ShippingOrderExists(int id)
        {
            return _context.ShippingOrders.Any(e => e.Id == id);
        }
    }
}
