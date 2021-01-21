using Newtonsoft.Json;
using salotto.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salotto
{
    public struct Goods
    {            
        private string goodname;
        public string GoodName
        {
            get { return goodname; }
            set { goodname = value; }
        }
        private int goodprice;
        public int GoodPrice
        {
            get { return goodprice; }
            set { goodprice = value; }
        }    
    }
    class GoodsType
    {
        private const string FILENAME = "GOOD.JSON";
        List<Goods> lgoods;
        Goods goods;
        string filedirectory;
        string filefullname;
        public GoodsType()
        {
            filedirectory = Properties.Settings.Default.Goods;
            FileCreate.FileAndDirCreate(FileType.directory, filedirectory);
            FileCreate.FileAndDirCreate(FileType.File, filedirectory + @"/" + FILENAME);
            filefullname = filedirectory + @"/" + FILENAME;
            lgoods = new List<Goods>();
        }
        public GoodsType(string name,int price)
        {
            goods.GoodName = name;
            goods.GoodPrice = price;
            filedirectory = Properties.Settings.Default.Goods;
            FileCreate.FileAndDirCreate(FileType.directory, filedirectory);
            FileCreate.FileAndDirCreate(FileType.File, filedirectory + @"/" + FILENAME);
            filefullname = filedirectory + @"/" + FILENAME;
            lgoods = new List<Goods>();
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        private void ReadFile()
        {
            
            string f = filedirectory + @"/" + FILENAME;
            if (File.Exists(f))
            {
                string Goodsinfo = File.ReadAllText(f);
                if (string.IsNullOrEmpty(Goodsinfo))
                {
                    return;
                }
                lgoods = JsonConvert.DeserializeObject<List<Goods>>(Goodsinfo);
            }    
            
                                                             
        }
        //查询
        public DataTable SearchGoods(string name)
        {
            ReadFile();
            string strTemp = "GoodName  like '%" + name + "%'";
            DataTable dt = ListToDatatableHelper.ToDataTable(lgoods);
            DataRow[] dr = dt.Select(strTemp);
            return ListToDatatableHelper.DataRowToDataTable(dr);

        }
        //新增
        public string AddGoods(Goods _good)
        {
            string result = "Error：新增失败，";
            try
            {
                ReadFile();
                foreach (var item in lgoods)
                {
                    if (item.GoodName==_good.GoodName)
                    {
                        return result+"已存在相同数据！";
                    }
                }
                lgoods.Add(_good);  
                File.WriteAllText(filefullname, JsonConvert.SerializeObject(lgoods));         
            }
            catch (Exception e)
            {
                return result += e.Message;
            }
            return "OK";
        
        }
        //修改
        public string EditGoods(Goods _good)
        {
            string result = "Error：修改失败";
            try
            {
                ReadFile();
                foreach (var item in lgoods)
                {
                    if (item.GoodName == _good.GoodName)
                    {
                        lgoods.Remove(item);
                        break;
                    }
                }
                lgoods.Add(_good);
                File.WriteAllText(filefullname, JsonConvert.SerializeObject(lgoods));
            }
            catch (Exception e)
            {
                return result += e.Message;
            }
            return "OK";
        }

        //删除
        public string DelGoods(string goodsid)
        {
            string result = "Error：删除失败，";
            try
            {
                ReadFile(); 
                foreach (var item in lgoods)
                {
                    if (item.GoodName== goodsid)
                    {
                        lgoods.Remove(item);
                        break;
                    }
                }
                File.WriteAllText(filefullname, JsonConvert.SerializeObject(lgoods));
            }
            catch (Exception e)
            {
                return result += e.Message;
            }
            return "OK";
        }



    }
}
