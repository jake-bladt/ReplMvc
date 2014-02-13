using System;
using System.Collections.Generic;

namespace ReplMvc.Controllers
{
    public interface IController
    {
        Dictionary<string, Func<String[], ActionResult>> GetCommandActions();
        string Name { get; }
    }
}
