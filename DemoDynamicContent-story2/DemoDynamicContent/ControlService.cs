using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDynamicContent
{
    public class ControlService
    {
        public List<ControlDetails> GetControls()
        {
            var result = new List<ControlDetails>();
            result.Add(new ControlDetails { Type = "TextEdit", Label = "First Name", IsRequired = true });
            result.Add(new ControlDetails { Type = "TextEdit", Label = "Last Name", IsRequired = true });
            result.Add(new ControlDetails { Type = "DateEdit", Label = "Birth Date", IsRequired = false });
            // add custom control
            result.Add(new ControlDetails { Type = "Component1", Label = "Custom1", IsRequired = false });
            return result;
        }
    }
}


