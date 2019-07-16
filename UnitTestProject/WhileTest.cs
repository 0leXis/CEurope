using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEVirtualMachine;

namespace UnitTestProject
{
    [TestClass]
    public class WhileTest
    {
        [TestMethod]
        public void WhileTest1()
        {
            var program =
@"використовуючи System;
            простірімен Проект1
	            програма
                    змінна А = 1;
		            поки (А == 1)
                        А = А + 1;
                    ПисатиРядок А;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhileTest2()
        {
            var program =
@"використовуючи System;
            простірімен Проект1
	            програма
                    змінна А = 1;
                    змінна Б = 1;
		            поки (А < 10)
                        поки (Б < 10 * А)
                            Б = Б + 1;
                    ПисатиРядок Б;
	            кінець	
            кінець";
            var result = Interpreter.Interpret(program);
            Assert.IsTrue(result);
        }
    }
}
