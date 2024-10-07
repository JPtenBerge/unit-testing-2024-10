using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject;

//[InternalsVisibleTo("DemoProject.Tests")]
public class Calculator
{
    public int Result { get; set; }

    public void Add(int x)
    {
        Result += x;
    }

    public void Minus(int x)
    {
        Result -= x;
    }
}
