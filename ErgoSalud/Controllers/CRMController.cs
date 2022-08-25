using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgoSalud.Controllers
{
    public class CRMController : Controller
    {
        // GET: CRM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Cotizador()
        {
            return View();
        }
    }
}