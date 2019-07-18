using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;

namespace UnitTestProject
{
    [TestClass]
    public class TableTest
    {
        [TestMethod]
        public void TableTest1()
        {
            var program =
@"використовуючи System;
            простірімен Проект1
	            програма
                    змінна А = {};
                    таблиця Б = {};
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TableTest2()
        {
            var program =
@"використовуючи System;
            простірімен Проект1
	            програма
                    змінна А = { a = 12, b = 23};
                    таблиця Б = { a = "" "" + 'd' + 's'};
                    ПисатиРядок Б.a;
                    ПисатиРядок А.b;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TableTest3()
        {
            var program =
@"використовуючи System;
            простірімен Проект1
	            програма
                    змінна А = {};
                    А.л = 12;
                    А[А.л] = 5;
                    ПисатиРядок А.12;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TableTest4()
        {
            var program =
@"використовуючи System;
            простірімен Проект1
	            програма
                    таблиця Б;
                    Б.п = 12;
                    Б.л = { d = 6 };
                    Б.л[3] = 5;
                    ПисатиРядок Б.л.3;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }
    }
}
