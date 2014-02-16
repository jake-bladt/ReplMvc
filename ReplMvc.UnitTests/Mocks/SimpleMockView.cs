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

        public SimpleMockView(Stack<String> requestStack)
        {
            RequestStack = requestStack;
            ResponseStack = new Stack<string>();
        }

        public string GetInput()
        {
            if (0 == RequestStack.Count)
            {
                return "quit";
            }
            else
            {
                return RequestStack.Pop();
            }
        }

        public string GetInput(string prompt)
        {
            ResponseStack.Push(prompt);
            return RequestStack.Pop();
        }

        public void SendMessage(string message)
        {
            ResponseStack.Push(message);
        }
    }
}
