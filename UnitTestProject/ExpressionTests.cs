using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;
using CEurope;

namespace UnitTestProject
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void ExpressionTest1()
        {
            var sourcedata = "a+b*(d-l)";
            int NextIndex = 0;
            int line_ptr = 0;
            string expression;
            bool IsInFunction = false;

            var Result = Interpreter.GetNextExpressionTEST(sourcedata, ref NextIndex, ref line_ptr, out expression, IsInFunction);

            Assert.AreEqual(sourcedata, expression);
        }

        [TestMethod]
        public void ExpressionTest2()
        {
            var sourcedata = "a+b*(d(-l)";
            int NextIndex = 0;
            int line_ptr = 0;
            string expression;
            bool IsInFunction = false;

            var Result = Interpreter.GetNextExpressionTEST(sourcedata, ref NextIndex, ref line_ptr, out expression, IsInFunction);

            Assert.IsFalse(Result);
        }

        [TestMethod]
        public void ExpressionTest3()
        {
            var sourcedata = "a+b*(d(-l)))";
            int NextIndex = 0;
            int line_ptr = 0;
            string expression;
            bool IsInFunction = true;

            var Result = Interpreter.GetNextExpressionTEST(sourcedata, ref NextIndex, ref line_ptr, out expression, IsInFunction);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void ExpressionTest4()
        {
            var sourcedata = "a+b*(d(-l)),";
            int NextIndex = 0;
            int line_ptr = 0;
            string expression;
            bool IsInFunction = true;

            var Result = Interpreter.GetNextExpressionTEST(sourcedata, ref NextIndex, ref line_ptr, out expression, IsInFunction);

            Assert.IsTrue(Result);
        }

        [TestMethod]
        public void ExpressionTest5()
        {
            //TODO: Check after function realization
            var sourcedata = "a+b*d+(-l) - ghs(sds+43*(f(x,y)))";
            int NextIndex = 0;
            int line_ptr = 0;
            string expression;
            bool IsInFunction = false;

            var Result = Interpreter.GetNextExpressionTEST(sourcedata, ref NextIndex, ref line_ptr, out expression, IsInFunction);

            Assert.IsTrue(Result);
        }
    }
}
