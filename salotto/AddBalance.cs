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
    public partial class AddBalance : Form
    {
        VIPManagement vip;
        Banlance bl;
        public AddBalance(VIPManagement v)
        {
            InitializeComponent();
            vip = v;
            bl = new Banlance(vip);
            textBox1.Text = v.VipCard;
            textBox2.Text = v.UserName;
            textBox3.Text = v.VipType;
            textBox4.Text = v.Balance.ToString();
            textBox5.Text = v.PhoneNumber;
            groupBox1.Enabled = false;
            dataGridView1.DataSource = bl.ShowHistory();
            SetdataGridViewHeaderCell();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bl = new Banlance(vip);
            bl.ExecAdd((int)numericUpDown1.Value);
            textBox4.Text = (Convert.ToInt32(textBox4.Text)+ numericUpDown1.Value).ToString();
            dataGridView1.DataSource = bl.ShowHistory();
            SetdataGridViewHeaderCell();
        }
        /// <summary>
        /// 设置  dataGridView1列名
        /// </summary>
        private void SetdataGridViewHeaderCell()
        {
            dataGridView1.Columns[0].HeaderCell.Value = "卡号";
            dataGridView1.Columns[1].HeaderCell.Value = "会员类型";
            dataGridView1.Columns[2].HeaderCell.Value = "姓名";
            dataGridView1.Columns[3].HeaderCell.Value = "手机号";
            dataGridView1.Columns[4].HeaderCell.Value = "时间";
            dataGridView1.Columns[5].HeaderCell.Value = "余额";
            dataGridView1.Columns[6].HeaderCell.Value = "信息";
        }
    }
}
