using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Tests;

[TestClass]
public class NavigateServiceTests
{
    NavigateService _sut;
    List<Car> _data;

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
        _sut = new NavigateService();
    }

    [TestMethod]
    public void Next_NothingHighlighted_HighlightFirstSuggestion()
    {
        var result = _sut.Next(_data, -1);
        result.Should().Be(0);
    }

    [TestMethod]
    public void Next_FirstSuggestionHighlighted_HighlightSecondSuggestion()
    {
        var result = _sut.Next(_data, 0);
        result.Should().Be(1);
    }

    [TestMethod]
    public void Next_LastSuggestionHighlighted_HighlightFirstSuggestion()
    {
        var result = _sut.Next(_data, _data.Count - 1);
        result.Should().Be(0);
    }
}
