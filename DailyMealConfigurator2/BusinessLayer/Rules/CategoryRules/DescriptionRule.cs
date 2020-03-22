using BusinessLayer.Interfaces;
using BusinessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Rules.CategoryRules
{
    public class DescriptionRule : ICategoryRule
    {
        public bool ApplyRule(Category category)
        {
            return !string.IsNullOrEmpty(category.Description);
        }

        public bool ApplyRule(string description)
        {
            return !string.IsNullOrEmpty(description);
        }
    }
}
