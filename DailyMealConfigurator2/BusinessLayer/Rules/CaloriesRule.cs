using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class CaloriesRule : IProductRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Calories <= 0)
            {
                return false;
            }
            return true;
        }

        public bool ApplyRule(double calories)
        {
            if (calories <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
