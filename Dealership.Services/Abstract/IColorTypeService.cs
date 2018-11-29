using Dealership.Data.Models;
using System.Collections.Generic;

namespace Dealership.Services.Abstract
{
    public interface IColorTypeService
    {
        IList<ColorType> GetColorTypes();

        ColorType GetColorType(int id);
    }
}