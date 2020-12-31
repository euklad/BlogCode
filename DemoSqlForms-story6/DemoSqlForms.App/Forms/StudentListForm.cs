using DemoSqlForms.Database.Model;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class StudentListForm : DataServiceBase<SchoolContext>
    {
        protected override void Define(DataServiceFormBuilder builder)
        {
            builder.Entity<StudentDetails>(e =>
            {
                e.ExcludeAll();

                e.Property(p => p.ID).IsPrimaryKey();

                e.Property(p => p.FirstMidName);

                e.Property(p => p.LastName);

                e.Property(p => p.EnrollmentDate).Format("dd-MMM-yyyy");

                e.Property(p => p.EnrollmentCount);

                // Parameter {0} is always PrimaryKey, parameters {1} and above - Filter Keys
                // {0} = AddressId {1} = CustomerId
                e.ContextButton("Edit", "StudentEdit/{0}").ContextButton("Delete", "StudentDelete/{0}").ContextButton("Enrollments", "EnrollmentList/{0}");

                e.DialogButton("StudentEdit/{0}", ButtonActionTypes.Add);
            });

            builder.SetListMethod(GetStudentList);
        }

        public class StudentDetails : Student
        {
            public int EnrollmentCount { get; set; }
        }

        public List<StudentDetails> GetStudentList(params object[] parameters)
        {
            using (var db = GetDbContext())
            {
                var query =
                    from s in db.Student
                    select new StudentDetails
                    {
                        ID = s.ID,
                        FirstMidName = s.FirstMidName,
                        LastName = s.LastName,
                        EnrollmentDate = s.EnrollmentDate,
                        EnrollmentCount = (db.Enrollment.Where(e => e.StudentID == s.ID).Count())
                    };

                var result = query.ToList();
                return result;
            }
        }
    }
}
