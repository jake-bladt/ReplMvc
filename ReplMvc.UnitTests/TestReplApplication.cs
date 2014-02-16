using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ReplMvc;
using ReplMvc.Controllers;
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

    }
}
