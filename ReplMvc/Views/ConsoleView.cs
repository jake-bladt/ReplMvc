using System;

namespace ReplMvc.Views
{
    public class ConsoleView : IView
    {
        public string GetInput()
        {
            return GetInput("Ready");
        }

        public string GetInput(string prompt)
        {
            SendMessage(prompt);
            Console.Write("> ");
            return Console.ReadLine();
        }

        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
