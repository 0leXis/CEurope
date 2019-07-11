using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;

namespace UnitTestProject
{
    [TestClass]
    public class ExpressionDoTests
    {
        [TestMethod]
        public void ExpressionDoTest1()
        {
            var expression = "2+2";
            string res;
            var result = Interpreter.InterpretExpressionTEST(expression, out res);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ExpressionDoTest2()
        {
            var expression = "2+2*4+(12-2*(-2))";
            string res;
            var result = Interpreter.InterpretExpressionTEST(expression, out res);
            Assert.IsNull(result);
        }
        [TestMethod]
        public void ExpressionDoTest3()
        {
            var expression = "\"Hello\"+\" World\"+'!'";
            string res;
            var result = Interpreter.InterpretExpressionTEST(expression, out res);
            Assert.AreEqual("Hello World!", res);
        }
        [TestMethod]
        public void ExpressionDoTest4()
        {
            var expression = "\"Hello\"+\" World\"+'!f'";
            string res;
            var result = Interpreter.InterpretExpressionTEST(expression, out res);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void ExpressionDoTest5()
        {
            var expression = "\"Hello\"+\" World\"+12";
            string res;
            var result = Interpreter.InterpretExpressionTEST(expression, out res);
            Assert.AreEqual("Hello World12", res);
        }
    }
}
