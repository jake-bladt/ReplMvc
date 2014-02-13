using System;

namespace ReplMvc.Views
{
    public interface IView
    {
        string GetInput(string prompt);
        void SendMessage(string message);
    }
}
