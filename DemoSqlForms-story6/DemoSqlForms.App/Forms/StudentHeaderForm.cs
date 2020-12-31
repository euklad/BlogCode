using DemoSqlForms.Database.Model;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class StudentHeaderForm : DynamicEditFormBase<SchoolContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Student>(e =>
            {
                e.ExcludeAll();

                e.Property(p => p.ID).IsReadOnly();

                e.Property(p => p.FirstMidName).IsReadOnly();

                e.Property(p => p.LastName).IsReadOnly();
            });
        }
    }
}
