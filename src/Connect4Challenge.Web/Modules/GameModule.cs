using Connect4Challenge.Interface;
using Connect4Challenge.Web.Models;
using Nancy;
using Nancy.Diagnostics.Modules;
using Nancy.Json;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                //var p = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "tests", "Connect4Challenge.TestImplementation", "bin", "Debug", "Connect4Challenge.TestImplementation.dll");
                var p = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "src", "Connect4Challenge.Bot", "bin", "Debug", "Connect4Challenge.Bot.dll");
                var o = Connect4Challenge.Bootstrapper.getSubClassFromAssembly<ConnectFour>(p);
                var res = "Name: " + o.Name + " Move: " + o.Move(new int[7, 6]);
                return res;
            };

            Post["/UploadAssembly"] = _ =>
            {
                var uploadModel = this.FormData<UploadModel>("data");

                /* Read Stream */
                Stream playerAssemblyStream = base.Request.Files.ElementAt(0).Value;
                Stream enemyAssemblyStream = base.Request.Files.ElementAt(0).Value;

                /* Generate ConnectFour-Class from Stream */
                // TODO: Derzeit nur möglich über einen File-Path die Klasse zu erzeugen. Notwendig ist die Übergabe eines Streams ODER abspeichern der uploaded dll
                //ConnectFour playerAssembly = Connect4Challenge.Bootstrapper.getSubClassFromAssembly<ConnectFour>(playerAssemblyStream);
                ConnectFour enemyAssembly = null;


                switch (uploadModel.BotType)
                {
                    case BotType.None:
                        /* Custom enemy assembly uploaded */
                        enemyAssemblyStream = base.Request.Files.ElementAt(1).Value;
                        // TODO: dito playerAssembly 
                        //enemyAssembly = Connect4Challenge.Bootstrapper.getSubClassFromAssembly<ConnectFour>(enemyAssemblyStream);
                        break;
                    case BotType.Random:
                        /* Bot with random moves */
                        var p = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "src", "Connect4Challenge.Bot", "bin", "Debug", "Connect4Challenge.Bot.dll");
                        enemyAssembly = Connect4Challenge.Bootstrapper.getSubClassFromAssembly<ConnectFour>(p);
                        break;
                    default:
                        break;
                }


                return "ok";
            };
        }
    }
}
