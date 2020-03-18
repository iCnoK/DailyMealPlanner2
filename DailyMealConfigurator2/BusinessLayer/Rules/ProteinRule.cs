using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class ProteinRule : IRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Protein <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
