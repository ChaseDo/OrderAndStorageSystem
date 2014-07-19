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
using KN_Order_Storage.Models;


namespace KN_Order_Storage.Controllers
{
    public class ClientController : Controller
    {
        private IOptionsProvider OptionsProvider = new OptionsProvider();

        private KN_Order_Storage_Entities db = new KN_Order_Storage_Entities();

        // GET: /Client/
        public ActionResult Index()
        {
            return View(db.ct_client.ToList());
        }

        // GET: /Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ct_client ct_client = db.ct_client.Find(id);
            if (ct_client == null)
            {
                return HttpNotFound();
            }
            return View(ct_client);
        }

        // GET: /Client/Create
        public ActionResult Create()
        {
            ViewBag.Source = (from x in OptionsProvider.GetClientSourceOption().Sources
                                select new SelectListItem()
                                {
                                    Text = x.Name,
                                    Value = x.Id.ToString()
                                }).ToList();
            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ct_client_id,ct_client_source,us_user_name,ct_create_time,ct_client_name,ct_status,ct_client_tel,ct_client_qq,ct_wedding_day,ct_reach_status,ct_order_status,ct_express,ct_freight,ct_address,ct_remark")] ct_client ct_client)
        {
            if (ModelState.IsValid)
            {
                db.ct_client.Add(ct_client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ct_client);
        }

        // GET: /Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ct_client ct_client = db.ct_client.Find(id);
            if (ct_client == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Source = (from x in OptionsProvider.GetClientSourceOption().Sources
                                  select new SelectListItem()
                                  {
                                      Text = x.Name,
                                      Value = x.Id.ToString()
                                  }).ToList();
            }
            return View(ct_client);
        }

        // POST: /Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ct_client_id,ct_client_source,us_user_name,ct_create_time,ct_client_name,ct_status,ct_client_tel,ct_client_qq,ct_wedding_day,ct_reach_status,ct_order_status,ct_express,ct_freight,ct_address,ct_remark")] ct_client ct_client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ct_client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ct_client);
        }

        // GET: /Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ct_client ct_client = db.ct_client.Find(id);
            if (ct_client == null)
            {
                return HttpNotFound();
            }
            return View(ct_client);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ct_client ct_client = db.ct_client.Find(id);
            db.ct_client.Remove(ct_client);
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
