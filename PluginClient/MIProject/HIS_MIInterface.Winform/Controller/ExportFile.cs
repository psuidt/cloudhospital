using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.ComponentModel;
using System.Reflection;

namespace HIS_MIInterface.Winform.Controller
{
    public enum ExportType
    {
        [Description("Microsoft Excel|*.xls")]
        Excel,
        [Description("(*.in)|*.in|" + "(*.txt)|*.txt|" + "(*.*)|*.*")]
        Txt
    }

    public class ExportFile
    {
        string sExportName = string.Empty;//导出文件名称
        string sSaveFullPath = string.Empty;//导出全路径
        string sFloderPath = string.Empty;//导出文件夹路径
        const int nExcel2003RowMaxLimit = 65536;
        const int nExcel2003ColMaxLimit = 256;
        ExportType currentExportType;
        List<Dictionary<string, string>> columnNames;
        string sSeparator = "|";

        public ExportFile(string _sExportName, List<Dictionary<string, string>> _columnNames, string _sSeparator)
        {
            this.sExportName = _sExportName;
            this.columnNames = _columnNames;
            sSeparator = _sSeparator;
        }

        /// <summary>
        /// 初始化SaveFileDialog
        /// </summary>
        /// <param name="_dialogType">SaveFileDialogType【枚举】</param>
        /// <returns>{确认导出：True;否则:false}</returns>
        public bool InitShowDialog(ExportType _dialogType)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.FileName = sExportName;
            string sFilter = EnumOperate.GetDescriptionFromEnumValue(_dialogType);
            dlgSave.Filter = sFilter;
            if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentExportType = _dialogType;
                sSaveFullPath = dlgSave.FileName;//C:\Users\Administrator\Desktop\发现LTULFI设备_20130520142107.xls
                sFloderPath = sSaveFullPath.Substring(0, sSaveFullPath.LastIndexOf(@"\") + 1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="_dtExport">需要导出的Datatable</param>
        /// <returns>{导出成功：True;否则:false}</returns>
        public bool DoExportWork(List<DataTable> _dtExports)
        {
            bool _dExportStatus = false;
            switch (currentExportType)
            {
                case ExportType.Excel:
                    _dExportStatus = true;
                    ExportToExecel(_dtExports, "", sExportName);
                    break;
                case ExportType.Txt:
                    _dExportStatus = true;
                    ExportToTxt(_dtExports);
                    break;
            }
            return _dExportStatus;
        }

        /// <summary>
        /// 导出为Excel
        /// </summary>
        /// <param name="_dtExport">需要导出的Datatable</param>
        /// <param name="_sSheetName">Excel中SheetName名称</param>
        private void ExportToExecel(List<DataTable> _dtExportList, string typeName, string _sSheetName)
        {
            try
            {
                //for (int i = 0; i < _dtExports.Count; i++)
                //{
                //    ExcelHelper.Export(_dtExports[i], "HIS目录", columnNames[i], sSaveFullPath);
                //}
                ExcelHelper.Export(_dtExportList, "HIS目录", columnNames, sSaveFullPath);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("导出到Excel失败，原因:{0}", ex.Message.Trim()));
            }
        }
        /// <summary>
        /// 导出到文本文件
        /// </summary>
        /// <param name="_dtExport">需要导出的Datatable</param>
        private void ExportToTxt(List<DataTable> _dtExports)
        {
            try
            {
                int _nRowIndex = 1;
                using (StreamWriter _objWriter = new StreamWriter(sSaveFullPath, false, System.Text.Encoding.UTF8))
                {
                    for (int d = 0; d < _dtExports.Count; d++)
                    {
                        foreach (DataRow row in _dtExports[d].Rows)
                        {
                            _nRowIndex++;
                            List<string> ls = new List<string>(columnNames[d].Keys);
                            string s = string.Empty;
                            for (int i = 0; i < ls.Count; i++)
                            {
                                s += (ls[i] == String.Empty ? "" : row[ls[i].ToString()].ToString().Trim()) + sSeparator;
                            }
                            _objWriter.WriteLine(s.Substring(0, s.Length - 1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("导出到文本文件失败，原因:{0}", ex.Message.Trim()));
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        public void OpenFile()
        {
            Cursor _currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = _currentCursor;
            if (MessageBox.Show("导出成功！是否打开该个文件吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process _process = new System.Diagnostics.Process();
                    _process.StartInfo.FileName = sSaveFullPath;
                    _process.StartInfo.Verb = "Open";
                    _process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    _process.Start();
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("未能打开该文件，原因:{0}", ex.Message.Trim()));
                }
            }
        }
    }

    public static class EnumOperate
    {
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

    }
}
