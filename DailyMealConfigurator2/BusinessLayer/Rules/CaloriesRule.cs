using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class CaloriesRule : IRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Calories <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
