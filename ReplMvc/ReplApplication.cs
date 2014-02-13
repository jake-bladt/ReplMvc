using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReplMvc.Exceptions;

using ReplMvc.Controllers;
using ReplMvc.Views;

namespace ReplMvc
{
    public class ReplApplication : IController
    {
        public IView View { get; protected set; }
        public Dictionary<string, Func<String[], ActionResult>> CommandActions { get; protected set; }

        public ReplApplication(IView view = null, IController[] controllers = null)
        {
            // Attempt to register view.
            if (null != view)
            {
                var viewRegisterResult = RegisterView(view);
                if (!viewRegisterResult.Success)
                {
                    throw new ReplAppSetupException(String.Format(
                        "Unable to load the view. The registration process returned the following errors: {0}",
                        viewRegisterResult.Messages));
                }
            }

            // Register this as a controller.
            var selfRegisterResult = RegisterControllers(new IController[] { this });
            if (!selfRegisterResult.Success)
            {
                if (null == View)
                {
                    throw new ReplAppSetupException(String.Format(
                        "Unable to load the application controller and no view available. The registration process returned the following errors: {0}",
                        selfRegisterResult.Messages));
                }
                else
                {
                    DisplayResult(selfRegisterResult);
                }
            }

            // Attempt to register controllers.
            if (null != controllers)
            {
                var controllersRegisterResult = RegisterControllers(controllers);
                if (!controllersRegisterResult.Success)
                {
                    if (null == View)
                    {
                        throw new ReplAppSetupException(String.Format(
                            "Unable to load the controllers and no view available. The registration process returned the following errors: {0}",
                            controllersRegisterResult.Messages));
                    }
                    else
                    {
                        DisplayResult(controllersRegisterResult);   
                    }
                }
            }
        }

        public virtual void Repl()
        {
            var lastResult = new ActionResult(null, true, false);
            while (!lastResult.IsTerminalAction)
            {
                var commandLine = View.GetInput();
                if (!String.IsNullOrEmpty(commandLine))
                {
                    var commandParts = commandLine.Split(' ');
                    var command = commandParts[0];

                    Func<String[], ActionResult> action = CommandActions.ContainsKey(command) ? CommandActions[command] : null;
                    if (null == action)
                    {
                        View.SendMessage(String.Format("{0} is not a recognized command.", command));
                        View.SendMessage("Valid commands are:");
                        DisplayResult(GetValidCommands(null));
                    }
                    else
                    {
                        string[] args = null;
                        if (commandParts.Length > 1) Array.Copy(commandParts, 1, args, 0, commandParts.Length - 1);
                        lastResult = action.Invoke(args);
                        DisplayResult(lastResult);
                    }
                }
            }
        }

        public virtual ActionResult GetValidCommands(String[] args)
        {
            var cmdNames = (from key in CommandActions.Keys orderby key select key).ToArray();
            return new ActionResult(cmdNames, true, false);
        }

        public virtual ActionResult RegisterView(IView view)
        {
            if (View == null)
            {
                View = view;
                return new ActionResult(null, true);
            }
            else
            {
                return new ActionResult( new String[] { "View already registered. ReplApplication only supports a single view." }, false, true);
            }
        }

        public virtual ActionResult RegisterControllers(IController[] controllers)
        {
            foreach (var controller in controllers)
            {
                var commands = controller.GetCommandActions();
                foreach (var command in commands)
                {
                    if (CommandActions.ContainsKey(command.Key))
                    {
                        var msgs = new string[] 
                        { String.Format("The controller {0} attempted to register a command called {1}, but that command was already taken.",
                            controller.Name, command.Key)};
                        return new ActionResult(msgs, false, true);
                    }
                    CommandActions[command.Key] = command.Value;
                }
            }
            return new ActionResult(null, true);
        }

        public virtual void DisplayResult(ActionResult ar)
        {
            if (null == View)
            {
                throw new NoViewRegisteredException("Cannot display action result. No view registered.");
            }

            View.SendMessage(ar.Success ? "OK" : "Error.");
            if (null != ar.Messages)
            {
                for (int i = 0; i < ar.Messages.Length; i++)
                {
                    View.SendMessage(ar.Messages[i]);
                }
            }
        }

        public virtual Dictionary<string, Func<string[], ActionResult>> GetCommandActions()
        {
            return new Dictionary<string, Func<string[], ActionResult>>()
            {
                { "list", this.GetValidCommands },
                { "quit", args => { return new ActionResult(null, true, true); } }
            };
        }

        public string Name
        {
            get { return "Application controller."; }
        }
    }
}
