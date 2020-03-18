using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class FatsRule : IRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Fats <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
