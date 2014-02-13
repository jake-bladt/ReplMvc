using System;

namespace ReplMvc
{
    public class ActionResult
    {
        public bool Success { get; set; }
        public bool IsTerminalAction { get; set; }
        public string[] Messages { get; set; }

        public ActionResult(string[] msgs, bool success, bool isTerminal = false)
        {
            Messages = msgs;
            Success = success;
            IsTerminalAction = isTerminal;
        }
    }
}
