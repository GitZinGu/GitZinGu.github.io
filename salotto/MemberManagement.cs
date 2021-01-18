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
    public partial class MemberManagement : Form
    {
        VIPManager vip;
        public MemberManagement()
        {
            InitializeComponent();
            vip = new VIPManager();  
        }

        private void button2_Click(object sender, EventArgs e)
        {               
            vip.ShowDialog();
            if (vip.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("操作成功");
                vip.Close(); 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= vip.vip.GetAllVipInfo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count==0|| dataGridView1.SelectedRows.Count>1)
            {
                MessageBox.Show("只能选择一行！");
            }
            VIPManagement vipm = new VIPManagement();
            vipm.VipCard= (int)dataGridView1.CurrentRow.Cells[0].Value;
            vipm.UserName= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            vipm.VipType= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            vipm.PhoneNumber= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            vipm.Balance= (int)dataGridView1.CurrentRow.Cells[5].Value;
            vip = new VIPManager(vipm);   
            if (vip.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("操作成功");
                vip.Close();
            }
        }
    }
}
