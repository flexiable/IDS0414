using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace websocket_server
{
    public class Program
    {
        public volatile static int tsIncre;
      public  static void Main(string[] args)
        {
            try
            {

       
            Parallel.ForEach(Enumerable.Range(1000, 1100).Except(new int[] { 1433 }), ts =>
            {
                tsIncre = ts;
                var wssv = new WebSocketServer("ws://localhost:"+ts);
                wssv.AddWebSocketService<laputa>("/laputa");
                wssv.Start();
            });
            }
            catch (System.Net.Sockets.SocketException ee)
            {

                Console.WriteLine(  ee.Message);
            }
            catch (System.AggregateException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

            Console.ReadKey();
            }
            //wssv.Stop();
        }
    }
    
    public class laputa : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Console.WriteLine($"onOpen:{System.Threading.Thread.CurrentThread.ManagedThreadId}");
            base.OnOpen();
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            var msg = e.Data.Contains("balus")
                        ? "i've been balused already..."+ e.Data
                        : "i'm not available now.";
            Console.WriteLine(e.Data);
            Send(msg);
        }
       
    }
}
