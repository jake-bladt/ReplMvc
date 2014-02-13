using System;

namespace ReplMvc.Views
{
    public class ConsoleView : IView
    {
        public void DisplayResult(ActionResult ar)
        {
            SendMessage(ar.Success ? "OK" : "Error.");
            if (null != ar.Messages)
            {
                for (int i = 0; i < ar.Messages.Length; i++)
                {
                    SendMessage(ar.Messages[i]);
                }
            }
        }

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
