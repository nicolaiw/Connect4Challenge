using Connect4Challenge.Web.Authentication;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Json;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Web
{
    public class ApplicationBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var sw = new Stopwatch();
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                sw.Start();
                var row = string.Format("{0} {1} ({2}) {3}", new string('*', 25), "Start", DateTime.Now, new string('*', 25));
                //Console.WriteLine("Start: " + ctx.Request.Url);
                Console.WriteLine(row);
                Console.WriteLine(ctx.Request.Url);
                using (var sr = new StreamReader(ctx.Request.Body))
                {
                    var jsonString = sr.ReadToEnd();
                    Console.WriteLine();
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        Console.WriteLine("Gesendeter jsonString:\n" + jsonString);
                    }
                }
                ctx.Request.Body.Seek(0, SeekOrigin.Begin);

                /*Return NULL: A return value of  null  means that no action is taken by the hook and that the request should be processed by the matching route.
                Return Object: If the interceptor returns a  Response  of its own, the request will never be processed by a route and the response will be sent back to the client.*/
                return null;
            });
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                sw.Stop();
                //Console.WriteLine("Stop: " + sw.ElapsedMilliseconds);
                var row = string.Format("{0} {1} ({2}) {3}", new string('*', 25), "Stop", sw.ElapsedMilliseconds, new string('*', 25));
                Console.WriteLine(row);

                /*The  After  hooks does not have any return value because one has already been produced by the appropriate route.
                Instead you get the option to modify (or completely replace) the existing response by accessing the  Response  property of the  NancyContext  that is passed in.*/
                sw.Reset();
            });
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                Console.WriteLine("Folgender Fehler aufgetaucht:\nSOURCE: " + ex.Source + "\nMESSAGE: " + ex.Message + "\nINNER EXCEPTION: " + ex.InnerException + "\nSTACKTRACE: " + ex.StackTrace);

                return null;
                //return ctx.Response.AsError(ex.ToString());
            });

            base.ApplicationStartup(container, pipelines);
            //CookieBasedSessions.Enable(pipelines);

            //NOTE: If you encounter the Nancy.Json.JsonSettings.MaxJsonLength Exceeded error because your payload is too high, change that limit in your Bootstrapper in  ApplicationStartup  to be  Nancy.Json.JsonSettings.MaxJsonLength = int.MaxValue; 
            JsonSettings.MaxJsonLength = int.MaxValue;

            var formsAuthConfiguration =
            new FormsAuthenticationConfiguration()
            {
                // Redirect beim Zugriff auf authentifizierungspflichtigen Controller
                //RedirectUrl = "~/login",
                DisableRedirect = true,
                UserMapper = container.Resolve<IUserMapper>()
            };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<IUserMapper, UserMapper>();
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/", @"Content"));
            base.ConfigureConventions(nancyConventions);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //container.Register<JsonSerializer, CustomJsonSerializer>(); //TODO: Behebt bei bool Datapoints, dass die Keys der POssibleValues mit uppercase beginnen. Noch zu prüfen ob das keine anderen Asuwirkungen hat...
        }
    }
}
