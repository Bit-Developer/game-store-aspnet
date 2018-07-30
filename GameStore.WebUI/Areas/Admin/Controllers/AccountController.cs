using GameStore.WebUI.Areas.Admin.Models;
using GameStore.Domain.Infrastructure;
using GameStore.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Domain.Identity;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Areas.Admin.Models.DTO;

namespace GameStore.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : BaseController
    {
        // GET: User
        public ActionResult AppUser()
        {
            var roles = RoleManager.Roles.ToList();
            List<RoleDTO> list = roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name, Description = r.Description }).ToList();
            ViewBag.Roles = list;
            return View();
        }
        public ActionResult AppRole()
        {
            return View();
        }
    }
}