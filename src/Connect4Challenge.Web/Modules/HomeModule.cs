using Connect4Challenge.Interface;
using Nancy;
using Nancy.Diagnostics.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Web.Modules
{
    public class HomeModule : BaseModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                return View["Index"];
            };
        }
    }
}
