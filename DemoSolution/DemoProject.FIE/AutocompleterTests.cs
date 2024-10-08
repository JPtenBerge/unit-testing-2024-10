using DemoProject.FIE;
using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Tests;

[TestClass]
public class AutocompleterTests
{
    List<Car> _data;
    Autocompleter<Car> _sut;
    INavigateService _mockNavigateService;

    [TestInitialize]
    public void Init()
    {
        _data = [
           new() { Make = "Renault", Model = "Clio", ProductionYear = 2024 },
           new() { Make = "Opel", Model = "Corsa", ProductionYear = 2024 },
           new() { Make = "Volkswagen", Model = "Golf", ProductionYear = 2024 },
           new() { Make = "Opel", Model = "Astra", ProductionYear = 2024 },
           new() { Make = "Peugeot", Model = "208", ProductionYear = 2024 },
           new() { Make = "Mercedes", Model = "CLA", ProductionYear = 2024 },
           new() { Make = "Hyuandai", Model = "i20", ProductionYear = 2024 },
        ];

        _mockNavigateService = A.Fake<INavigateService>();
        _sut = new Autocompleter<Car>(_mockNavigateService);
        _sut.Data = _data;
    }

    [TestMethod]
    public void MyTestMethod()
    {
        _sut.Query = "e";
        _sut.Autocomplete();
        A.CallTo(() => _mockNavigateService.Next(A<List<Car>>._, A<int>._)).Returns(12);

        _sut.Next();

        A.CallTo(() => _mockNavigateService.Next(A<List<Car>>._, A<int>._)).MustHaveHappened();
        _sut.HighlightedSuggestionIndex.Should().Be(12);
    }
}
