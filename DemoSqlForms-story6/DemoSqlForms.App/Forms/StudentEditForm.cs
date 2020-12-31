using DemoSqlForms.Database.Model;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class StudentEditForm : DynamicEditFormBase<SchoolContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Student>(e =>
            {
                e.Property(p => p.ID).IsReadOnly();

                e.Property(p => p.FirstMidName).IsRequired();

                e.Property(p => p.LastName).IsRequired();

                e.Property(p => p.EnrollmentDate).Rule(DefaultDate, FormRuleTriggers.Create).Rule(CheckDate);

                e.DialogButton(ButtonActionTypes.Cancel).DialogButton(ButtonActionTypes.Validate).DialogButton(ButtonActionTypes.Submit);

                e.DialogButtonNavigation("StudentList", ButtonActionTypes.Cancel, ButtonActionTypes.Delete, ButtonActionTypes.Submit);
            });
        }

        public FormRuleResult DefaultDate(Student model)
        {
            model.EnrollmentDate = new DateTime(DateTime.Now.Year, 9, 1);
            return null;
        }

        public FormRuleResult CheckDate(Student model)
        {
            if (model.EnrollmentDate < new DateTime(2015, 1, 1))
            {
                return new FormRuleResult("EnrollmentDate is incorrect");
            }

            return null;
        }
    }
}
