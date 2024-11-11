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
using KoiShip.MVCWebApp.DTO.Request;

namespace KoiShip.MVCWebApp.Controllers
{
    public class ShipMentsController : Controller
    {
        private async Task<ShipMent> GetDetailAsync(int id)
        {
            ShipMent shipMent = null; // Initialize as null to indicate when fetching fails.
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(Const.API + "ShipMents/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                    if (result != null && result.Data != null)
                    {
                        shipMent = JsonConvert.DeserializeObject<ShipMent>(result.Data.ToString());
                    }
                }
            }
            return shipMent;
        }

        public ShipMentsController()
        {
        }

        // GET: ShipMents
        public async Task<IActionResult> Index()
        {
            List<ShipMent> ShipMents = new List<ShipMent>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "ShipMents"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            ShipMents = JsonConvert.DeserializeObject<List<ShipMent>>(result.Data.ToString());
                        }
                    }
                }
            }

            return View(ShipMents);
        }

        // GET: ShipMents/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var shipMent= await GetDetailAsync(id);
            if (shipMent == null)
            {
                return NotFound();
            }

            return View(shipMent);
        }

        // GET: ShipMents/Create
        public IActionResult Create()
        {
            var userList = GetUserList();
            ViewData["UserId"] = new SelectList(userList.Result, "Id", "UserName");
            return View();
        }

        // POST: ShipMents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Vehicle,EstimatedArrivalDate,StartDate,EndDate,HealthCheck,Description,Weight,Status")] ShipMentCreate shipMent)
        {
            if (ModelState.IsValid)
            {
                bool saveStatus = false;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.API + "ShipMents", shipMent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                saveStatus = true;
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
                    // You can log the error or show an error message here
                    ModelState.AddModelError(string.Empty, "Failed to save the shipping order.");
                }
            }

            var userList = await GetUserList();
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", shipMent.UserId);
            return View(shipMent);
        }

        // GET: ShipMents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipMent = await GetDetailAsync(id.Value);

            var userList = await GetUserList();
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", shipMent.UserId);

            if (shipMent == null)
                return NotFound();
            return View(shipMent);
        }

        // POST: ShipMents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Vehicle,EstimatedArrivalDate,StartDate,EndDate,HealthCheck,Description,Weight,Status")] ShipMentEdit shipMent)
        {
            if (id != shipMent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool updateStatus = false;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.API + "ShipMents/" + id, shipMent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                            if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                            {
                                updateStatus = true;
                            }
                        }
                    }
                }

                if (updateStatus)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            var userList = await GetUserList();
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", shipMent.UserId);
            return View(shipMent);
        }

        // GET: ShipMents/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var shipMent = await GetDetailAsync(id);
            if (shipMent == null)
                return NotFound();
            return View(shipMent);
        }

        // POST: ShipMents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(Const.API + "ShipMents/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Status == Const.SUCCESS_DELETE_CODE)
                        {
                            deleteStatus = true;
                        }
                    }
                }
            }

            if (deleteStatus)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Delete), new { id });
        }

        private async Task<List<User>> GetUserList()
        {
            var userList = new List<User>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Users"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            userList = JsonConvert.DeserializeObject<List<User>>(result.Data.ToString());
                        }
                    }
                }
            }
            return userList;
        }
    }
}
