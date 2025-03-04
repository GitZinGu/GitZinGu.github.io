﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using salotto.Tool;

namespace salotto
{          
    [Serializable]
    public class VIPManagement
    {
        [NonSerialized]
        private List<VIPManagement> AllVIP = null;          
        private string vipcard;
        /// <summary>
        /// 卡号
        /// </summary>
        public string VipCard 
        {   
            get 
            { 
                return this.vipcard; 
            }
            set 
            {
                this.vipcard=value;
            } 
        }
        private string vtype;
        /// <summary>
        /// 会员类型
        /// </summary>
        public string VipType
        {
            get
            {
                return this.vtype;
            }
            set
            {
                this.vtype = value;
            }
        }
        private string username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }
        private string phonenumber;
        /// <summary>
        /// 手机号
        /// </summary>
        /// 
        public string PhoneNumber
        {
            get
            {
                return this.phonenumber;
            }
            set
            {
                this.phonenumber = value;
            }
        }

  
        private int balance;
        /// <summary>
        /// 余额
        /// </summary>
        public int Balance
        {
            get
            {
                return this.balance;
            }
            set
            {
                this.balance = value;
            }
        }

        /// <summary>
        /// 注册时间
        /// </summary>
        private string createtime;
        public string CreateTime 
        {
            get
            {
               return createtime; 
            }
            set
            {
                this.createtime = value;
            }
        }

        //通过ID获取信息
        public static VIPManagement GetVipInfo(string carid)
        {                                              
            string url = Properties.Settings.Default.VIP+ $"/{carid}";
            FileInfo f = new FileInfo(url);
            if (File.Exists(f.FullName))
            {
                string vipinfo = File.ReadAllText(f.FullName);
                return JsonConvert.DeserializeObject<VIPManagement>(vipinfo);
            }
            else
            {
                return null;
            }        
        }          

        /// <summary>
        /// 获取所有会员信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllVipInfo()
        {
            AllVIP = new List<VIPManagement>();
            string url = Properties.Settings.Default.VIP;
            FileInfo f = new FileInfo(url);
            if (!System.IO.Directory.Exists(f.FullName))
            {
                System.IO.Directory.CreateDirectory(f.FullName);//不存在就创建文件夹 } 
            }
            DirectoryInfo root = new DirectoryInfo(f.FullName);
            FileInfo[] files = root.GetFiles();
            foreach (var item in files)
            {
                string vipinfo = File.ReadAllText(item.FullName);
                AllVIP.Add(JsonConvert.DeserializeObject<VIPManagement>(vipinfo));
            }
            //获取文件目录
            return ListToDatatableHelper.ToDataTable(AllVIP);    
        }     

        //查询
        public DataTable SearchVip(string _carid,string _name, string phone)
        {
            string strTemp = "VipCard  like '%" + _carid + "%'"+" and UserName like '%" + _name + "%'"
                           + "and PhoneNumber like '%" + phone + "%'" ;
            DataTable dt = GetAllVipInfo();
            DataRow[] dr= dt.Select(strTemp);
            return ListToDatatableHelper.DataRowToDataTable(dr);
        }

        //新增
        public string AddVipUser(VIPManagement vip)
        {
            vip.CreateTime = DateTime.Now.ToLocalTime().ToString();
            string result = "ERROR:";
            GetAllVipInfo();
            foreach (var item in AllVIP)
            {
                if (item.VipCard== vip.VipCard)
                {
                    result += $"已存在卡号为{vip.VipCard}的会员信息！";
                    return result;   
                }
            }
            File.WriteAllText(Properties.Settings.Default.VIP+$"/{vip.VipCard}",JsonConvert.SerializeObject(vip));
            return "OK";
        
        }
        //修改
        public string EditVipUser(VIPManagement vip)
        {                                  
            File.Delete(Properties.Settings.Default.VIP + $"/{vip.VipCard}");
            File.WriteAllText(Properties.Settings.Default.VIP + $"/{vip.VipCard}", JsonConvert.SerializeObject(vip));
            return "OK";

        }

        //删除
        public static void DeleteVipUser(string VipCard)
        {
            File.Delete(Properties.Settings.Default.VIP + $"/{VipCard}");       
        }         

    }
}
