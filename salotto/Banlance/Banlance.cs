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
    enum BanlanceType
    {
        办理,
        消费,
        充值   
    }

    [Serializable]
    class Banlance
    {
        [NonSerialized]
        private VIPManagement svip;      
        private List<Banlance> AllBanlance = null;

        public string vipcard { get; set; }
        public string vtype { get; set; }
        public string username { get; set; }
        public string phonenumber { get; set; }
        public string CreateTime { get; set; } //充值时间
        public int addbanlance { get; set; } //充值金额
        public string info { get; set; }//信息

        public Banlance()
        {

        }
        public Banlance(VIPManagement vip)
        {
            this.svip = vip;
            this.vipcard = vip.VipCard;
            this.vtype = vip.VipType;
            this.username = vip.UserName;
            this.phonenumber = vip.PhoneNumber;
            if (AllBanlance == null)
            {
                AllBanlance = new List<Banlance>();
                string url = Properties.Settings.Default.History;
                if (!System.IO.Directory.Exists(url))
                {
                    System.IO.Directory.CreateDirectory(url);//不存在就创建文件夹  
                }
                string filefullname = Properties.Settings.Default.History + $"/{vip.VipCard}";
                if (File.Exists(filefullname))
                {
                    string vipinfo = File.ReadAllText(filefullname);
                    AllBanlance = JsonConvert.DeserializeObject<List<Banlance>>(vipinfo);

                }       
            }
        }

        /// <summary>
        /// 查看历史记录
        /// </summary>
        /// <returns></returns>
        public DataTable ShowHistory()
        {           
            return ListToDatatableHelper.ToDataTable(AllBanlance);
        }


        /// <summary>
        /// 执行充值
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string ExecAdd(int num, BanlanceType bt)
        {
            string s = "Error:";
            try
            {      
                addbanlance = num;
                CreateTime = DateTime.Now.ToLocalTime().ToString();
                info = bt.ToString();  
                AllBanlance.Add(this);
                FileInfo f = new FileInfo(Properties.Settings.Default.History + $"/{vipcard}");
                File.Delete(f.FullName);
                File.WriteAllText(f.FullName, JsonConvert.SerializeObject(AllBanlance));
                       
                if (bt== BanlanceType.充值)
                {
                    svip.Balance += num;
                    svip.EditVipUser(svip);
                }
                return "OK";
            }
            catch (Exception ex)
            {
                s += ex.Message;       
            }

            return s;
        }

    }
}
