using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoCastleProxy
{
    public class PersonModel : IModel
    {
        public virtual string FirstName { get; set; }

        [ModelRule(typeof(PersonRule))]
        public virtual string LastName { get; set; }
        public virtual DateTime? BirthDate { get; set; }

        public virtual List<string> PropertyChangeList { get; set; } = 
            new List<string>();
    }

    // a proxy example
    public class PersonModelProxy : PersonModel
    {
        public override string FirstName 
        { 
            get
            {
                Intercept("get_FirstName", base.FirstName);
                return base.FirstName;
            }
            
            set 
            {
                Intercept("set_FirstName", value);
                base.FirstName = value;
            } 
        }

        private void Intercept(string propertyName, object value)
        {
            // do something here
        }
    }
}

