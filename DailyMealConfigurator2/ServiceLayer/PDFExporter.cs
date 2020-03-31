using BusinessLayer.MealPlanner;
using BusinessLayer.Utility;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class PDFExporter
    {
        public async static void ExportToPDFAsync(string file, User user, List<MealTime> mealTimes, double totalCalories)
        {
            using (PdfDocument document = new PdfDocument())
            {
                PdfPage page = document.Pages.Add();
                PdfGraphics graphics = page.Graphics;
                PdfFont font1 = new PdfStandardFont(PdfFontFamily.Helvetica, 40);
                PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                PdfFont font3 = new PdfTrueTypeFont(new Font("Arial Unicode MS", 15), true);
                graphics.DrawString("Daily Food Ration", font2, PdfBrushes.DarkRed, new PointF(0, 0));
                PdfPen pdfPen = new PdfPen(Color.DarkBlue, 2);

                graphics.DrawLine(pdfPen, 0, 50, 600, 50);

                graphics.DrawString("User Data", font2, PdfBrushes.DarkSlateBlue, new PointF(0, 195));

                graphics.DrawString("Weight: " + user.Weight, font3, PdfBrushes.Black, new PointF(0, 220));
                graphics.DrawString("Height: " + user.Height, font3, PdfBrushes.Black, new PointF(0, 240));
                graphics.DrawString("Age: " + user.Age, font3, PdfBrushes.Black, new PointF(0, 260));
                graphics.DrawString("Daily Activiry: " + user.DailyActivity, font3, PdfBrushes.Black, new PointF(0, 280));

                graphics.DrawString("ARM: " + user.GetARM(), font3, PdfBrushes.Black, new PointF(300, 220));
                graphics.DrawString("BMR: " + user.GetBMR(), font3, PdfBrushes.Black, new PointF(300, 240));
                graphics.DrawString("Daily Calories Rate: " + user.GetDailyCaloriesRate(), font3, PdfBrushes.Black, new PointF(300, 260));

                graphics.DrawLine(pdfPen, 0, 310, 600, 310);

                graphics.DrawString("MealTimes", font2, PdfBrushes.DarkSlateBlue, new PointF(0, 320));

                int height = 360;

                foreach (var mealTime in mealTimes)
                {
                    height += 10;
                    graphics.DrawString(mealTime.Name, font2, PdfBrushes.PaleVioletRed, new PointF(0, height));
                    foreach (var product in mealTime.Products)
                    {
                        height += 20;
                        graphics.DrawString($"{product.Name}: {product.Gramms} gramms", font3, PdfBrushes.Black, new PointF(0, height));
                    }
                    height += 20;
                }

                graphics.DrawLine(pdfPen, 0, height + 20, 600, height + 20);

                graphics.DrawString("Total: " + Math.Round(totalCalories, 3) + " calories", font2, PdfBrushes.PaleVioletRed, new PointF(0, height + 40));

                await Task.Run(() => document.Save(file));
                document.Close(true);
            }
        }
    }
}
