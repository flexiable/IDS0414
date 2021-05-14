using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace websocket_client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parallel.For(1000, 1100, P =>
              {

            using (var ws = new WebSocket($"ws://localhost:{P}/laputa"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("laputa says: " + e.Data);

                ws.Connect();
                ws.Send(P+"-balus");
            }
              });
            }
            catch (System.Net.Sockets.SocketException ee)
            {

                Console.WriteLine(ee.Message);
            }
            catch (System.AggregateException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

                Console.ReadKey();
            }
        }
    }
}
