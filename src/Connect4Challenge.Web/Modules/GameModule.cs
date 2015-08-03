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
    public class GameModule : BaseModule
    {
        public GameModule()
            : base("/game")
        {
            Get["/"] = _ =>
            {
                var p = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "tests", "Connect4Challenge.TestImplementation", "bin", "Debug", "Connect4Challenge.TestImplementation.dll");
                var o = Connect4Challenge.Bootstrapper.getSubClassFromAssembly<ConnectFour>(p);
                var res = "Name: " + o.Name + " Move: " + o.Move(new int[7, 6]);
                return res;
            };
        }
    }
}
