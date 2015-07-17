using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Web.Modules
{
    public class BaseModule : NancyModule
    {
        public BaseModule()
            : base()
        {

        }

        public BaseModule(string modulePath)
            : base("/api" + modulePath)
        {
        }

        /// <summary>
        /// Ließt den Request-Stream aus und Deserialisiert in den richtigen Typen anhand von "$type"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        //protected T GetRequestData<T>()
        //{
        //    Request.Body.Position = 0;
        //    var jsonString = new StreamReader(this.Request.Body).ReadToEnd();
        //    T message = JsonConvert.DeserializeObject<T>(jsonString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.All });

        //    return message;
        //}

        protected T FormData<T>(string key)
        {
            var dynDict = this.Request.Form as DynamicDictionary;
            if (dynDict != null)
            {
                return dynDict.ContainsKey(key) ? (T)dynDict[key] : default(T);
            }
            else
            {
                return default(T);
            }
        }

        // http://blog.bekijkhet.com/2012/04/c-nancy-selfhosting-webapplication-with.html
        private void SetupModelDefaults()
        {
            //var sw = new Stopwatch();
            //Before.AddItemToStartOfPipeline(ctx =>
            //{
            //    sw.Start();
            //    Console.WriteLine("Start: ");
            //    return null;
            //});
            //// The  Before  pipeline enables you to intercept the request before it is passed to the appropriate route handler. This gives you a couple of possibilities such as modifying parts of the request or even prematurely aborting the request by returning a response that will be sent back to the caller.
            //Before.AddItemToEndOfPipeline(ctx =>
            //{
            //    //UserModel = new UserModel();
            //    //UserModel.IsAuthenticated = (ctx.CurrentUser == null);

            //    //A return value of  null  means that no action is taken by the hook and that the request should be processed by the matching route.
            //    return null;
            //});

            //// The  After  pipeline is defined using the same syntax as the  Before  pipeline, and the passed in parameter is also the current  NancyContext . The difference is that the hook does not return a value.
            //After.AddItemToEndOfPipeline(ctx =>
            //{
            //    var now = DateTime.Now;
            //    ctx.Response.WithCookie("lastvisit", now.ToShortDateString() + " " + now.ToShortTimeString(), now.AddYears(1));

            //    sw.Stop();
            //    Console.WriteLine("Stop: " + sw.ElapsedMilliseconds);
            //});
        }
    }
}
