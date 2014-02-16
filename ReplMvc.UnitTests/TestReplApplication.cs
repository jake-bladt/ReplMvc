using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ReplMvc;

namespace ReplMvc.UnitTests
{
    [TestClass]
    public class TestReplApplication
    {
        [TestMethod]
        public void TestEmptyReplApplication()
        {
            var app = new ReplApplication();
            Assert.AreEqual(2, app.CommandActions.Count);  // built-in commands from ReplApplication.
            Assert.IsNull(app.View);
        }
    }
}
