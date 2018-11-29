using System.Threading.Tasks;

namespace Dealership.Services.Abstract
{
    public interface IEditCarService
    {
       Task< string> EditBrand(string[] parameters);
      
       Task< string> EditModel(string[] parameters);
       
       Task< string> EditHorsePower(string[] parameters);
      
       Task< string> EditEngineCapacity(string[] parameters);
       
       Task< string> EditIsSold(string[] parameters);
       
       Task< string> EditPrice(string[] parameters);
       
       Task< string> EditProductionDate(string[] parameters);
       
       Task< string> EditBodyType(string[] parameters);
       
       Task< string> EditColor(string[] parameters);
       
       Task< string> EditColorType(string[] parameters);

       Task< string> EditFuelType(string[] parameters);

       Task< string> EditGearbox(string[] parameters);
    }
}
