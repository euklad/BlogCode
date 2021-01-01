using DemoSqlForms.Database.Model;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class CourseEditForm : DynamicEditFormBase<SchoolContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Course>(e =>
            {
                e.Property(p => p.CourseID).IsPrimaryKey().IsUnique(); //.IsReadOnly(false) 

                e.Property(p => p.Title).IsRequired();

                e.Property(p => p.Credits).IsRequired();

                e.DialogButton(ButtonActionTypes.Cancel).DialogButton(ButtonActionTypes.Submit);

                e.DialogButtonNavigation("CourseList", ButtonActionTypes.Cancel, ButtonActionTypes.Delete, ButtonActionTypes.Submit);
            });
        }
    }
}
