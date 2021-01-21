using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salotto
{
    public partial class PartChild : Form
    {

        string type = "";
        public PartChild()
        {
            InitializeComponent();
            this.Text = "商品新增";
            type = "新增";
        }
        public PartChild(Goods _goods)
        {
            InitializeComponent();
            this.Text = "商品修改";
            textBox1.Text = _goods.GoodName;
            textBox1.Enabled = false;
            numericUpDown1.Value = _goods.GoodPrice;
            type = "修改";
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            GoodsType gt = new GoodsType();
            Goods good = new Goods();
            good.GoodName = textBox1.Text;
            good.GoodPrice =(int)numericUpDown1.Value;
            switch (type)
            {
                case "新增":
                    gt.AddGoods(good);
                    break;
                case "修改":
                    gt.EditGoods(good);
                    break;
                default:
                    break;
            }
            this.DialogResult = DialogResult.OK;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
