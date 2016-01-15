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
            Post["/UploadAssembly"] = _ =>
            {
                //var uploadModel = this.FormData<UploadModel>("data");

                //TODO: remove code duplicates

                //Player 1
                var pPath = "C:\\Temp\\" + base.Request.Files.ElementAt(0).Name;
                Stream playerAssemblyStream = base.Request.Files.ElementAt(0).Value;
                using (var playerFileStream = File.OpenWrite(pPath))
                {
                    playerAssemblyStream.CopyTo(playerFileStream);
                }
                var player = Bootstrapper.getSubClassFromAssembly<ConnectFour>(pPath);

                //Player 2
                var ePath = "C:\\Temp\\" + base.Request.Files.ElementAt(1).Name;
                Stream enemyAssemblyStream = base.Request.Files.ElementAt(1).Value;
                using (var enemyFileStream = File.OpenWrite(ePath))
                {
                    enemyAssemblyStream.CopyTo(enemyFileStream);
                }
                var enemy = Bootstrapper.getSubClassFromAssembly<ConnectFour>(ePath);
               
                var res = RunTime.gameInterOp(player, enemy, 4, new int[7, 6]).ToArray();
                
                return res;
            };
        }
    }
}
