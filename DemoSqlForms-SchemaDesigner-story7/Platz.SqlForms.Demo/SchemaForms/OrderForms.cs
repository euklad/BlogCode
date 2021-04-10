using PlatzDemo.SchemaStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platz.SqlForms.Demo.SchemaForms
{
    public class OrderForm : StoreDynamicEditFormBase<PlatzDemoDataContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Order>(e =>
            {
                e.Property(p => p.Id).IsReadOnly();
                e.Property(p => p.ClientName).IsRequired();
                e.Property(p => p.Created).IsRequired();
                e.DialogButton(ButtonActionTypes.Cancel).DialogButton(ButtonActionTypes.Validate).DialogButton(ButtonActionTypes.Submit);
                e.DialogButtonNavigation("OrderList", ButtonActionTypes.Delete, ButtonActionTypes.Cancel, ButtonActionTypes.Submit);
            });
        }
    }

    public class OrderReadOnlyForm : StoreDynamicEditFormBase<PlatzDemoDataContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Order>(e =>
            {
                e.Property(p => p.Id).IsReadOnly();
                e.Property(p => p.ClientName).IsReadOnly();
                e.Property(p => p.Created).IsReadOnly();
            });
        }
    }

    public class OrderListForm : StoreDataServiceBase<PlatzDemoDataContext>
    {
        protected override void Define(DataServiceFormBuilder builder)
        {
            builder.Entity<Order>(e =>
            {
                e.ExcludeAll();
                e.Property(p => p.Id).IsPrimaryKey();
                e.Property(p => p.ClientName);
                e.Property(p => p.Created);
                e.ContextButton("Edit", "OrderEdit/{0}").ContextButton("Delete", "OrderDelete/{0}").ContextButton("Items", "OrderItemList/{0}");
                e.DialogButton("OrderEdit/0", ButtonActionTypes.Add);
            });

            builder.SetListMethod(GetOrderList);
        }

        public List<Order> GetOrderList(params object[] parameters)
        {
            var db = GetDbContext();
            var result = db.Get(typeof(Order)).Cast<Order>().ToList();
            return result;
        }
    }
}
