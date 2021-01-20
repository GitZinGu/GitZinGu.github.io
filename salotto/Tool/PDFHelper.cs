using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salotto.Tool
{
     public  class PDFHelper
    {    
        public static void PrintPDF(DataTable dt,string path)
        {
            //新建PDF文档
            Spire.Pdf.PdfDocument pdf = new Spire.Pdf.PdfDocument();
            //添加页面
            PdfPageBase page = pdf.Pages.Add();
            //创建表格
            PdfTable table = new PdfTable();

            //将DataGridView的数据导入到表格
            table.DataSource = dt;

            //显示表头（默认为不显示）
            table.Style.ShowHeader = true;

            //设置单元格内容与边框的间距
            table.Style.CellPadding = 2;

            //设置表格的布局 (超过一页自动将表格续写到下一页)
            PdfTableLayoutFormat tableLayout = new PdfTableLayoutFormat();
            tableLayout.Break = PdfLayoutBreakType.FitElement;
            tableLayout.Layout = PdfLayoutType.Paginate;

            //添加自定义事件
            table.BeginRowLayout += new BeginRowLayoutEventHandler(table_BeginRowLayout);

            //将表格绘制到页面并指定绘制的位置和范围
            table.Draw(page, new RectangleF(10, 50, 300, 300), tableLayout);

            pdf.SaveToFile(path);
            File.Open(path, FileMode.Open);
        }
        //在自定义事件中设置单元格字体和文本对齐方式
        private static void table_BeginRowLayout(object sender, BeginRowLayoutEventArgs args)
        {
            PdfCellStyle cellstyle = new PdfCellStyle();
            cellstyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            cellstyle.Font = new PdfTrueTypeFont(new Font("Arial Unicode MS", 9f), true);
            args.CellStyle = cellstyle;
        }

    }
}
