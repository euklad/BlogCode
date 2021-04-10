using PlatzDemo.SchemaStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platz.SqlForms.Demo.SchemaForms
{
    public class OrderItemForm : StoreDynamicEditFormBase<PlatzDemoDataContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<OrderItem>(e =>
            {
                e.ExcludeAll();
                e.Property(p => p.Id).IsReadOnly();
                e.Property(p => p.OrderId).IsFilter().IsHidden();
                e.Property(p => p.ProductId).Dropdown<Product>().Set(c => c.Id, c => c.Name).IsRequired();
                e.Property(p => p.Qty).IsRequired();
                e.DialogButton(ButtonActionTypes.Cancel).DialogButton(ButtonActionTypes.Validate).DialogButton(ButtonActionTypes.Submit);
                e.DialogButtonNavigation("OrderItemList/{1}", ButtonActionTypes.Delete, ButtonActionTypes.Cancel, ButtonActionTypes.Submit);
            });
        }
    }

    public class OrderItemListForm : PlatzDemoService
    {
        protected override void Define(DataServiceFormBuilder builder)
        {
            builder.Entity<OrderItemProduct>(e =>
            {
                e.ExcludeAll();
                e.Property(p => p.Id).IsPrimaryKey();
                e.Property(p => p.OrderId).IsFilter().IsHidden();
                e.Property(p => p.Name);
                e.Property(p => p.Price);
                e.Property(p => p.Qty);
                e.ContextButton("Edit", "OrderItemEdit/{0}/{1}").ContextButton("Delete", "OrderItemDelete/{0}/{1}");
                e.DialogButton("OrderList", ButtonActionTypes.Custom, "Back");
                e.DialogButton("OrderItemEdit/0/{1}", ButtonActionTypes.Add);
            });

            builder.SetListMethod(GetOrderItemProductList);
        }

    }
}
