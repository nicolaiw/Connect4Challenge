using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Challenge.Web
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Uri uri = new Uri("http://localhost:4444/Connect4Challenge/");
                Uri uri = new Uri("http://localhost:4444/");
                using (var nancyHost = new NancyHost(new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true } }, uri))
                {
                    Console.WriteLine("Nancy now listening - navigating to " + uri.OriginalString);
                    nancyHost.Start();
                    Console.WriteLine("Press enter to stop");
                    Console.ReadKey();
                    nancyHost.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Stopped. Good bye!");
            Console.ReadKey();
        }
    }
}
