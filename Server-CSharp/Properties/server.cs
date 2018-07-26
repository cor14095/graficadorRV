using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using Microsoft.CSharp;
using System.Net;

namespace ConsoleApplication1
{
    class serv
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");   // Localhost.
            TcpListener serverSocket = new TcpListener(address, 8081);
            Socket clientSocket = default(Socket);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started"); 

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptSocket();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
                HandleClient client = new HandleClient();
                client.StartClient(clientSocket, Convert.ToString(counter));
            }
        }
    }

   
    public class HandleClient
    {
        Socket clientSocket;
        string clNo;
        public void StartClient(Socket inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(Compile);
            ctThread.Start();
        }

        private void Compile()
        {
            byte[] msg = new byte[1024];
            string data = "";
            string response;
            Byte[] sendBytes;
            
            byte[] msg2 = new byte[1024];
            string data2 = "";
            string response2;
            Byte[] sendBytes2;

            try {
                int k=clientSocket.Receive(msg);
                
                data = System.Text.Encoding.ASCII.GetString(msg);
                data = data.Split('$')[1];
                Console.WriteLine("Receiving Data");
            
                response = "515 confirmed task -- calculate: " + data;
                //Console.WriteLine(response);
                sendBytes = Encoding.ASCII.GetBytes(response);
                // Tell the client that we have started.
                clientSocket.Send(sendBytes);
               
                CodeSnippetCompileUnit compileUnit = new CodeSnippetCompileUnit(data);
                CodeDomProvider provider = new CSharpCodeProvider();
                // Compile the parameters.
                CompilerParameters cParameters = new CompilerParameters();
                // Generate a compiled version of everything.
                CompilerResults results = provider.CompileAssemblyFromDom(cParameters, compileUnit);
                // Get the type for method. 
                Type type = results.CompiledAssembly.GetType("MyType");
                MethodInfo method = type.GetMethod("Evaluate");


                while (data2 != "quit")
                {
                   // Console.WriteLine("________________________");
                    int k2=clientSocket.Receive(msg2);
                
                    data2 = System.Text.Encoding.ASCII.GetString(msg2);
                    int index = data2.IndexOf("~");
                    if (index > 0)
                        data2 = data2.Substring(0, index);
                    //Console.WriteLine(data2);
                    if (data2.IndexOf("quit") > 0 )
                    {
                        Console.WriteLine("done");
                        break;
                    }
                    
                    string data3 = data2.Split('$')[1];
                
                    var ps = data3.Split('@');
               
                    var px = float.Parse(ps[0],
                        System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                    var pz = float.Parse(ps[1],
                        System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                    var py = float.Parse(ps[2],
                        System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                
                    var p = (float) method.Invoke(null, new object[] {px,pz,py });
                
                    response2 = p.ToString();
                    sendBytes2 = Encoding.ASCII.GetBytes(response2);
                    clientSocket.Send(sendBytes2);
                   // Console.WriteLine(" - Responding: " + response2);
                }
                
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> " + ex.ToString());
            }
           
        }
    } 
}