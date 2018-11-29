using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class ColorTypeService : IColorTypeService
    {
        private readonly DealershipContext context;

        public ColorTypeService(DealershipContext context)
        {
            this.context = context;
        }

        public IList<ColorType> GetColorTypes()
        {
            return this.context.ColorTypes.ToList();
        }
        public ColorType GetColorType(int id)
        {
            var colorType = this.context.ColorTypes.Find(id);
            if (colorType == null)
            {
                throw new ServiceException($"There is no colorType with id {id}.");
            }
            return colorType;
        }
    }
}
