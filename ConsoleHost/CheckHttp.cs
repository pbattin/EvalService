using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class ServiceInspector : IDispatchMessageInspector, IParameterInspector
    {
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            Console.WriteLine("IParameterInspector.AfterCall called.");
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            Console.WriteLine("IDispatchMessageInspector.AfterReceiveRequest called.");
            return null;
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            Console.WriteLine("IParameterInspector.BeforeCall called.");
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            Console.WriteLine("IDispatchMessageInspector.BeforeSendReply called.");
        }
    }

    class ServiceBehavior :  IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher chDisp in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher epDisp in chDisp.Endpoints)
                {
                    epDisp.DispatchRuntime.MessageInspectors.Add(new ServiceInspector());
                    foreach (DispatchOperation op in epDisp.DispatchRuntime.Operations)
                        op.ParameterInspectors.Add(new ServiceInspector());
                }
            }
            }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Console.WriteLine("IServiceBehavior.Validate called.");
        }
    }

    class HttpExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {

            get

            {

                return typeof(ServiceBehavior);

            }

        }

        protected override object CreateBehavior()

        {

            return new ServiceBehavior();

        }
    }
}
