using FoodApp.Models;

namespace FoodApp.Service
{
    public interface IFoodService
    {
        List<Yemekler> GetYemeklers();
        List<Yemekler> SaveYemeklers(List<Yemekler> yemekler);
    }
}
