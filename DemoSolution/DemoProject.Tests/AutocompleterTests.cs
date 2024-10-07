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
    NepNavigateService _nepNavigateService;

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
        _nepNavigateService = new NepNavigateService();
        _sut = new Autocompleter<Car>(_nepNavigateService);
        _sut.Data = _data;
    }

    [TestMethod]
    public void Autocomplete_BasicQuery_GiveSuggestions()
    {
        // Arrange
        _sut.Query = "e";

        // Act
        _sut.Autocomplete();

        // Assert
        var expected = new List<Car>
        {
           new() { Make = "Renault", Model = "Clio", ProductionYear = 2024},
           new() { Make = "Opel", Model = "Corsa", ProductionYear = 2024},
           new() { Make = "Volkswagen", Model = "Golf", ProductionYear = 2024},
           new() { Make = "Opel", Model = "Astra", ProductionYear = 2024},
           new() { Make = "Peugeot", Model = "208", ProductionYear = 2024},
           new() { Make = "Mercedes", Model = "CLA", ProductionYear = 2023},
        };
        _sut.Suggestions.Should().BeEquivalentTo(expected, options =>
        {
            return options.Excluding(x => x.ProductionYear);
        });
    }

    [TestMethod]
    public void Autocomplete_NonMatchingQuery_GiveNoSuggestions()
    {
        // Arrange
        _sut.Query = "q";

        // Act
        _sut.Autocomplete();

        // Assert
        _sut.Suggestions.Should().BeEmpty();
    }

    [TestMethod]
    public void Autocomplete_AllPropsMatchingQuery_GiveNoSuggestions()
    {
        // Arrange
        _sut.Query = "q";

        // Act
        _sut.Autocomplete();

        // Assert
        _sut.Suggestions.Should().BeEmpty();
    }

    [TestMethod]
    public void Autocomplete_QueryMatchingMultipleProps_GiveUniqueSuggestions()
    {
        // Arrange
        _sut.Query = "o";

        // Act
        _sut.Autocomplete();

        // Assert
        _sut.Suggestions.Should().OnlyHaveUniqueItems();
    }

    [TestMethod]
    public void MyTestMethod()
    {
        _sut.Next();

        _nepNavigateService.HasNextBeenCalled.Should().BeTrue();
        _sut.HighlightedSuggestionIndex.Should().Be(12);
    }
}
