using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MessageInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var head = MessageHeader.CreateHeader("Header", "Eval", "");
           request.Headers.Add(head);
            
            Console.WriteLine("AfterREcieveRequest" + "eval.Submitter");
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("Before send reply");
        }
    }

    public class MessageEndPtBehaviour : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            Console.WriteLine("Insider Apply Dispatch Behaviour");
            MessageInspector inspector = new MessageInspector();
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }
    }

    public class MessageEndPtElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {

            get

            {

                return typeof(MessageEndPtBehaviour);

            }

        }

        protected override object CreateBehavior()

        {

            return new MessageEndPtBehaviour();

        }
    }

}
