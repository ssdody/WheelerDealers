using Dealership.Data.Models;

namespace Dealership.Services.Abstract
{
    public interface IColorService
    {
        Color AddColor(string name, int colorTypeId);

        Color GetColor(string name, int colorTypeId);
    }
}