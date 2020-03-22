using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class ProteinsRule : IProductRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Proteins <= 0)
            {
                return false;
            }
            return true;
        }

        public bool ApplyRule(double proteins)
        {
            if (proteins <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
