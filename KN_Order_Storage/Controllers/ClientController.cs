﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KN_Order_Storage;
using KN_Order_Storage.Logic.Interfaces;
using KN_Order_Storage.Logic.Implementor;
using KN_Order_Storage.Models;
using KN_Order_Storage.Common;
using LinqKit;


namespace KN_Order_Storage.Controllers
{
    public class ClientController : Controller
    {
        private IOptionsProvider OptionsProvider = new OptionsProvider();
        private KN_Order_Storage_Entities db = new KN_Order_Storage_Entities();

        // GET: /Client/
        public ActionResult Index(string source, string reachstatus, string orderstatus, string dp1, string dp2, string criteria, string term)
        {
            ViewBag.Source = OptionsProvider.PopulateClientSourceOption(true);

            if (String.IsNullOrEmpty(source))
            {
                return View(db.ct_client.Where(ct => ct.ct_status == WebConstants.cStatusYes).ToList());
            }
            else
            {
                var predicate = PredicateBuilder.True<ct_client>();

                if (source != WebConstants.cSearchAll)
                {
                    predicate = predicate.And(p => p.ct_client_source.Contains(source));
                }
                if (reachstatus != WebConstants.cSearchAll)
                {
                    predicate = predicate.And(p => p.ct_reach_status.Contains(reachstatus));
                }
                if (orderstatus != WebConstants.cSearchAll)
                {
                    predicate = predicate.And(p => p.ct_order_status.Contains(orderstatus));
                }
                if (dp1 != String.Empty)
                {

                    predicate = predicate.And(p => p.ct_wedding_day >= DateTime.Parse(dp1));
                }
                if (dp2 != String.Empty)
                {

                    predicate = predicate.And(p => p.ct_wedding_day <= DateTime.Parse(dp2));
                }
                if (criteria == "name" && term != String.Empty)
                {
                    predicate = predicate.And(p => p.ct_client_name.Contains(term));
                }
                if (criteria == "tel" && term != String.Empty)
                {
                    predicate = predicate.And(p => p.ct_client_tel.Contains(term));
                }
                if (criteria == "qq" && term != String.Empty)
                {
                    predicate = predicate.And(p => p.ct_client_qq.Contains(term));
                }
                return View(db.ct_client.AsExpandable().Where(predicate).ToList());
            }
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
            ViewBag.Source = OptionsProvider.PopulateClientSourceOption(false);

            ct_client client = new ct_client();
            client.us_user_name = "admin";
            client.ct_status = WebConstants.cStatusYes;
            client.ct_create_time = DateTime.Now;

            return View(client);
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
            ViewBag.Source = OptionsProvider.PopulateClientSourceOption(false);
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
                ViewBag.Source = OptionsProvider.PopulateClientSourceOption(false);
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
            ct_client.ct_status = WebConstants.cStatusNo;
            db.Entry(ct_client).State = EntityState.Modified;
            //db.ct_client.Remove(ct_client);
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
