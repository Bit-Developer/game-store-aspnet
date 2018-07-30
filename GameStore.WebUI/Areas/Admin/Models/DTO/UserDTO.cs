using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Areas.Admin.Models.DTO
{
    public class UserDTO
    {
        public String Id { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Membership { get; set; }
    }
}