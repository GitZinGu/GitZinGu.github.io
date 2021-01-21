using salotto.MouseHelper;
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
        bool toolstipshow = false;
        PanelEnhanced panel1 = null;
        MemberManagement mm = null;  //会员管理
        Part part = null; //商品管理


        public Form1()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            groupBox2.Visible = false;
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
        MouseHook mh = null;
        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            //MouserSet();
            LoadPanl();      
            SetBackGroundImage();   
            SetPanlLight();    
            SetToolStripImage();
            //隐藏ToolStrip
            TimerTool();
        }
        private void mh_MouseClickEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //MessageBox.Show(e.X + "-" + e.Y);

               // textBox1.Text = e.X.ToString();
               // textBox2.Text = e.Y.ToString();
            }
        }

        private void mh_MouseMoveEvent(object sender, MouseEventArgs e)
        {
            int x = e.Location.X;
            int y = e.Location.Y;
            if (toolstipshow)
            {
                if (x < this.Location.X || x > this.Location.X + this.Size.Width ||  y < this.Location.Y + 30 || y > this.Location.Y + 60)
                {
                    toolstipshow = false;
                    toolStrip1.Visible = toolstipshow;
                }
            }
            else
            {
                if (x >= this.Location.X && x <= this.Location.X + this.Size.Width && y == this.Location.Y + 30)
                {
                    toolstipshow = true;
                    toolStrip1.Visible = toolstipshow;
                }
            }
            /*
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
            textBox3.Text = this.Location.X.ToString() ;
            textBox4.Text = this.Location.Y.ToString();
            */
        }    
        private void MouserSet()
        {
            mh = new MouseHook();
            mh.SetHook();
            mh.MouseMoveEvent += mh_MouseMoveEvent;
            mh.MouseClickEvent += mh_MouseClickEvent;
        }

        System.Timers.Timer timer;
        private void TimerTool()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 2000;
            timer = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timer.AutoReset = false;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
            //绑定Elapsed事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(HiddenToolStrip);

        }
        private void HiddenToolStrip(object sender, System.Timers.ElapsedEventArgs e)
        {
            toolStrip1.Visible = toolstipshow;
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mh!=null)
            {
                mh.UnHook();
            }
            
        }
         /// <summary>
         /// 商品管理
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            part = new Part();
            part.Dock = DockStyle.Fill;
            part.TopLevel = false;
            panel1.Controls.Add(part);
            part.Show();
        }
    }
}
