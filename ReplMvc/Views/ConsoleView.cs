using System;

namespace ReplMvc.Views
{
    public class ConsoleView : IView
    {
        public string GetInput()
        {
            return GetInput(String.Empty);
        }

        public string GetInput(string prompt)
        {
            if(!String.IsNullOrEmpty(prompt)) SendMessage(prompt);
            Console.Write("> ");
            return Console.ReadLine();
        }

        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
