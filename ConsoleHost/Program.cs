using EvalServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //  ServiceHost host = new ServiceHost(typeof(EvalService));
            /* host.AddServiceEndpoint(typeof(IEvalService), new BasicHttpBinding(), "http://localhost:8080/evals/basic");
             host.AddServiceEndpoint(typeof(IEvalService), new WSHttpBinding(), "http://localhost:8080/evals/ws");
             host.AddServiceEndpoint(typeof(IEvalService), new NetTcpBinding(), "net.tcp://localhost:8081/evals");
             */
            /*  try
              {
                  host.Open();
                  PrintServiceInfo(host);
                  Console.ReadLine();
                  host.Close();
              }
              catch(Exception e)
              {
                  Console.WriteLine(e.Message);
                  host.Abort();
              }*/

            /* ServiceEndpointCollection endpints = MetadataResolver.Resolve(typeof(IEvalService), new EndpointAddress("http://localhost:8733/EvalServiceLibrary/mex"));

              foreach(ServiceEndpoint se in endpints)
              {
                  EvalRefFromService.EvalServiceClient channel = new EvalRefFromService.EvalServiceClient(se.Binding, se.Address);
                  try
                  {
                      Eval eval = new Eval() { Submitter = "ressy", Timesent = DateTime.Now, Comments = "Preston here!" };
                      //  channel.SubmitEval(eval);
                      channel.GetEvalsCompleted += Channel_GetEvalsCompleted;
                      channel.SubmitEvals("Preston", "HEllo there agaub", DateTime.Now);                  
                      channel.GetEvalsAsync();
                      Console.WriteLine("Waiting....");                  

                  }
                  catch (FaultException fe)
                  {
                      Console.WriteLine("FaultException handler: {0}", fe.GetType());
                      channel.Abort();
                  }
                  catch (CommunicationException ce)
                  {
                      Console.WriteLine("CommunicationException handler: {0}", ce.GetType());
                      channel.Abort();
                  }
                  catch (TimeoutException te)
                  {
                      Console.WriteLine("TimeoutException handler: {0}", te.GetType());
                      channel.Abort();
                  }
              }

              Console.ReadLine();*/

            EvalRefFromService.EvalServiceClient client = new EvalRefFromService.EvalServiceClient();
            client.ClientCredentials.Windows.ClientCredential.UserName = "PReston";
            client.SubmitEval(new Eval() { Submitter = "Preston" });


        }

        private static void Channel_GetEvalsCompleted(object sender, EvalRefFromService.GetEvalsCompletedEventArgs e)
        {
          
            Console.WriteLine("Number of evals: {0}", e.Result.Count());
            Console.ReadLine();
        }

        public static void PrintServiceInfo(ServiceHost host)
        {
            Console.WriteLine("{0} is up and running with these endpoints:", host.Description.ServiceType);
            foreach(ServiceEndpoint se in host.Description.Endpoints )
            {
                Console.WriteLine(se.Address);
            }
        }
    }
}
