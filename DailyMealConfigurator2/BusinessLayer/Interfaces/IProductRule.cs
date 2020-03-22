using BusinessLayer.Utility;

namespace BusinessLayer.Interfaces
{
    public interface IProductRule
    {
        bool ApplyRule(Product product);
    }
}
