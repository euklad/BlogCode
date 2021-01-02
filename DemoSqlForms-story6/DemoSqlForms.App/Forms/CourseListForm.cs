using DemoSqlForms.Database.Model;
using Platz.SqlForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSqlForms.App.Forms
{
    public class CourseListForm : DataServiceBase<SchoolContext>
    {
        protected override void Define(DataServiceFormBuilder builder)
        {
            builder.Entity<Course>(e =>
            {
                e.ExcludeAll();

                e.Property(p => p.CourseID).IsPrimaryKey();

                e.Property(p => p.Title);

                e.Property(p => p.Credits);

                // Parameter {0} is always PrimaryKey, parameters {1} and above - Filter Keys
                // {0} = AddressId {1} = CustomerId
                e.ContextButton("Edit", "CourseEdit/{0}").ContextButton("Delete", "CourseDelete/{0}");

                e.DialogButton("CourseEdit/0", ButtonActionTypes.Add);
            });

            builder.SetListMethod(GetCourseList);
        }

        public List<Course> GetCourseList(params object[] parameters)
        {
            using (var db = GetDbContext())
            {
                var query =
                    from s in db.Course
                    select new Course
                    {
                        CourseID = s.CourseID,
                        Title = s.Title,
                        Credits = s.Credits
                    };

                var result = query.ToList();
                return result;
            }
        }
    }
}
