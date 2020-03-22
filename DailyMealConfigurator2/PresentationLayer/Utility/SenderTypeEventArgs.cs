using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Utility
{
    public class SenderTypeEventArgs : EventArgs
    {
        public SenderTypeEventArgs(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
