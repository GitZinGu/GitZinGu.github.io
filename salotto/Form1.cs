using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salotto
{
    public partial class Form1 : Form
    {
        PanelEnhanced panel1 = null;
        MemberManagement mm = null;  //会员管理
        public Form1()
        {
            InitializeComponent();     
        }        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        } 
        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {               
            LoadPanl();      
            SetBackGroundImage();   
            SetPanlLight();    
            SetToolStripImage();
        }
        /// <summary>
        /// 设置工具栏图片
        /// </summary>
        private void SetToolStripImage()
        {
            toolStripButton1.Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\home.ico");
            toolStripButton2.Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\Vip.ico");
            toolStripButton3.Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\bill.ico");
            toolStripButton4.Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\data.ico");
            toolStripButton5.Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\Exit.ico");
        }      
        /// <summary>
        ///      添加控件
        /// </summary>
        private void LoadPanl()
        {          
            panel1 = new PanelEnhanced();
            panel1.Dock = DockStyle.Fill;
            this.Controls.Add(panel1);

        }
        /// <summary>
        /// 防止闪烁
        /// </summary>
        private void SetPanlLight()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }
        /// <summary>
        /// 设置背景图
        /// </summary>
        private void SetBackGroundImage()
        {                                     
                switch ((int)DateTime.Now.DayOfWeek)
                {
                    case 0:
                         panel1.BackgroundImage = Properties.Resources.funny_week7;
                        break;
                    case 1:
                         panel1.BackgroundImage = Properties.Resources.funny_week1;
                         break;
                    case 2:
                         panel1.BackgroundImage = Properties.Resources.funny_week2;
                         break;
                    case 3:
                         panel1.BackgroundImage = Properties.Resources.funny_week3;
                         break;
                    case 4:
                         panel1.BackgroundImage = Properties.Resources.funny_week4;
                         break;
                    case 5:
                         panel1.BackgroundImage = Properties.Resources.funny_week5;
                    break;
                        default:
                        panel1.BackgroundImage = Properties.Resources.funny_week6;
                    break;
                }  
        }

        /// <summary>
        /// 会员管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            mm = new MemberManagement();
            mm.Dock = DockStyle.Fill;
            mm.TopLevel = false;
            panel1.Controls.Add(mm);
            mm.Show();
        }
    }
}
