using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Chart.service
{
    class DemoClass
    {
        int m_DisplayInt;
        public int Display
        {
            get { return m_DisplayInt; }
            set { m_DisplayInt = value; }
        }

        string m_DisplayString;
        public String DisplayString
        {
            get { return m_DisplayString; }
            set { m_DisplayString = value; }
        }
    }
}
