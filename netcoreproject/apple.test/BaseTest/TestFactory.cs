using apple.test.quartz;
using System;
using System.Collections.Generic;
using System.Text;

namespace apple.test.BaseTest
{
    public class TestFactory
    {
        public static ITest GetMyTest(int type = 0)
        {
            ITest test = new quartztest();
            switch (type)
            {
                case 1:
                    test = new quartztest(); break;
            }
            return test;
        }
    }
}
