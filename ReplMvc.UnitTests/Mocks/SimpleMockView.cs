using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReplMvc;
using ReplMvc.Views;

namespace ReplMvc.UnitTests.Mocks
{
    public class SimpleMockView : IView
    {
        public Stack<String> ResponseStack { get; protected set; }
        public Stack<String> RequestStack { get; protected set; }

        public SimpleMockView(Stack<String> responseStack)
        {
            RequestStack = new Stack<string>();
            ResponseStack = responseStack;
        }

        public string GetInput()
        {
            return ResponseStack.Pop();
        }

        public string GetInput(string prompt)
        {
            RequestStack.Push(prompt);
            return ResponseStack.Pop();
        }

        public void SendMessage(string message)
        {
            RequestStack.Push(message);
        }
    }
}
