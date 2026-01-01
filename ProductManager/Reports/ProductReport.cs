using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using ProductManager.Data;
using System;
using System.Drawing;
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
            var header = new PageHeaderBand
            {
                HeightF = 30
            };

            XRLabel hName = new XRLabel
            {
                Text = "Product Name",
                WidthF = 200,
                HeightF=30,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Borders = BorderSide.All,
                Padding = new PaddingInfo(5, 5, 0, 0)
            };

            XRLabel hPrice = new XRLabel
            {
                Text = "Price",
                LeftF = 200,
                WidthF = 100,
                HeightF = 30,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Borders = BorderSide.All,
                TextAlignment = TextAlignment.MiddleRight,
                Padding = new PaddingInfo(5, 5, 0, 0)
            };
            header.Controls.AddRange(new XRControl[] { hName, hPrice });
            Bands.Add(header);

            // ===== DETAIL (DATA ROWS) =====
            var detail = new DetailBand
            {
                HeightF = 25
            };

            XRLabel name = new XRLabel
            {
                WidthF = 200,
                HeightF = 25,
                Borders = BorderSide.All,
                TextAlignment = TextAlignment.MiddleLeft,
                Padding = new PaddingInfo(5, 5, 0, 0)
            };
            name.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "[Name]"));

            XRLabel price = new XRLabel
            {
                LeftF = 200,
                WidthF = 100,
                HeightF = 25,
                Borders = BorderSide.All,
                TextAlignment = TextAlignment.MiddleRight,
                Padding = new PaddingInfo(5, 5, 0, 0),
                TextFormatString = "{0:C}"
            };
            price.ExpressionBindings.Add(
                new ExpressionBinding("BeforePrint", "Text", "[Price]"));

            detail.Controls.AddRange(new XRControl[] { name, price });
            Bands.Add(detail);

        } 
    }
}
