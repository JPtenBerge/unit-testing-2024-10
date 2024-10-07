using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace DemoProject.Tests;

[TestClass]
public class CalculatorTests
{
    Calculator _sut;

    [TestInitialize] // async inheritance - gaat af vooraf aan IEDERE test
    public void Init()
    {
        _sut = new Calculator();
    }

    //public CalculatorTests()
    //{
           
    //}
    //~CalculatorTests()
    //{ 

    //}

    [TestMethod]
    public void TestMethod1()
    {
        Assert.AreEqual(1, 1);
        Assert.AreEqual("hoi", "doei");
    }

    // CalculatorAddShouldIncrementWhen()
    // WhenDitThen
    // GivenWhenThen
    // UnitOfWork_State_ExpectedBehavior
    // 


    [TestMethod]
    public void Add_PositiveIntegers_AddsNumbers()
    {
        // Arrange - setup
        
        _sut.Add(4);

        // Act - doen
        _sut.Add(8);

        // Assert - checken
        Assert.AreEqual(12, _sut.Result);
        Assert.AreEqual(12, _sut.Result);
        Assert.AreEqual(12, _sut.Result);
        Assert.AreEqual(12, _sut.Result);
        Assert.AreEqual(12, _sut.Result);
    }

    [TestMethod]
    public void Add_MixedPositiveAndNegativeIntegers_AddsNumbers()
    {
        // system under test
        _sut.Add(-4);
        _sut.Add(8);
        _sut.Add(-15);
        Assert.AreEqual(-11, _sut.Result);
    }

    [TestMethod]
    public void AddTest3()
    {
        // system under test
        _sut.Minus(4);
        _sut.Minus(8);

        Assert.AreEqual(-12, _sut.Result);
    }

    [TestMethod]
    public void AddTest4()
    {
        //Assembly.GetExecutingAssembly().GetTypes().First().GetMethods(BindingFlags.).First().Invoke()

        // system under test
        _sut.Result = -3;
        _sut.Minus(9);

        Assert.AreEqual(-12, _sut.Result);
    }
}