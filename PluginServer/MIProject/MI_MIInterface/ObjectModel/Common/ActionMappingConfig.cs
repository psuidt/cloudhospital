using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MI_MIInterface.ObjectModel.Common
{
    public class ActionMappingConfig
    {
        private static System.Xml.XmlDocument xmlDoc = null;
        private static string configfile = System.Windows.Forms.Application.StartupPath + "\\ModulePlugin\\MIService\\ActionMapping.xml";
        private static void InitConfig()
        {
            xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.Load(configfile);
        }

        public static List<ActionObjectMapping> getActionList()
        {
            if (xmlDoc == null)
            {
                InitConfig();
            }
            else
            {
                xmlDoc.Load(configfile);
            }

            List<ActionObjectMapping> _List = new List<ActionObjectMapping>();
            System.Xml.XmlNodeList xnlist = xmlDoc.DocumentElement.SelectNodes("ActionMapping/action");
            foreach (XmlNode n in xnlist)
            {
                ActionObjectMapping action = new ActionObjectMapping();
                action.Name = n.Attributes["name"].Value;
                action.ObjectName = n.Attributes["actionclass"].Value;
                _List.Add(action);
            }
            return _List;
        }

        public static string GetHospitalDataValue(string actionclass, string hospitalId, string name)
        {
            if (xmlDoc == null)
            {
                InitConfig();
            }
            else
            {
                xmlDoc.Load(configfile);
            }
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("HospitalData/hospital[@actionclass='" + actionclass + "' and @hospitalId='" + hospitalId + "']/data[@name='" + name + "']");
            if (xn != null) return xn.Attributes["value"].Value;
            return null;
        }
        /// <summary>
        /// 获取配置文件中各个方法中的配置
        /// </summary>
        /// <param name="typeId">医保类型ID 与数据库一致</param>
        /// <param name="method">方法名</param>
        /// <param name="name">该方法所需的数据名</param>
        /// <returns></returns>
        public static string GetMedicalInsuranceData(int typeId, string method, string name)
        {
            if (xmlDoc == null)
            {
                InitConfig();
            }
            else
            {
                xmlDoc.Load(configfile);
            }
            XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("MedicalInsuranceData/MedicalInsurance[@typeId='" + typeId + "']/method[@name='" + method + "']/data[@name='" + name + "']");
            if (xn != null) return xn.Attributes["value"].Value;
            return "";
        }
    }


    public class ActionObjectMapping
    {
        public string Name { get; set; }
        public string ObjectName { get; set; }
    }
}
