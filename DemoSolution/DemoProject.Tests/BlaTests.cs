using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Tests;

[TestClass]
public class BlaTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DivideTest()
    {
        var sut = new Calculator();
        sut.Divide(0);
    }

    [TestMethod]
    public void DivideBetereTest()
    {
        var sut = new Calculator();
        Assert.ThrowsException<ArgumentException>(() => sut.Divide(0));
    }

    // parameterized tests / data-driven tests

    [TestMethod]
    [DataRow(4, 8, 12)]
    [DataRow(-4, -8, -12)]
    [DataRow(4, -8, -4)]
    [DataRow(-4, 8, 4)]
    public void ParamTest(int first, int second, int expected)
    {
        var sut = new Calculator();
        sut.Add(first);
        sut.Add(second);
        Assert.AreEqual(expected, sut.Result);
    }

    [TestMethod]
    //[DynamicData()]
    public void MyTestMethod()
    {

    }
}
