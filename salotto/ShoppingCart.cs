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
    public struct ConsumptionInfo 
    {
        public string Goods { get; set; }
        public string GoodsNumber { get; set; }
        public string UnitPrice { get; set; }
        public string TotalPrice { get; set; }  

    }               

    public struct RecordsConsumption
    {
        /*会员信息*/
        public string VipCard { get; set; }
        public string Vtype { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Banlance { get; set; }
        /*消费信息*/
        public List<ConsumptionInfo> ls { get; set; }      
        public string ConsumptionTime { get; set; }   
        
    }

    public  class ShoppingCart
    {
        public ConsumptionInfo cinfo;
        public VIPManagement vinfo;
        private List<ConsumptionInfo> cifo;
        private readonly string filedir = Properties.Settings.Default.Shop;

        public ShoppingCart(VIPManagement _vip)
        {
            cifo = new List<ConsumptionInfo>();
            vinfo = _vip;      
        }

        //添加
        public DataTable AddGoods(ConsumptionInfo _cinfo)
        {
            bool isexist = false;
            foreach (var item in cifo)
            {
                if (item.Goods==_cinfo.Goods)
                {
                    isexist = true;
                    break;
                }
            }
            if (isexist)
            {
                ConsumptionInfo ci = new ConsumptionInfo();
                foreach (var item in cifo)
                {
                    if (item.Goods == _cinfo.Goods)
                    {
                        ci = item;
                        cifo.Remove(item);
                        break;
                    }
                }
                _cinfo.GoodsNumber = (Convert.ToInt32(_cinfo.GoodsNumber) + Convert.ToInt32(ci.GoodsNumber)).ToString();
                _cinfo.TotalPrice = (Convert.ToInt32(_cinfo.TotalPrice) + Convert.ToInt32(ci.TotalPrice)).ToString();
            }        
            cifo.Add(_cinfo);     
            return  ListToDatatableHelper.ToDataTable(cifo);
        }

        //结算 
        public void Settlement(DataTable dt,int TotalPrice)
        {
            //删除库存                                      
            Banlance bl = new Banlance(vinfo);
            bl.ExecAdd(vinfo.Balance - TotalPrice,BanlanceType.消费);
            //写入文件
            RecordsConsumption ls =new  RecordsConsumption();
            ls.VipCard = vinfo.VipCard;
            ls.Vtype = vinfo.VipType;
            ls.UserName = vinfo.UserName;
            ls.PhoneNumber  = vinfo.PhoneNumber;
            ls.Banlance = vinfo.PhoneNumber;
            ls.ls = new List<ConsumptionInfo>();
            ConsumptionInfo cinfo ;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cinfo = new ConsumptionInfo();
                cinfo.Goods = dt.Rows[i]["Goods"].ToString();
                cinfo.GoodsNumber = dt.Rows[i]["GoodsNumber"].ToString();
                cinfo.TotalPrice = dt.Rows[i]["TotalPrice"].ToString();
                cinfo.UnitPrice = dt.Rows[i]["UnitPrice"].ToString();
                ls.ls.Add(cinfo);
            }
            ls.ConsumptionTime = DateTime.Now.ToLocalTime().ToString();

            string filediry = filedir + @"/"+ls.VipCard;
            string fileurl = filediry + $"/{DateTime.Now.ToString("yyyyMMddhhmmss")}.txt";
            FileCreate.FileAndDirCreate(FileType.directory, filediry);
            //FileCreate.FileAndDirCreate(FileType.File, fileurl);
            File.WriteAllText(fileurl, JsonConvert.SerializeObject(ls));

        }

    }
}
