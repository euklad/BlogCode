using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCastleProxy
{
    public class PersonRule : IModelRule
    {
        public void Execute(object model, string fieldName)
        {
            var personModel = model as PersonModel;

            if (personModel != null && fieldName == "LastName")
            {
                if (personModel.FirstName?.ToLower() == "john" &&
                    personModel.LastName?.ToLower() == "lennon")
                {
                    personModel.BirthDate = new DateTime(1940, 10, 9);
                }
            }
        }
    }
}



