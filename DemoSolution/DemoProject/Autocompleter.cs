using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoProject;

public class Autocompleter<T>
{
    public string Query { get; set; }
    public List<T> Data { get; set; }
    public List<T> Suggestions { get; set; }
    public int HighlightedSuggestionIndex { get; set; } = -1;
    private INavigateService _navigateService;

    public Autocompleter(INavigateService navigateService)
    {
        _navigateService = navigateService;
    }

    public void Autocomplete()
    {
        Suggestions = new List<T>();

        // reflection
        var props = Data.First()!.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
        foreach (var item in Data)
        {
            foreach (var prop in props)
            {
                var value = prop.GetValue(item) as string;
                if (value.Contains(Query, StringComparison.OrdinalIgnoreCase))
                {
                    Suggestions.Add(item);
                    break;
                }
            }
        }
    }

    public void Next()
    {
        HighlightedSuggestionIndex = _navigateService.Next(Suggestions, HighlightedSuggestionIndex);
    }
}
