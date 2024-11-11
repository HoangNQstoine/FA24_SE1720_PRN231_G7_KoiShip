using KoiShip.Common;
using KoiShip.MVCWebApp.DTO.Request;
using KoiShip.Service.Base;
using KoiShip_DB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiShip.MVCWebApp.Controllers
{
    public class ShippingOrdersController : Controller
    {
        // GET: ShippingOrders
        public async Task<IActionResult> Index(string? phoneNumber, int? totalPrice)
        {
            List<ShippingOrder> orders = new List<ShippingOrder>();

            using (var httpClient = new HttpClient())
            {
                // Build the query parameters if search criteria are provided
                var query = $"ShippingOrders?phoneNumber={phoneNumber}&totalPrice={totalPrice}";

                using (var response = await httpClient.GetAsync(Const.API + query))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            orders = JsonConvert.DeserializeObject<List<ShippingOrder>>(result.Data.ToString());
                        }
                    }
                }
            }

            return View(orders);
        }

        //public async Task<IActionResult> Index()
        //{
        //    List<ShippingOrder> orders = new List<ShippingOrder>();

        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync(Const.API + "ShippingOrders"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                var result = JsonConvert.DeserializeObject<BusinessResult>(content);

        //                if (result != null && result.Data != null)
        //                {
        //                    orders = JsonConvert.DeserializeObject<List<ShippingOrder>>(result.Data.ToString());
        //                }
        //            }
        //        }
        //    }

        //    return View(orders);
        //}

        // GET: ShippingOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShippingOrder shippingOrder = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "ShippingOrders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            shippingOrder = JsonConvert.DeserializeObject<ShippingOrder>(result.Data.ToString());
                        }
                    }
                }
            }

            if (shippingOrder == null)
            {
                return NotFound();
            }

            return View(shippingOrder);
        }

        // GET: ShippingOrders/Create
        // GET: ShippingOrders/Create
        public async Task<IActionResult> Create()
        {
            // Fetch the data for the dropdowns
            var pricingList = await GetPricingList();
            var shipMentList = await GetShipMentList();
            var userList = await GetUserList();

            // Populate ViewData with the dropdown lists
            ViewData["PricingId"] = new SelectList(pricingList, "Id", "Currency");
            ViewData["ShipMentId"] = new SelectList(shipMentList, "Id", "Description");
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName");

            return View();
        }

        // POST: ShippingOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShippingOrderRequest shippingOrder)
        {
            // Fetch the data again for the dropdowns to preserve selection after post
            var pricingList = await GetPricingList();
            var shipMentList = await GetShipMentList();
            var userList = await GetUserList();

            // Set ViewData for dropdowns to maintain the selected value after postback
            ViewData["PricingId"] = new SelectList(pricingList, "Id", "Currency", shippingOrder.PricingId);
            ViewData["ShipMentId"] = new SelectList(shipMentList, "Id", "Description", shippingOrder.ShipMentId);
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", shippingOrder.UserId);

            if (ModelState.IsValid)
            {
                bool saveStatus = false;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.API + "ShippingOrders", shippingOrder))
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

            // Return the same view with populated dropdowns if the model is invalid
            return View(shippingOrder);
        }


        // GET: ShippingOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ShippingOrder shippingOrder = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "ShippingOrders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            shippingOrder = JsonConvert.DeserializeObject<ShippingOrder>(result.Data.ToString());
                        }
                    }
                }
            }

            if (shippingOrder == null)
            {
                return NotFound();
            }

            var pricingList = await GetPricingList();
            var shipMentList = await GetShipMentList();
            var userList = await GetUserList();

            ViewData["PricingId"] = new SelectList(pricingList, "Id", "Currency", shippingOrder.PricingId);
            ViewData["ShipMentId"] = new SelectList(shipMentList, "Id", "Description", shippingOrder.ShipMentId);
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", shippingOrder.UserId);

            return View(shippingOrder);
        }

        // POST: ShippingOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PricingId,ShipMentId,AdressTo,PhoneNumber,TotalPrice,Description,OrderDate,ShippingDate,EstimatedDeliveryDate,Status")] ShippingOrderEdit shippingOrder)
        {
            if (id != shippingOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool updateStatus = false;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PutAsJsonAsync(Const.API + "ShippingOrders/" + id, shippingOrder))
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

            var pricingList = await GetPricingList();
            var shipMentList = await GetShipMentList();
            var userList = await GetUserList();

            ViewData["PricingId"] = new SelectList(pricingList, "Id", "Currency", shippingOrder.PricingId);
            ViewData["ShipMentId"] = new SelectList(shipMentList, "Id", "Description", shippingOrder.ShipMentId);
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", shippingOrder.UserId);

            return View(shippingOrder);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "ShippingOrders/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<ShippingOrder>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }
        // POST: ShippingOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool deleteStatus = false;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(Const.API + "ShippingOrders/" + id))
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

        // Helper methods to get data for dropdowns
        private async Task<List<Pricing>> GetPricingList()
        {
            var pricingList = new List<Pricing>();
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
                            pricingList = JsonConvert.DeserializeObject<List<Pricing>>(result.Data.ToString());
                        }
                    }
                }
            }
            return pricingList;
        }

        private async Task<List<ShipMent>> GetShipMentList()
        {
            var shipMentList = new List<ShipMent>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Shipments"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            shipMentList = JsonConvert.DeserializeObject<List<ShipMent>>(result.Data.ToString());
                        }
                    }
                }
            }
            return shipMentList;
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
