using DemoSqlForms.Database.Model;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class EnrollmentEditForm : DynamicEditFormBase<SchoolContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Enrollment>(e =>
            {
                e.Property(p => p.EnrollmentID).IsPrimaryKey().IsReadOnly();

                e.Property(p => p.StudentID).IsFilter().IsHidden();

                e.Property(p => p.CourseID).IsRequired().Dropdown<Course>().Set(c => c.CourseID, c => c.Title);

                e.Property(p => p.Grade).IsRequired().Rule(DefaultGrade, FormRuleTriggers.Create).Dropdown<Grade>().Set(g => g, g => g);

                e.DialogButton(ButtonActionTypes.Cancel).DialogButton(ButtonActionTypes.Submit);

                // {0} always reserved for Primary Key (EnrollmentID in this case) but EnrollmentList accepts StudentId as parameter
                e.DialogButtonNavigation("EnrollmentList/{1}", ButtonActionTypes.Cancel, ButtonActionTypes.Delete, ButtonActionTypes.Submit);
            });
        }

        public FormRuleResult DefaultGrade(Enrollment model)
        {
            model.Grade = Grade.A;
            return null;
        }
    }
}
