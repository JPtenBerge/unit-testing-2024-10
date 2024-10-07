
namespace DemoProject;

public interface INavigateService
{
    int Next<T>(List<T> data, int currentHighlightedIndex);
}