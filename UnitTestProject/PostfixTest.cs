using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class PostfixTest
    {
        [TestMethod]
        public void PostfixTest1()
        {
            var expression = "3+4-44";
            List<string> res;
            var result = Interpreter.GetPostfixFormTEST(expression, out res);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PostfixTest2()
        {
            var expression = "\"3fd54f\"+(8-44)*7";
            List<string> res;
            var result = Interpreter.GetPostfixFormTEST(expression, out res);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PostfixTest3()
        {
            var expression = "3+('d'*(-44))*7";
            List<string> res;
            var result = Interpreter.GetPostfixFormTEST(expression, out res);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PostfixTest4()
        {
            var expression = "34+(56*(-44)))*7";
            List<string> res;
            var result = Interpreter.GetPostfixFormTEST(expression, out res);
            Assert.IsNotNull(result);
        }
    }
}
