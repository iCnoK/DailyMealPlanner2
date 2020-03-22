using BusinessLayer.Interfaces;
using BusinessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Rules.CategoryRules
{
    public class NameRule : ICategoryRule
    {
        public bool ApplyRule(Category category)
        {
            return !string.IsNullOrEmpty(category.Name);
        }

        public bool ApplyRule(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}
