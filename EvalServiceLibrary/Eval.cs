using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EvalServiceLibrary
{
    [DataContract]
    public class Eval
    {
        [DataMember]
        public string Submitter;
        [DataMember]
        public DateTime Timesent;
        [DataMember]
        public string Comments;
    }

    [ServiceContract]
    public interface IEvalService
    {
        [OperationContract]
        void SubmitEval(Eval eval);
        [OperationContract]
        void SubmitEvals(string Submitter, string Comments, DateTime Timesent);
        [OperationContract]
        List<Eval> GetEvals();
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EvalService : IEvalService
    {
        List<Eval> evals = new List<Eval>();
        public List<Eval> GetEvals()
        {
              return evals;
        }

        public void SubmitEval(Eval eval)
        {
            if(eval.Submitter.Equals("Throw"))
            {
                throw new FaultException("Error within SubmitEval");                
            }
            evals.Add(eval);
        }

        public void SubmitEvals(string _Submitter, string _Comments, DateTime _Timesent)
        {
            Eval eval = new Eval() { Submitter = _Submitter, Comments = _Comments, Timesent = _Timesent };
            evals.Add(eval);
        }
    }
}
