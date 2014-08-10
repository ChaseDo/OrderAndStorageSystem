using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KN_Order_Storage;
using KN_Order_Storage.Logic.Interfaces;
using KN_Order_Storage.Logic.Implementor;
using KN_Order_Storage.Common;

namespace KN_Order_Storage.Controllers
{
    public class OrderController : Controller
    {
        private IOptionsProvider OptionsProvider = new OptionsProvider();
        private KN_Order_Storage_Entities db = new KN_Order_Storage_Entities();

        // GET: /Order/
        public ActionResult Index()
        {
            var or_order = db.or_order.Include(o => o.ct_client).Include(o => o.in_inventory);
            return View(or_order.ToList());
        }

        // GET: /Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            or_order or_order = db.or_order.Find(id);
            if (or_order == null)
            {
                return HttpNotFound();
            }
            return View(or_order);
        }

        // GET: /Order/Create
        public ActionResult Create()
        {
            ViewBag.Source = OptionsProvider.PopulateClientSourceOption(false);

            ct_client client = new ct_client();
            client.ct_status = WebConstants.cStatusYes;
            client.us_user_name = "admin";
            client.ct_create_time = DateTime.Now;

            or_order order = new or_order();
            order.or_status = WebConstants.cStatusYes;
            order.or_from = "城西";
            order.ct_client = client;

            return View(order);
        }

        // POST: /Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "or_order_id,ct_client_id,or_order_type,in_inventory_id,or_status,or_from,or_order_time,or_delivered_time,or_style_id,or_style_name,or_handled_by,or_price,or_amount,or_remark,or_s_shoulder,or_s_chest,or_s_waist,or_s_buttocks,or_s_stature,or_s_length,or_s_shoes,or_order_status,or_order_no")] or_order or_order)
        {
            if (ModelState.IsValid)
            {
                db.ct_client.Add(or_order.ct_client);
                db.or_order.Add(or_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ct_client_id = new SelectList(db.ct_client, "ct_client_id", "ct_client_source", or_order.ct_client_id);
            ViewBag.in_inventory_id = new SelectList(db.in_inventory, "in_inventory_id", "in_inventory_type", or_order.in_inventory_id);
            return View(or_order);
        }

        // GET: /Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            or_order or_order = db.or_order.Find(id);
            if (or_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ct_client_id = new SelectList(db.ct_client, "ct_client_id", "ct_client_source", or_order.ct_client_id);
            ViewBag.in_inventory_id = new SelectList(db.in_inventory, "in_inventory_id", "in_inventory_type", or_order.in_inventory_id);
            return View(or_order);
        }

        // POST: /Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "or_order_id,ct_client_id,or_order_type,in_inventory_id,or_status,or_from,or_order_time,or_delivered_time,or_style_id,or_style_name,or_handled_by,or_price,or_amount,or_remark,or_s_shoulder,or_s_chest,or_s_waist,or_s_buttocks,or_s_stature,or_s_length,or_s_shoes,or_order_status,or_order_no")] or_order or_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(or_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ct_client_id = new SelectList(db.ct_client, "ct_client_id", "ct_client_source", or_order.ct_client_id);
            ViewBag.in_inventory_id = new SelectList(db.in_inventory, "in_inventory_id", "in_inventory_type", or_order.in_inventory_id);
            return View(or_order);
        }

        // GET: /Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            or_order or_order = db.or_order.Find(id);
            if (or_order == null)
            {
                return HttpNotFound();
            }
            return View(or_order);
        }

        // POST: /Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            or_order or_order = db.or_order.Find(id);
            db.or_order.Remove(or_order);
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
