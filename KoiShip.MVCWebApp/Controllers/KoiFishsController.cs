using KoiShip.Common;
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
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.API + "KoiFishs"))
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
            return View();
        }


        // POST: KoiFish/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CategoryId,Name,Weight,Age,ColorPattern,Price,Description,UrlImg,Status")] KoiFish koiFish)
        {
            using (var httpClient = new HttpClient())
            {
                koiFish.CategoryId = koiFish.CategoryId;    
                koiFish.UserId = 1;
                koiFish.CategoryId = 1;
                var jsonContent = JsonConvert.SerializeObject(koiFish);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(Const.API + "KoiFishs", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(koiFish);
        }

        // GET: KoiFish/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: KoiFish/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CategoryId,Name,Weight,Age,ColorPattern,Price,Description,UrlImg,Status")] KoiFish koiFish)
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
    }
}
