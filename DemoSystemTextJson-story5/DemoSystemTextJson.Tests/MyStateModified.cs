using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystemTextJson.Tests
{
    public class MyStateModified : IJsonModelWrapper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public DateTime LastUpdated { get; set; }

        public object Model { get; set; }

        // IJsonModelWrapper
        public string ModelFullName { get; set; }
    }
}
