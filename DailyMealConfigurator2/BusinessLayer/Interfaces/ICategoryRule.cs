using BusinessLayer.Utility;

namespace BusinessLayer.Interfaces
{
    public interface ICategoryRule
    {
        bool ApplyRule(Category category);
    }
}
