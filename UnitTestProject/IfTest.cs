using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;
using System.IO;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class IfTest
    {
        [TestMethod]
        public void IfTest1()
        {
            var program =
            @"використовуючи System;
            простірімен Проект1
	            програма
		            якщо(true)
			            ПисатиРядок true;
		            інакше
                        ПисатиРядок false;
                    ПисатиРядок 12;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IfTest2()
        {
            var program =
            @"використовуючи System;
            простірімен Проект1
	            програма 
		            якщо (true)
			            ПисатиРядок true;
                    ПисатиРядок 12;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IfTest3()
        {
            var program =
            @"використовуючи System;
            простірімен Проект1
	            програма 
		            якщо (true)
                        почати
			                ПисатиРядок true;
                            ПисатиРядок true;
                            ПисатиРядок true;
                        кінець
                    ПисатиРядок 12;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IfTest4()
        {
            var program =
            @"використовуючи System;
            простірімен Проект1
	            програма 
		            якщо(true)
                        почати
			                ПисатиРядок true;
                            ПисатиРядок true;
                            ПисатиРядок true;
                        кінець
		            інакше
                        почати
			                ПисатиРядок false;
                            ПисатиРядок false;
                            ПисатиРядок false;
                        кінець
                    ПисатиРядок 12;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IfTest5()
        {
            var program =
            @"використовуючи System;
            простірімен Проект1
	            програма 
		            якщо(true)
                        почати
			                ПисатиРядок true;
                            ПисатиРядок true;
                            ПисатиРядок true;
                        кінець
		            інакше
                        почати
			                ПисатиРядок false;
                            ПисатиРядок false;
                            ПисатиРядок false;
                        кінець
                    ПисатиРядок 12;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IfTest6()
        {
            var program =
            @"використовуючи System;
простірімен Проект1
    програма 
        якщо(false)
            якщо(false)
                ПисатиРядок 1;
            інакше
                почати
                    ПисатиРядок 2;
                    ПисатиРядок 2;
                    ПисатиРядок 2;
                кінець
        інакше
            ПисатиРядок 3;
        ПисатиРядок 4;
    кінець  
кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }
    }
}
