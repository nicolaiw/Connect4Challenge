using Nancy;
using Nancy.Diagnostics.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Web.Modules
{
    public class GameModule : BaseModule
    {
        public GameModule()
        {
            Get["/"] = x =>
            {
                return View["Index"];
            };
        }
    }
}
