using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Font = NPOI.SS.UserModel.Font;

namespace Excel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Button btn = new Button();
            btn.Location = new Point(100, 100);
            this.Controls.Add(btn);

            btn.Click += Btn_Click;

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        //public void Create()
        //{
        //    HSSFWorkbook book = new HSSFWorkbook();
        //    ISheet sheet = book.CreateSheet("Sheet1");

        //    IRow row = sheet.CreateRow(20);//index代表多少行
        //    row.HeightInPoints = 35;//行高
        //    ICell cell = row.CreateCell(0);//创建第一列
        //    cell.SetCellValue("设置单元格的值");



        //}

        /// <summary>
        /// 导出基本操作示例方法
        /// </summary>
        public static void ExportExcel()
        {
            //初始化一个新的HSSFWorkbook实例
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();

            //设置excel必须的文件属性（该属性用来存储 如作者、标题、标记、备注、主题等信息，右键可查看的属性信息）
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.DocumentSummaryInformation = dsi;
            hssfworkbook.SummaryInformation = si;


            //新建一个Workbook默认都会新建3个Sheet(标准的Excel文件有3个Sheet)。所以必须加入下面的创建Sheet的代码才能保证生成的文件正常
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.CreateSheet("new sheet");
            // hssfworkbook.CreateSheet("Sheet1");
            // hssfworkbook.CreateSheet("Sheet2");
            // hssfworkbook.CreateSheet("Sheet3");

            //建创行
            Row row1 = sheet.CreateRow(0);
            //建单元格，比如创建A1位置的单元格：
            row1.Height = 500;
            CellStyle s = hssfworkbook.CreateCellStyle();
            s.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
            s.FillPattern = FillPatternType.SOLID_FOREGROUND;

            //第一列
            Cell cell1 = row1.CreateCell(0);
            cell1.CellStyle = s;

            Font font = hssfworkbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 20;
            //设置字体加粗样式
            font.Boldweight = (short)FontBoldWeight.BOLD;
            cell1.CellStyle.SetFont(font);
            cell1.CellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            cell1.SetCellValue("《大明宫保护办月报月计划》——( 行政管理部)6月工作计划");
            cell1.CellStyle.BorderBottom = CellBorderType.THIN;
            cell1.CellStyle.BorderLeft = CellBorderType.THIN;
            cell1.CellStyle.BorderRight = CellBorderType.THIN;
            cell1.CellStyle.BorderTop = CellBorderType.THIN;

            sheet.SetColumnWidth(0, 4 * 256);
            //第二列
            Cell cell2 = row1.CreateCell(1);
            cell2.CellStyle = s;
            sheet.SetColumnWidth(1, 12 * 256);
            //第三列
            Cell cell3 = row1.CreateCell(2);
            cell3.CellStyle = s;
            sheet.SetColumnWidth(2, 20 * 256);
            //第四列
            Cell cell4 = row1.CreateCell(3);
            cell4.CellStyle = s;
            sheet.SetColumnWidth(3, 25 * 256);
            //第五列
            Cell cell5 = row1.CreateCell(4);
            cell5.CellStyle = s;
            sheet.SetColumnWidth(4, 35 * 256);
            //第六列
            Cell cell6 = row1.CreateCell(5);
            cell6.CellStyle = s;
            sheet.SetColumnWidth(5, 20 * 256);
            //第七列
            Cell cell7 = row1.CreateCell(6);
            cell7.CellStyle = s;
            sheet.SetColumnWidth(6, 20* 256);
            //第八列
            Cell cell8 = row1.CreateCell(7);
            cell8.CellStyle = s;
            sheet.SetColumnWidth(7, 20 * 256);
            //第9列
            Cell cell9 = row1.CreateCell(8);
            cell9.CellStyle = s;
            sheet.SetColumnWidth(8, 20 * 256);

            //第10列
            Cell cell10 = row1.CreateCell(9);
            cell10.CellStyle = s;
            sheet.SetColumnWidth(9, 20 * 256);

            //第11列
            Cell cell11 = row1.CreateCell(10);
            cell11.CellStyle = s;
            sheet.SetColumnWidth(10, 20 * 256);

            CellRangeAddress r = new CellRangeAddress(0, 0, 0, 10);
            sheet.AddMergedRegion(r);
            //sheet.AddMergedRegion(new NPOI.SS.Util.Region(0, 0, 0, 10));


            CreateRow2(hssfworkbook, sheet);

            CreateRow3_4(hssfworkbook,sheet);

            _1stModule _1StModule = new _1stModule() { _1stModuleName = "管理工作" };

            List<_2ndModule> _2NdModules = new List<_2ndModule>();
            _2ndModule _2NdModule = new _2ndModule { _2ndModuleName = "与战略地图要求相关" };
            DataItem item = new DataItem
            {
                Work = "保护办十年工作总结",
                Result = "30日前完成保护办十年工作总结（总结部分）",
                _1stWeek = "根据主要领导意见进行修改",
                _2ndWeek = "进行修改",
                _3rdWeek = "进行修改",
                _4thWeek = "完成总结",
                PersonInCharge = "雷博",
                Penaty = "50"
            };
            _2NdModule.datas = new List<DataItem>();
            _2NdModule.datas.Add(item);
            _2NdModules.Add(_2NdModule);

            _2NdModule = new _2ndModule { _2ndModuleName = "与制度、流程、标准、工具相关" };
            _2NdModule.datas = new List<DataItem>();
            _2NdModules.Add(_2NdModule);
            _2NdModule = new _2ndModule { _2ndModuleName = "与企业文化相关" };
            _2NdModule.datas = new List<DataItem>();
            _2NdModules.Add(_2NdModule);
            _2NdModule = new _2ndModule { _2ndModuleName = "与团队建设相关" };
            item = new DataItem
            {
                Work = "组织公文写作培训",
                Result = "30日前完成培训",
                _1stWeek = "",
                _2ndWeek = "与培训老师确定时间和内容",
                _3rdWeek = "与培训老师确定时间和内容",
                _4thWeek = "完成培训",
                PersonInCharge = "王倩",
                Penaty = "50"
            };
            _2NdModule.datas = new List<DataItem>();
            _2NdModule.datas.Add(item);
            _2NdModules.Add(_2NdModule);

            _1StModule._2ndModules = _2NdModules;

            CreateRowsByModules(_1StModule,sheet,hssfworkbook);


            //把这个HSSFWorkbook实例写入文件
            FileStream file = new FileStream("Example1.xls", FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        private static void CreateRowsByModules(_1stModule module, HSSFSheet sheet, HSSFWorkbook hssfworkbook)
        {
            int offset = 4;
            for  ( int i = 0; i <  module._2ndModules.Count; i++ ) {

                for (int j = 0; j < module._2ndModules[i].datas.Count; j++)
                {
                    offset++;
                    Row row2 = sheet.CreateRow(offset);

                    Font font = hssfworkbook.CreateFont();
                    font.FontName = "宋体";
                    font.FontHeightInPoints = 12;
                    //设置字体加粗样式
                    font.Boldweight = (short)FontBoldWeight.BOLD;

                    CellStyle style = hssfworkbook.CreateCellStyle();
                    //设置边框
                    style.BorderTop = CellBorderType.THIN;
                    style.BorderBottom = CellBorderType.THIN;
                    style.BorderLeft = CellBorderType.THIN;
                    style.BorderRight = CellBorderType.THIN;
                    style.WrapText = true;
                    //设置单元格的样式：水平对齐居中
                    style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    style.VerticalAlignment = VerticalAlignment.CENTER;

                    style.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
                    style.FillPattern = FillPatternType.SOLID_FOREGROUND;
                    //使用SetFont方法将字体样式添加到单元格样式中 
                    style.SetFont(font);

                    var cell = row2.CreateCell(3);
                    cell.SetCellValue(module._2ndModules[i].datas[j].Work);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(4);
                    cell.SetCellValue(module._2ndModules[i].datas[j].Result);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(5);
                    cell.SetCellValue(module._2ndModules[i].datas[j]._1stWeek);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(6);
                    cell.SetCellValue(module._2ndModules[i].datas[j]._2ndWeek);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(7);
                    cell.SetCellValue(module._2ndModules[i].datas[j]._3rdWeek);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(8);
                    cell.SetCellValue(module._2ndModules[i].datas[j]._4thWeek);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(9);
                    cell.SetCellValue(module._2ndModules[i].datas[j].PersonInCharge);
                    cell.CellStyle = style;

                    cell = row2.CreateCell(10);
                    cell.SetCellValue(module._2ndModules[i].datas[j].Penaty);
                    cell.CellStyle = style;
                }

            }


        }

        private static void CreateRow3_4(HSSFWorkbook hssfworkbook, HSSFSheet sheet)
        {
            Font font = hssfworkbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 12;
            //设置字体加粗样式
            font.Boldweight = (short)FontBoldWeight.BOLD;

            CellStyle style = hssfworkbook.CreateCellStyle();
            //设置边框
            style.BorderTop = CellBorderType.THIN;
            style.BorderBottom = CellBorderType.THIN;
            style.BorderLeft = CellBorderType.THIN;
            style.BorderRight = CellBorderType.THIN;
            //设置单元格的样式：水平对齐居中
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
            style.VerticalAlignment = VerticalAlignment.CENTER;

            style.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
            style.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //使用SetFont方法将字体样式添加到单元格样式中 
            style.SetFont(font);

            //建创行
            Row row2 = sheet.CreateRow(2);

            string[] strs = new string[] { "NO", "一级模块", "二级模块", "工作\n安排", "结果定义" };
            for (int i = 0; i < strs.Length; i++) {
                Cell cell = row2.CreateCell(i);
                //cell.CellStyle.SetFont(font);
                cell.SetCellValue(strs[i]);
                cell.CellStyle = style;
            }

           var newcell =  row2.CreateCell(5);
            newcell.CellStyle = style;
            newcell.SetCellValue("过程节点和完成时间");
            newcell = row2.CreateCell(6);
            newcell = row2.CreateCell(7);
            newcell = row2.CreateCell(8);
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 5, 2, 8));

            newcell = row2.CreateCell(9);
            newcell.CellStyle = style;
            newcell.SetCellValue("责任人");

            newcell = row2.CreateCell(10);
            newcell.CellStyle = style;
            newcell.SetCellValue("自罚承诺");

            Row  row3  = sheet.CreateRow(3);
            string[] strs2 = new string[] { "", "", "", "", "可衡量、有价值、看得见、摸得着", "第一周", "第二周", "第三周", "第四周", "", "" };

            for (int i = 0; i < strs2.Length; i++)
            {
                Cell cell = row3.CreateCell(i);
                //cell.CellStyle.SetFont(font);
                cell.SetCellValue(strs2[i]);
                cell.CellStyle = style;
            }

            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 0, 3, 0));
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 1, 3, 1));
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 2, 3, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 3, 3, 3));
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 9, 3, 9));
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(2, 10, 3, 10));
        }

        public static void CreateRow2(HSSFWorkbook hssfworkbook, HSSFSheet sheet) {

            NPOI.SS.UserModel.Font font = hssfworkbook.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 12;
            //设置字体加粗样式
            font.Boldweight = (short)FontBoldWeight.BOLD;

            CellStyle style = hssfworkbook.CreateCellStyle();
            //设置边框
            style.BorderTop = CellBorderType.THIN;
            style.BorderBottom = CellBorderType.THIN;
            style.BorderLeft = CellBorderType.THIN;
            style.BorderRight = CellBorderType.THIN;
            //设置单元格的样式：水平对齐居中
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;

            style.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
            style.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //使用SetFont方法将字体样式添加到单元格样式中 
            style.SetFont(font);
            
            
            //建创行
            Row row2 = sheet.CreateRow(1);
            row2.Height = 350;
            //第一列
            Cell cell = row2.CreateCell(0);
            cell.CellStyle.SetFont(font);
            cell.SetCellValue("报告人");
            cell.CellStyle = style;

            cell = row2.CreateCell(1);
            cell.CellStyle = style;
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(1, 0, 1, 1));

            cell = row2.CreateCell(2);
            cell.CellStyle = style;
            cell.SetCellValue("雷博");

            cell = row2.CreateCell(3);
            cell.CellStyle = style;
            cell.SetCellValue("部门");

            cell = row2.CreateCell(4);
            cell.CellStyle = style;
            cell.SetCellValue("行政管理部");

            cell = row2.CreateCell(5);
            cell.CellStyle = style;
            cell.SetCellValue("开始日期");

            cell = row2.CreateCell(6);
            cell.CellStyle = style;
            cell.SetCellValue("2019.6.1");
            cell = row2.CreateCell(7);
            cell.CellStyle = style;
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(1, 6, 1, 7));

            cell = row2.CreateCell(8);
            cell.CellStyle = style;
            cell.SetCellValue("结束日期");

            cell = row2.CreateCell(9);
            cell.CellStyle = style;
            cell.SetCellValue("2019.6.30");
            cell = row2.CreateCell(10);
            cell.CellStyle = style;
            sheet.AddMergedRegion(new NPOI.SS.Util.Region(1, 9, 1, 10));

        }

        //public static void Ex2() {
        //    //说明：设置列宽、行高与合并单元格

        //    //1.创建EXCEL中的Workbook         
        //    HSSFWorkbook myworkbook = new HSSFWorkbook();

        ////2.创建Workbook中的Sheet        
        //ISheet mysheet = myworkbook.CreateSheet("sheet1");

        ////4.合并单元格区域 例： 第1行到第1行 第2列到第3列围成的矩形区域
        //mysheet.AddMergedRegion(new CellRangeAddress(0, 0, 1, 2));

        //    //5.创建Row中的Cell并赋值
        //    IRow row0 = mysheet.CreateRow(0);
        //row0.CreateCell(0).SetCellValue("0-0");
        //row0.CreateCell(1).SetCellValue("0-1");
        //row0.CreateCell(3).SetCellValue("0-3");

        ////6.设置列宽   SetColumnWidth(列的索引号从0开始, N * 256) 第二个参数的单位是1/256个字符宽度。例：将第四列宽度设置为了30个字符。
        //mysheet.SetColumnWidth(3, 30 * 256);

        //    //7.设置行高   Height的单位是1/20个点。例：设置高度为50个点
        //    row0.Height=50*20;
         
        //    //5.保存       
        //    FileStream file = new FileStream(@"myworkbook2.xls", FileMode.Create);
        //myworkbook.Write(file);
        //    file.Close();

        //    }

        private void button1_Click(object sender, EventArgs e)
        {
            //Ex2();
        }

        ///// <summary>
        ///// 导出Excel
        ///// </summary>
        ///// <param name="DeptId"></param>
        //public void ExportToExcel(int DeptId, List<FM_CostApply> CostApplyList, int beginYear, int beginMonth, int endYear, int endMonth)
        //{
        //    foreach (var Item in CostApplyList)
        //    {
        //        CostApplyItem.AddRange(Item.FM_CostApplyItem);
        //    }
        //    var Project = CostApplyItem.GroupBy(a => a.FM_Project.ProjectName).ToList();
        //    //创建工作簿
        //    HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        //    string[] headName = { "年度", "月", "日", "申请类型", "新科目名称", "部门名称", "项目名称", "凭证号", "摘要", "金额" };
        //    string[] ColumnName = { "Year", "Month", "Day", "Type", "SubJectName", "DeptName", "ProjectName", "CardNum", "Summary", "Cost" };
        //    //创建Sheet页
        //    if (Project.Count > 0)
        //    {
        //        foreach (var proc in Project)
        //        {
        //            //该项目下申请的所有的科目
        //            var SubjectName = CostApplyItem.Where(a => a.FM_Project.ProjectName == proc.Key).GroupBy(a => a.FM_SecondSubject.SubjectName).ToList();
        //            try
        //            {
        //                //创建Sheet页
        //                ISheet sheet = hssfworkbook.CreateSheet(proc.Key);

        //                //获取项目下的费用明细
        //                List<CostApplyExcel> model = GetCostApply(proc.Key, CostApplyList);

        //                var Dic = model.GroupBy(a => a.SubJectName).ToDictionary(w => w.Key, r => r.ToList());

        //                //集合转换为DataTable
        //                DataTable dt = ConvtToDataTable.ToDataTable<CostApplyExcel>(model);

        //                int RowIndex = 2;

        //                #region  如果为第一行
        //                IRow IRow = sheet.CreateRow(0);
        //                for (int h = 0; h < 10; h++)
        //                {
        //                    ICell Icell = IRow.CreateCell(h);
        //                    Icell.SetCellValue(BeginDate.ToString("yyyy.MM") + "-" + EndDate.ToString("yyyy.MM") + " " + proc.Key + "项目汇总表");

        //                    ICellStyle style = hssfworkbook.CreateCellStyle();
        //                    //设置单元格的样式：水平对齐居中
        //                    style.Alignment = HorizontalAlignment.CENTER;
        //                    //新建一个字体样式对象
        //                    IFont font = hssfworkbook.CreateFont();
        //                    font.FontName = "宋体";
        //                    font.FontHeightInPoints = 18;
        //                    //设置字体加粗样式
        //                    font.Boldweight = (short)FontBoldWeight.BOLD;
        //                    //使用SetFont方法将字体样式添加到单元格样式中 
        //                    style.SetFont(font);
        //                    //将新的样式赋给单元格
        //                    Icell.CellStyle = style;
        //                    //合并单元格
        //                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 9));
        //                }
        //                #endregion

        //                #region   表头
        //                IRow Irows2 = sheet.CreateRow(1);
        //                for (int j = 0; j < 10; j++)
        //                {
        //                    ICell Icell2 = Irows2.CreateCell(j);
        //                    ICellStyle Istyle2 = hssfworkbook.CreateCellStyle();
        //                    //设置边框
        //                    Istyle2.BorderTop = BorderStyle.THIN;
        //                    Istyle2.BorderBottom = BorderStyle.THIN;
        //                    Istyle2.BorderLeft = BorderStyle.THIN;
        //                    Istyle2.BorderRight = BorderStyle.THIN;
        //                    //设置单元格的样式：水平对齐居中
        //                    Istyle2.Alignment = HorizontalAlignment.CENTER;
        //                    //新建一个字体样式对象
        //                    IFont Ifont2 = hssfworkbook.CreateFont();
        //                    Ifont2.FontName = "宋体";
        //                    Ifont2.FontHeightInPoints = 11;
        //                    //设置字体加粗样式
        //                    Ifont2.Boldweight = (short)FontBoldWeight.BOLD;
        //                    //使用SetFont方法将字体样式添加到单元格样式中 
        //                    Istyle2.SetFont(Ifont2);
        //                    //将新的样式赋给单元格
        //                    Icell2.CellStyle = Istyle2;
        //                    Icell2.SetCellValue(headName[j]);
        //                }
        //                #endregion

        //                foreach (var DicItem in Dic)
        //                {
        //                    int SumStartRows = RowIndex + 1;         //求和的开始行 
        //                    //集合转换为DataTable
        //                    DataTable table = ConvtToDataTable.ToDataTable<CostApplyExcel>(DicItem.Value);
        //                    for (int i = 0; i <= DicItem.Value.Count; i++)
        //                    {
        //                        IRow row = sheet.CreateRow(RowIndex);

        //                        if (i == DicItem.Value.Count)
        //                        {
        //                            for (int j = 0; j < 10; j++)
        //                            {
        //                                if (j == 3)
        //                                {
        //                                    #region      汇总求和文字
        //                                    ICell cell = row.CreateCell(j);

        //                                    DataRow TableRow = table.Rows[i - 1];
        //                                    string subName = TableRow[5].ToString();

        //                                    ICellStyle style = hssfworkbook.CreateCellStyle();
        //                                    //设置边框
        //                                    style.BorderTop = BorderStyle.THIN;
        //                                    style.BorderBottom = BorderStyle.THIN;
        //                                    style.BorderLeft = BorderStyle.THIN;
        //                                    style.BorderRight = BorderStyle.THIN;
        //                                    //设置单元格的样式：水平对齐居中
        //                                    style.Alignment = HorizontalAlignment.CENTER;
        //                                    //新建一个字体样式对象
        //                                    IFont font = hssfworkbook.CreateFont();
        //                                    font.FontName = "宋体";
        //                                    font.FontHeightInPoints = 11;
        //                                    //设置字体加粗样式
        //                                    font.Boldweight = (short)FontBoldWeight.BOLD;
        //                                    //使用SetFont方法将字体样式添加到单元格样式中 
        //                                    style.SetFont(font);
        //                                    //将新的样式赋给单元格
        //                                    cell.CellStyle = style;
        //                                    cell.SetCellValue(subName + " 汇总");
        //                                    #endregion
        //                                }
        //                                else if (j == 9)            //合计
        //                                {
        //                                    #region      汇总求和公式插入
        //                                    ICell cell = row.CreateCell(j);
        //                                    ICellStyle style = hssfworkbook.CreateCellStyle();
        //                                    //设置边框
        //                                    style.BorderTop = BorderStyle.THIN;
        //                                    style.BorderBottom = BorderStyle.THIN;
        //                                    style.BorderLeft = BorderStyle.THIN;
        //                                    style.BorderRight = BorderStyle.THIN;
        //                                    //设置单元格的样式：水平对齐居中
        //                                    style.Alignment = HorizontalAlignment.CENTER;
        //                                    //新建一个字体样式对象
        //                                    IFont font = hssfworkbook.CreateFont();
        //                                    font.FontName = "宋体";
        //                                    font.FontHeightInPoints = 11;
        //                                    //使用SetFont方法将字体样式添加到单元格样式中 
        //                                    style.SetFont(font);
        //                                    //将新的样式赋给单元格
        //                                    cell.CellStyle = style;

        //                                    string format = "sum(";
        //                                    for (int s = SumStartRows; s < (DicItem.Value.Count + SumStartRows); s++)
        //                                    {
        //                                        format += ("J" + s + ",");
        //                                    }
        //                                    format += ")";

        //                                    cell.SetCellFormula(format);

        //                                    #endregion      汇总求和
        //                                }
        //                                else
        //                                {
        //                                    #region      汇总求和-普通单元格
        //                                    ICell cell = row.CreateCell(j);
        //                                    ICellStyle style = hssfworkbook.CreateCellStyle();
        //                                    //设置边框
        //                                    style.BorderTop = BorderStyle.THIN;
        //                                    style.BorderBottom = BorderStyle.THIN;
        //                                    style.BorderLeft = BorderStyle.THIN;
        //                                    style.BorderRight = BorderStyle.THIN;
        //                                    //设置单元格的样式：水平对齐居中
        //                                    style.Alignment = HorizontalAlignment.CENTER;
        //                                    //新建一个字体样式对象
        //                                    IFont font = hssfworkbook.CreateFont();
        //                                    font.FontName = "宋体";
        //                                    font.FontHeightInPoints = 11;
        //                                    //使用SetFont方法将字体样式添加到单元格样式中 
        //                                    style.SetFont(font);
        //                                    //将新的样式赋给单元格
        //                                    cell.CellStyle = style;
        //                                    #endregion
        //                                }
        //                            }
        //                        }
        //                        else if (i < DicItem.Value.Count)
        //                        {
        //                            #region   插入值
        //                            DataRow TableRow = table.Rows[i];
        //                            for (int j = 0; j < 10; j++)
        //                            {
        //                                ICell cell = row.CreateCell(j);
        //                                ICellStyle style = hssfworkbook.CreateCellStyle();
        //                                //设置边框
        //                                style.BorderTop = BorderStyle.THIN;
        //                                style.BorderBottom = BorderStyle.THIN;
        //                                style.BorderLeft = BorderStyle.THIN;
        //                                style.BorderRight = BorderStyle.THIN;
        //                                //设置单元格的样式：水平对齐居中
        //                                style.Alignment = HorizontalAlignment.CENTER;
        //                                //设置单元格属性为文本
        //                                style.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
        //                                //新建一个字体样式对象
        //                                IFont font = hssfworkbook.CreateFont();
        //                                font.FontName = "宋体";
        //                                font.FontHeightInPoints = 11;
        //                                //使用SetFont方法将字体样式添加到单元格样式中 
        //                                style.SetFont(font);
        //                                //将新的样式赋给单元格
        //                                cell.CellStyle = style;
        //                                string val = TableRow[ColumnName[j]].ToString();
        //                                if (j == 9)
        //                                {
        //                                    double cost = double.Parse(val);
        //                                    cell.SetCellValue(cost);
        //                                }
        //                                else
        //                                {
        //                                    cell.SetCellValue(val);
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                        RowIndex++;
        //                    }
        //                }
        //                for (int h = 0; h < 9; h++)
        //                {
        //                    sheet.AutoSizeColumn(h);　　//会按照值的长短 自动调节列的大小
        //                }
        //            }
        //            catch (Exception ex) { }
        //        }
        //    }
        //    else
        //    {
        //        //创建Sheet页
        //        ISheet sheet = hssfworkbook.CreateSheet();
        //    }
        //    string Path = Server.MapPath("~/upload/财务导出");
        //    if (!System.IO.Directory.Exists(Path))
        //        System.IO.Directory.CreateDirectory(Path);
        //    string fileName = DateTime.Now.ToFileTime() + ".xls";
        //    using (FileStream file = new FileStream(Path + "\\" + fileName, FileMode.Create))
        //    {
        //        hssfworkbook.Write(file);　　//创建test.xls文件。
        //        file.Close();
        //        result = ConfigurationManager.AppSettings["Websitet"] + "upload/财务导出/" + fileName;
        //    }
        //    HttpContext context = System.Web.HttpContext.Current;
        //    context.Response.Write(result);
        //    context.Response.End();
        //}
    }



    public class _1stModule {
        public string _1stModuleName { get; set; }
        public List<_2ndModule> _2ndModules { get; set; }
    }

    public class _2ndModule {
        public string _2ndModuleName { get; set; }    
        public List<DataItem> datas { get; set; }
    }

    public class DataItem {
        public string Work { get; set; }
        public string Result { get; set; }
        public string _1stWeek { get; set; }
        public string _2ndWeek { get; set; }
        public string _3rdWeek { get; set; }
        public string _4thWeek { get; set; }
        public string PersonInCharge { get; set; }
        public string Penaty { get; set; }

    }
}
