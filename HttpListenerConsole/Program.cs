using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace HttpListenerConsole
{
    internal class Program
    {
        static void Main()
        {
            HttpListener listener = null;
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:8080/");
                listener.Start();
                while (true)
                {
                    Console.WriteLine("waiting...");
                    HttpListenerContext context = listener.GetContext();
                    string msg = "Hello!";
                    context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    using (Stream stream = context.Response.OutputStream)
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(msg);
                        }
                    }
                    Console.WriteLine("msg sent...");
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Status);
            }
        }
    }
}
