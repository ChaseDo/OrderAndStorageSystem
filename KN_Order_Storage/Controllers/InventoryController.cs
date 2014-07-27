using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KN_Order_Storage;

namespace KN_Order_Storage.Controllers
{
    public class InventoryController : Controller
    {
        private KN_Order_Storage_Entities db = new KN_Order_Storage_Entities();

        // GET: /Inventory/
        public ActionResult Index()
        {
            return View(db.in_inventory.ToList());
        }

        // GET: /Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            in_inventory in_inventory = db.in_inventory.Find(id);
            if (in_inventory == null)
            {
                return HttpNotFound();
            }
            return View(in_inventory);
        }

        // GET: /Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="in_inventory_id,in_inventory_type,us_user_name,in_create_time,in_image,in_style_id,in_style_name,in_status,in_color,in_size,in_eur,in_usd,in_cny,in_amount,in_remark")] in_inventory in_inventory)
        {
            if (ModelState.IsValid)
            {
                db.in_inventory.Add(in_inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(in_inventory);
        }

        // GET: /Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            in_inventory in_inventory = db.in_inventory.Find(id);
            if (in_inventory == null)
            {
                return HttpNotFound();
            }
            return View(in_inventory);
        }

        // POST: /Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="in_inventory_id,in_inventory_type,us_user_name,in_create_time,in_image,in_style_id,in_style_name,in_status,in_color,in_size,in_eur,in_usd,in_cny,in_amount,in_remark")] in_inventory in_inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(in_inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(in_inventory);
        }

        // GET: /Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            in_inventory in_inventory = db.in_inventory.Find(id);
            if (in_inventory == null)
            {
                return HttpNotFound();
            }
            return View(in_inventory);
        }

        // POST: /Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            in_inventory in_inventory = db.in_inventory.Find(id);
            db.in_inventory.Remove(in_inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
