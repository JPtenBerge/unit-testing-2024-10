using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject;

public class Codingbat
{
    public string StartOz(string str)
    {
        if (str.Substring(0, 2) == "oz")
        {
            return "oz";
        }
        if (str[1] == 'z')
        {
            return "z";
        }
        if (str[0] == 'o')
        {
            return "o";
        }

        return "oz";
    }

}
