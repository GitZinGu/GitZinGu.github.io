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
    public partial class Consumption : Form
    {
        List<Goods> listgood;
        GoodsType gt;
        ShoppingCart sp;
        public Consumption(VIPManagement v)
        {
            InitializeComponent();
            gt = new GoodsType();
            textBox1.Text = v.VipCard;
            textBox2.Text = v.UserName;
            textBox3.Text = v.VipType;
            textBox4.Text = v.Balance.ToString();
            textBox5.Text = v.PhoneNumber;
            groupBox1.Enabled = false;
            sp = new ShoppingCart(v);
        }

        private void init()
        {                                   
            
        }

        private void Consumption_Load(object sender, EventArgs e)
        {
            listgood = new List<Goods>();
            DataTable dt = gt.SearchGoods(null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["GoodName"].ToString());
                Goods gd = new Goods();
                gd.GoodName = dt.Rows[i]["GoodName"].ToString();
                gd.GoodPrice = Convert.ToInt32(dt.Rows[i]["GoodPrice"]);
                listgood.Add(gd);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in listgood)
            {
                if (item.GoodName== comboBox1.SelectedItem.ToString())
                {
                    textBox6.Text = item.GoodPrice.ToString();
                    label10.Text = (item.GoodPrice * numericUpDown1.Value).ToString();
                    break;
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int price = 0;
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                price = Convert.ToInt32(textBox6.Text);
            }
            label10.Text = (price * numericUpDown1.Value).ToString();
        }
        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            label11.Text = "0";
            init();
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ConsumptionInfo ci = new ConsumptionInfo();
            ci.Goods = comboBox1.Text;
            ci.GoodsNumber = numericUpDown1.Value.ToString();
            ci.UnitPrice = textBox6.Text;
            ci.TotalPrice = label10.Text;
            dataGridView1.DataSource = sp.AddGoods(ci);
            float s = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                s += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value); 
            }
            label11.Text = s.ToString();
            SetdataGridViewHeaderCell();
        }

        private void SetdataGridViewHeaderCell()
        {
            dataGridView1.Columns[0].HeaderCell.Value = "商品";
            dataGridView1.Columns[1].HeaderCell.Value = "数量";
            dataGridView1.Columns[2].HeaderCell.Value = "单价";
            dataGridView1.Columns[3].HeaderCell.Value = "总价";      
        }
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                DataTable dt =(DataTable) dataGridView1.DataSource;
                sp.Settlement(dt,Convert.ToInt32(label11.Text));  
            }
        }
    }

}
