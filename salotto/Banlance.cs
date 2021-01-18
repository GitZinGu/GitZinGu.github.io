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
    [Serializable]
    class Banlance
    {
        [NonSerialized]
        private VIPManagement svip;
        [NonSerialized]
        private List<Banlance> AllBanlance = null;

        private int vipcard;
        private string vtype;
        private string username;
        private string phonenumber;
        private DateTime CreateTime; //充值时间
        private int addbanlance;  //充值金额
        private string info; //信息
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
                string url = Properties.Settings.Default.VIP;
                if (!System.IO.Directory.Exists(url))
                {
                    System.IO.Directory.CreateDirectory(url);//不存在就创建文件夹 } 
                }
                DirectoryInfo root = new DirectoryInfo(url);
                FileInfo[] files = root.GetFiles();
                foreach (var item in files)
                {
                    string vipinfo = File.ReadAllText(item.FullName);
                    AllBanlance.Add(JsonConvert.DeserializeObject<Banlance>(vipinfo));
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
        public string ExecAdd(int num,bool Create=false)
        {
            string s = "Error:";
            try
            {      
                addbanlance = num;
                CreateTime = DateTime.Now;
                info = Create ? "新增" : "充值";
                AllBanlance.Add(this);
                File.Delete(Properties.Settings.Default.VIP + $"/{vipcard}");
                File.WriteAllText(Properties.Settings.Default.VIP + $"/{vipcard}", JsonConvert.SerializeObject(AllBanlance));
                       
                if (!Create)
                {
                    svip.Balance += num;
                    svip.EditVipUser(svip);
                }
                s= "OK";
            }
            catch (Exception ex)
            {
                s += ex.Message;       
            }

            return s;
        }

    }
}
