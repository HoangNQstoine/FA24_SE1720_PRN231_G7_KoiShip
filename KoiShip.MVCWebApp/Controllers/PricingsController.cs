using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiShip_DB.Data.Models;
using KoiShip.Common;
using KoiShip.Service.Base;
using Newtonsoft.Json;
using System.Text;

namespace KoiShip.MVCWebApp.Controllers
{
    public class PricingsController : Controller
    {
        public PricingsController()
        {
        }

        // GET: Pricings
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Pricings"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Pricing>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<ShippingOrder>());
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Pricings/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Pricing>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }


        // GET: Pricings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pricings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,WeightRange,ShippingMethod,Description,Price,Currency,EffectiveDate,ExpiryDate,Status")] Pricing pricing)
        {
            using (var httpClient = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(pricing);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(Const.API + "Pricings", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(pricing);
        }

        // GET: Pricings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Pricings/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Pricing>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }

        // POST: Pricings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity,WeightRange,ShippingMethod,Description,Price,Currency,EffectiveDate,ExpiryDate,Status")] Pricing pricing)
        {
            if (id != pricing.Id)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(pricing);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(Const.API + "Pricings/" + id, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(pricing);
        }

        // GET: Pricings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Pricings/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Pricing>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }

        // POST: Pricings/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(Const.API + "Pricings/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return NotFound();
        }
    }
}
