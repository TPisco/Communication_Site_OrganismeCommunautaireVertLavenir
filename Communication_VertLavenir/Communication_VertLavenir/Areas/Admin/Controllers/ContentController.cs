using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Areas.Admin.Controllers
{
    public class ContentController : AdminBaseController
    {
        public ContentController(AppDbContext db) : base(db) { }

        public async Task<IActionResult> Index()
        {
            ViewBag.Settings = await Db.SiteSettings.OrderBy(s => s.Key).ToListAsync();
            ViewBag.Metrics = await Db.ImpactMetrics.OrderBy(m => m.DisplayOrder).ToListAsync();
            ViewBag.Values = await Db.OrgValues.OrderBy(v => v.DisplayOrder).ToListAsync();
            ViewBag.Actions = await Db.KeyActions.OrderBy(a => a.DisplayOrder).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSettings(Dictionary<string, string> settings)
        {
            foreach (var kv in settings)
            {
                var s = await Db.SiteSettings.FirstOrDefaultAsync(x => x.Key == kv.Key);
                if (s == null)
                    Db.SiteSettings.Add(new SiteSetting { Key = kv.Key, Value = kv.Value });
                else
                    s.Value = kv.Value;
            }
            await Db.SaveChangesAsync();
            TempData["Success"] = "Paramètres enregistrés.";
            return RedirectToAction(nameof(Index));
        }

        // ---- Impact metrics ----
        public async Task<IActionResult> EditMetric(int? id)
        {
            var m = id.HasValue ? await Db.ImpactMetrics.FindAsync(id.Value) : new ImpactMetric();
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMetric(ImpactMetric model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Id == 0) Db.ImpactMetrics.Add(model);
            else Db.ImpactMetrics.Update(model);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Métrique enregistrée.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMetric(int id)
        {
            var m = await Db.ImpactMetrics.FindAsync(id);
            if (m != null) { Db.ImpactMetrics.Remove(m); await Db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }

        // ---- Values ----
        public async Task<IActionResult> EditValue(int? id)
        {
            var v = id.HasValue ? await Db.OrgValues.FindAsync(id.Value) : new OrgValue();
            if (v == null) return NotFound();
            return View(v);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditValue(OrgValue model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Id == 0) Db.OrgValues.Add(model);
            else Db.OrgValues.Update(model);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Valeur enregistrée.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var v = await Db.OrgValues.FindAsync(id);
            if (v != null) { Db.OrgValues.Remove(v); await Db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }

        // ---- Key actions ----
        public async Task<IActionResult> EditAction(int? id)
        {
            var a = id.HasValue ? await Db.KeyActions.FindAsync(id.Value) : new KeyAction();
            if (a == null) return NotFound();
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAction(KeyAction model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.Id == 0) Db.KeyActions.Add(model);
            else Db.KeyActions.Update(model);
            await Db.SaveChangesAsync();
            TempData["Success"] = "Action enregistrée.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAction(int id)
        {
            var a = await Db.KeyActions.FindAsync(id);
            if (a != null) { Db.KeyActions.Remove(a); await Db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
