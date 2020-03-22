using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class NameRule : IProductRule
    {
        public bool ApplyRule(Product product)
        {
            return !string.IsNullOrEmpty(product.Name);
        }

        public bool ApplyRule(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}
