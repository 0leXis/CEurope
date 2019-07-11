using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;

namespace UnitTestProject
{
    [TestClass]
    public class TypeTests
    {
        [TestMethod]
        public void TypeTest1()
        {
            var literal = "123";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.AreEqual("Int", type);
        }
        [TestMethod]
        public void TypeTest2()
        {
            var literal = "123.23";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.AreEqual("Double", type);
        }
        [TestMethod]
        public void TypeTest3()
        {
            var literal = "123.257";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.AreEqual("Double", type);
        }
        [TestMethod]
        public void TypeTest4()
        {
            var literal = "1237358785687683";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.AreEqual("Long", type);
        }
        [TestMethod]
        public void TypeTest5()
        {
            var literal = "'d'";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.AreEqual("Char", type);
        }
        [TestMethod]
        public void TypeTest6()
        {
            var literal = "\"12373asdf683\"";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.AreEqual("String", type);
        }
        [TestMethod]
        public void TypeTest7()
        {
            var literal = "'12'";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.IsFalse(Result);
        }
        [TestMethod]
        public void TypeTest8()
        {
            var literal = "123.243.35";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.IsFalse(Result);
        }
        [TestMethod]
        public void TypeTest9()
        {
            var literal = "12h4sdfa5";
            string type;

            var Result = Interpreter.GetLiteralTypeTEST(literal, out type);

            Assert.IsFalse(Result);
        }
    }
}
