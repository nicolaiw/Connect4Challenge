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
                //TODO: test getSubClassFromAssemblyPath as well
                //TODO: ExceptionHanlding

                //Player 1
                Stream playerAssemblyStream = base.Request.Files.ElementAt(0).Value;
                ConnectFour player;

                using (var memoryStream = new MemoryStream())
                {
                    playerAssemblyStream.CopyTo(memoryStream);
                    player = Connect4Challenge.Bootstrapper.getSubClassFromAssemblyBytes<ConnectFour>(memoryStream.ToArray());
                }
               
                //Player 2
                Stream enemyAssemblyStream = base.Request.Files.ElementAt(1).Value;
                ConnectFour enemy;

                using (var memoryStream = new MemoryStream())
                {
                    enemyAssemblyStream.CopyTo(memoryStream);
                    enemy = Connect4Challenge.Bootstrapper.getSubClassFromAssemblyBytes<ConnectFour>(memoryStream.ToArray());
                }
               
                var res = RunTime.gameInterOp(player, enemy, 4, new int[7, 6]).ToArray();
                var pitch = RunTime.createPitch(res, 6, 5);
                return pitch.ToString();
            };
        }
    }
}
