using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class GrammsRule : IRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Gramms <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
