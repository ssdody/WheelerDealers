using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Linq;

namespace Dealership.Services
{
    public class ColorService : IColorService
    {
        private readonly DealershipContext context;

        public ColorService(DealershipContext context)
        {
            this.context = context;
        }

        public Color AddColor(string name, int colorTypeId)
        {
            if (this.context.ColorTypes.Find(colorTypeId) == null)
            {
                throw new ServiceException($"There is no colorType with id {colorTypeId}.");
            }
            var color = new Color() { Name = name, ColorTypeId = colorTypeId };
            this.context.Colors.Add(color);
            this.context.SaveChanges();
            return color;
        }

        public Color GetColor(string name, int colorTypeId)
        {
            //must return null if not found
            return this.context.Colors.FirstOrDefault(c => c.Name == name && c.ColorTypeId == colorTypeId);
        }
    }
}
