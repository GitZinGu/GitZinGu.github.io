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
    struct Goods
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
        List<Goods> lgoods;
        Goods goods;
        string filedirectory;
        public GoodsType(string name,int price)
        {
            goods.GoodName = name;
            goods.GoodPrice = price;
            filedirectory = Properties.Settings.Default.Goods;
            lgoods = new List<Goods>();
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        private void ReadFile()
        {               
            FileInfo f = new FileInfo(filedirectory);
            if (File.Exists(f.FullName))
            {
                string Goodsinfo = File.ReadAllText(f.FullName);
                lgoods = JsonConvert.DeserializeObject<List<Goods>>(Goodsinfo);
            }
            
                                                             
        }
        //查询
        public DataTable SearchGoods(string name)
        {
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
                File.WriteAllText(filedirectory, JsonConvert.SerializeObject(lgoods));         
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
                    }
                }
                lgoods.Add(_good);
                File.WriteAllText(filedirectory, JsonConvert.SerializeObject(lgoods));
            }
            catch (Exception e)
            {
                return result += e.Message;
            }
            return "OK";
        }

        //删除
        public string DelGoods(List<Goods> goods)
        {
            string result = "Error：删除失败，";
            try
            {
                ReadFile();       
                lgoods= lgoods.Except(goods).ToList();
                File.WriteAllText(filedirectory, JsonConvert.SerializeObject(lgoods));
            }
            catch (Exception e)
            {
                return result += e.Message;
            }
            return "OK";
        }



    }
}
