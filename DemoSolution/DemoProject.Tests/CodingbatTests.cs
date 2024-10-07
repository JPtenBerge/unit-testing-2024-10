using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Tests;

[TestClass]
public class CodingbatTests
{
    Codingbat _sut;

    [TestInitialize]
    public void Init()
    {
        _sut = new Codingbat();
    }

    [TestMethod]
    public void StartOz_Hello_ReturnsOz()
    {
        var result = _sut.StartOz("Hello");
        Assert.AreEqual("oz", result);
    }

    [TestMethod]
    public void StartOz_bzoo_ReturnsOz()
    {
        var result = _sut.StartOz("bzoo");
        Assert.AreEqual("z", result);
    }

    [TestMethod]
    public void StartOz_oxx_ReturnsO()
    {
        var result = _sut.StartOz("oxx");
        Assert.AreEqual("o", result);
    }

    [TestMethod]
    public void StartOz_ozymandias_ReturnsO()
    {
        var result = _sut.StartOz("ozymandias");
        Assert.AreEqual("oz", result);
    }

    [TestMethod]
    public void StartOz_ozymsvandias_ReturnsO()
    {
        var result = _sut.StartOz("ozymandias");
        Assert.AreEqual("oz", result);
    }
}
