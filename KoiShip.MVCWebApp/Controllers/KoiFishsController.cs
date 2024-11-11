using KoiShip.Common;
using KoiShip.MVCWebApp.DTO.Request;
using KoiShip.Service.Base;
using KoiShip_DB.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace KoiShip.MVCWebApp.Controllers
{
    public class KoiFishsController : Controller
    {
        public KoiFishsController()
        {
        }

        // GET: KoiFish
        public async Task<IActionResult> Index(string? Name, int? Age)
        {
            using (var httpClient = new HttpClient())
            {
                var query = $"KoiFishs?Name={Name}&Age={Age}";

                using (var response = await httpClient.GetAsync(Const.API + query))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<KoiFish>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<KoiFish>());
        }

        public async Task<IActionResult> Details(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "KoiFishs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<KoiFish>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }

        // GET: KoiFish/Create
        public async Task<IActionResult> Create(int id)
        {
            var categoryList = await GetCategoryList();
            var userList = await GetUserList();
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name");
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName");
            return View();
        }


        // POST: KoiFish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CategoryId,Name,Weight,Age,ColorPattern,Price,Description,UrlImg,Status")] KoiFishCreate koiFish)
        {
            if (ModelState.IsValid)
            {
                bool saveStatus = false;

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(Const.API + "KoiFishs", koiFish))
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

            var categoryList = await GetCategoryList();
            var userList = await GetUserList();
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name", koiFish.CategoryId);
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", koiFish.UserId);
            return View(koiFish);
        }

        // GET: KoiFish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var koiFish = new KoiFish();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "KoiFishs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            koiFish = JsonConvert.DeserializeObject<KoiFish>(result.Data.ToString());
                        }
                    }
                }
            }
            var categoryList = await GetCategoryList();
            var userList = await GetUserList();
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name", koiFish.CategoryId);
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", koiFish.UserId);
            return View(koiFish);
        }

        // POST: KoiFish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CategoryId,Name,Weight,Age,ColorPattern,Price,Description,UrlImg,Status")] KoiFishEdit koiFish)
        {
            if (id != koiFish.Id)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(koiFish);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(Const.API + "KoiFishs/" + id, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            var categoryList = await GetCategoryList();
            var userList = await GetUserList();
            ViewData["CategoryId"] = new SelectList(categoryList, "Id", "Name", koiFish.CategoryId);
            ViewData["UserId"] = new SelectList(userList, "Id", "UserName", koiFish.UserId);

            return View(koiFish);

        }

        // GET: KoiFish/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "KoiFishs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<KoiFish>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return NotFound();
        }

        // POST: KoiFish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(Const.API + "KoiFishs/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return NotFound();
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

        private async Task<List<Category>> GetCategoryList()
        {
            var userList = new List<Category>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "Categorys"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            userList = JsonConvert.DeserializeObject<List<Category>>(result.Data.ToString());
                        }
                    }
                }
            }
            return userList;
        }
    }
}
