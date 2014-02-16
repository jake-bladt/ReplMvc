using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReplMvc;
using ReplMvc.Controllers;

namespace ReplMvc.UnitTests.Mocks
{
    class PassthroughController : IController
    {
        public Dictionary<string, Func<string[], ActionResult>> GetCommandActions()
        {
            return new Dictionary<string, Func<string[], ActionResult>> { { "call-action", this.CallAction } };
        }

        public ActionResult CallAction(string[] args)
        {
            return new ActionResult(args, true, false);
        }

        public string Name
        {
            get { return "Passthrough controller"; }
        }
    }
}
