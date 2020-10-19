using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSystemTextJson.Tests
{
    public interface IModel
    {

    }

    public class MyModel : IModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class MyState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public DateTime LastUpdated { get; set; }

        public object Model { get; set; }
    }

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

    public class MyStateWrapper : MyState, IJsonModelWrapper
    {
        public string ModelFullName { get; set; }
    }
}
