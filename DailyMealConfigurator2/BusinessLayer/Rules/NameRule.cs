using BusinessLayer.Interfaces;
using BusinessLayer.Utility;

namespace BusinessLayer.Rules
{
    public class NameRule : IRule
    {
        public bool ApplyRule(Product product)
        {
            return string.IsNullOrEmpty(product.Name);
        }
    }
}
