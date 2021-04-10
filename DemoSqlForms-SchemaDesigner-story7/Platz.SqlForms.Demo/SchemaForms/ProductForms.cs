using PlatzDemo.SchemaStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platz.SqlForms.Demo.SchemaForms
{
    public class ProductForm : StoreDynamicEditFormBase<PlatzDemoDataContext>
    {
        protected override void Define(DynamicFormBuilder builder)
        {
            builder.Entity<Product>(e =>
            {
                e.Property(p => p.Id).IsReadOnly();
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Price).IsRequired();
                e.DialogButton(ButtonActionTypes.Cancel).DialogButton(ButtonActionTypes.Validate).DialogButton(ButtonActionTypes.Submit);
                e.DialogButtonNavigation("ProductList", ButtonActionTypes.Delete, ButtonActionTypes.Cancel, ButtonActionTypes.Submit);
            });
        }
    }

    public class ProductListForm : StoreDataServiceBase<PlatzDemoDataContext>
    {
        protected override void Define(DataServiceFormBuilder builder)
        {
            builder.Entity<Product>(e =>
            {
                e.ExcludeAll();
                e.Property(p => p.Id).IsPrimaryKey();
                e.Property(p => p.Name);
                e.Property(p => p.Price);
                e.ContextButton("Edit", "ProductEdit/{0}").ContextButton("Delete", "ProductDelete/{0}");
                e.DialogButton("ProductEdit/0", ButtonActionTypes.Add);
            });

            builder.SetListMethod(GetProductList);
        }

        public List<Product> GetProductList(params object[] parameters)
        {
            var db = GetDbContext();
            var result = db.Get(typeof(Product)).Cast<Product>().ToList();
            return result;
        }
    }
}
