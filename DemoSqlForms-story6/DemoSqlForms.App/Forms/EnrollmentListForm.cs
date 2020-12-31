using Default;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class EnrollmentListForm : MyDataService
    {
        protected override void Define(DataServiceFormBuilder builder)
        {
            builder.Entity<EnrollmentDetails>(e =>
            {
                e.ExcludeAll();

                e.Property(p => p.EnrollmentID).IsPrimaryKey();

                e.Property(p => p.StudentID).IsFilter().IsReadOnly();

                e.Property(p => p.CourseID);

                e.Property(p => p.Grade);

                e.Property(p => p.Title);

                e.Property(p => p.Credits);

                // Parameter {0} is always PrimaryKey, parameters {1} and above - Filter Keys
                // {0} = EnrollmentID {1} = StudentID
                e.ContextButton("Edit", "EnrollmentEdit/{0}/{1}").ContextButton("Delete", "EnrollmentDelete/{0}/{1}");

                e.DialogButton("StudentList", ButtonActionTypes.Custom, "Back");

                e.DialogButton("CustomerAddrEdit/0/{1}", ButtonActionTypes.Add);
            });

            builder.SetListMethod(GetEnrollmentDetailsList);
        }
    }
}
