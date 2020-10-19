using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystemTextJson.Tests
{
    public class MyStateWrapper : MyState, IJsonModelWrapper
    {
        public string ModelFullName { get; set; }
    }
}
