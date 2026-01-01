using DevExpress.XtraReports.UI;
using ProductManager.Data;
using System;
using System.Linq;

namespace ProductManager.Reports
{
    public class ProductReport : XtraReport
    {
        public ProductReport()
        {
            using (var db = new AppDbContext())
            {
                DataSource = db.Products.ToList();
            }

            Bands.Add(new DetailBand());

            XRLabel name = new XRLabel { WidthF = 200 };
            name.ExpressionBindings.Add(
                new ExpressionBinding("Text", "[Name]"));

            XRLabel price = new XRLabel { LeftF = 220 };
            price.ExpressionBindings.Add(
                new ExpressionBinding("Text", "[Price]"));

            Bands[0].Controls.AddRange(new XRControl[] { name, price });
        }
    }
}
