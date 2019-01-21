using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dal_XML_imp
    {
        private string Path = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)+"/data/test.xml";
        public bool Insert<T>(Type type, T Data)
        {
            return true;
        }
        public bool Update<T>(Type type, T Data)
        {
            return true;
        }
        public bool Del<T>(Type type, T Data)
        {
            return true;
        }
        public void GetOne<T>(Type type, string ID)
        {
            return;
        }
        public List<T> GetAll<T>(Type type)
        {
            return new List<T>();
        }
        public DataTable Read(/*string path*/)
        {
            DataTable table = new DataTable("Item");
            try
            {
                DataSet lstNode = new DataSet();
                lstNode.ReadXml(this.Path);
                table = lstNode.Tables["Item"];
                return table;
            }
            catch (Exception ex)
            {
                return table;
            }
        }

    }
    public enum Type
    {
        Tests = 1, Testers, Trainees
    }
}
