using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCastleProxy
{
    public interface IModel
    {
        List<string> PropertyChangeList { get; }
    }
}

