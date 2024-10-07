using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Tests;

internal class NepNavigateService : INavigateService
{
    public bool HasNextBeenCalled { get; set; }

    public int Next<T>(List<T> data, int currentHighlightedIndex)
    {
        HasNextBeenCalled = true;
        return 12;
    }
}
