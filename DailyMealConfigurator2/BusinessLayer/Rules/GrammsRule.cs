using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class GrammsRule : IProductRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Gramms <= 0)
            {
                return false;
            }
            return true;
        }

        public bool ApplyRule(double gramms)
        {
            if (gramms <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
