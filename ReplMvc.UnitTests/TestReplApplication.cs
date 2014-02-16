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
    public class TestReplApplication
    {
        [TestMethod]
        public void TestInitializeEmptyReplApplication()
        {
            var app = new ReplApplication();
            Assert.AreEqual(2, app.CommandActions.Count);  // built-in commands from ReplApplication.
            Assert.IsNull(app.View);
        }

        [TestMethod]
        public void TestInitializeWithViewAndController()
        {
            var view = new SimpleMockView(new Stack<String>());
            var passthroughController = new PassthroughController();
            var controllers = new IController[] { passthroughController };
            var app = new ReplApplication(view, controllers);

            Assert.AreEqual(3, app.CommandActions.Count);  // 2 from ReplApplication + 1 from passthrough controller.
            Assert.AreEqual(view, app.View);
        }

        [TestMethod]
        public void TestCallCommandWithNullArgs()
        {
            var commandStack = new Stack<String>();
            commandStack.Push("call-action");
            var view = new SimpleMockView(commandStack);
            var passthroughController = new PassthroughController();
            var controllers = new IController[] { passthroughController };
            var app = new ReplApplication(view, controllers);

            app.Repl();

            // Responses should only be two OKs.
            Assert.AreEqual(2, view.ResponseStack.Count);
            while (view.ResponseStack.Count > 0)
            {
                Assert.AreEqual("OK", view.ResponseStack.Pop());
            }
        }


        [TestMethod]
        public void TestCallCommandWithArgs()
        {
            var commandStack = new Stack<String>();
            commandStack.Push("call-action this that");
            var view = new SimpleMockView(commandStack);
            var passthroughController = new PassthroughController();
            var controllers = new IController[] { passthroughController };
            var app = new ReplApplication(view, controllers);

            app.Repl();

            // Responses should be OK this that OK
            Assert.AreEqual(4, view.ResponseStack.Count);
            var missingResponses = new List<String> { "this", "that", "OK" };
            while (view.ResponseStack.Count > 0)
            {
                var resp = view.ResponseStack.Pop();
                if (missingResponses.Contains(resp)) missingResponses.Remove(resp);
            }
            Assert.AreEqual(0, missingResponses.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ReplAppSetupException))]
        public void TestRedundantCommandRegistrationWithNullView()
        {
            var passthroughController = new PassthroughController();
            var controllers = new IController[] { passthroughController, passthroughController };
            var app = new ReplApplication(null, controllers);
        }

        [TestMethod]
        [ExpectedException(typeof(NoViewRegisteredException))]
        public void TestAttemptedOutputWithNullView()
        {
            var passthroughController = new PassthroughController();
            var controllers = new IController[] { passthroughController };
            var app = new ReplApplication(null, controllers);
            app.Repl();
        }

        [TestMethod]
        public void TestRedundantViewRegistration()
        {
            var passthroughController = new PassthroughController();
            var view = new SimpleMockView(new Stack<String>());
            var controllers = new IController[] { passthroughController };
            var app = new ReplApplication(view, controllers);
            var registerResult = app.RegisterView(view);
            Assert.AreEqual("View already registered. ReplApplication only supports a single view.", registerResult.Messages[0]);
        }
    }
}
