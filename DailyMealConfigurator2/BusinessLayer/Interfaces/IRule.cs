using BusinessLayer.Utility;

namespace BusinessLayer.Interfaces
{
    public interface IRule
    {
        bool ApplyRule(Product product);
    }
}
