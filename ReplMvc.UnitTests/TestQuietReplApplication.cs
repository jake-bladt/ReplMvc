using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ReplMvc;
using ReplMvc.Controllers;
using ReplMvc.Exceptions;
using ReplMvc.UnitTests.Mocks;

namespace ReplMvc.UnitTests
{
    [TestClass]
    public class TestQuietReplApplication
    {

        [TestMethod]
        public void TestQuietCallCommandWithArgs()
        {
            var commandStack = new Stack<String>();
            commandStack.Push("call-action this that");
            var view = new SimpleMockView(commandStack);
            var passthroughController = new PassthroughController();
            var controllers = new IController[] { passthroughController };
            var app = new ReplApplication(view, controllers) { Quieter = true };

            app.Repl();

            Assert.AreEqual(2, view.ResponseStack.Count);
            var missingResponses = new List<String> { "this", "that" };
            while (view.ResponseStack.Count > 0)
            {
                var resp = view.ResponseStack.Pop();
                if (missingResponses.Contains(resp)) missingResponses.Remove(resp);
            }
            Assert.AreEqual(0, missingResponses.Count);
        }

    }
}
