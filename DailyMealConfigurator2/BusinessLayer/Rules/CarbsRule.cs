using BusinessLayer.Interfaces;
using BusinessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Rules
{
    public class CarbsRule : IProductRule
    {
        public bool ApplyRule(Product product)
        {
            if (product.Carbs <= 0)
            {
                return false;
            }
            return true;
        }

        public bool ApplyRule(double carbs)
        {
            if (carbs <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
