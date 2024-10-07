using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject;

public class NavigateService : INavigateService
{
    public int Next<T>(List<T> data, int currentHighlightedIndex)
    {
        return (currentHighlightedIndex + 1) % data.Count;
    }
}
