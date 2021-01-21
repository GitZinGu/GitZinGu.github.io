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
    public partial class Part : Form
    {
        GoodsType GT;
        public Part()
        {
            InitializeComponent();
            GT = new GoodsType();
        }
         /// <summary>
         /// 查询
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {                          
            dataGridView1.DataSource= GT.SearchGoods(textBox1.Text.Trim());

        }
         /// <summary>
         /// 新增
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            PartChild pc = new PartChild();
            pc.ShowDialog();
            if (pc.DialogResult==DialogResult.OK)
            {
                button1_Click(null, null);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("请选择一行！");
                return;
            }
            Goods gd = new Goods();
            gd.GoodName = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            gd.GoodPrice = (int)dataGridView1.CurrentRow.Cells[1].Value;
            PartChild pc = new PartChild(gd);
            pc.ShowDialog();
            if (pc.DialogResult == DialogResult.OK)
            {
                button1_Click(null, null);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("请最少选择一行！");
                return;
            }
            if (MessageBox.Show("是否删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //删除
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    GT.DelGoods(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
                MessageBox.Show("执行成功");
                button1_Click(null, null);
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
