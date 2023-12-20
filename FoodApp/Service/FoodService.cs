using EFCore.BulkExtensions;
using FoodApp.Models;

namespace FoodApp.Service
{
    public class FoodService : IFoodService
    {

        TestDBContext _dbContext = null;

        public FoodService(TestDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public List<Yemekler> GetYemeklers()
        {
            return _dbContext.Yemeklers.ToList();
        }

        public List<Yemekler> SaveYemeklers(List<Yemekler> yemekler)
        {
            _dbContext.BulkInsert(yemekler);
            return yemekler;
        }
    }
}
