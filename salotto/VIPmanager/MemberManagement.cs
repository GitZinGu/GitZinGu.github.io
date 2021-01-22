using salotto.Tool;
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
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            vip = new VIPManager();
            vip.StartPosition = FormStartPosition.CenterParent;
            vip.ShowDialog();
            if (vip.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("操作成功");
                vip.Close();
                button1_Click(null, null);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource= vip.vip.SearchVip(textBox1.Text.Trim(),textBox2.Text.Trim(),textBox3.Text.Trim());
            SetdataGridViewHeaderCell();
        }

        private void SetdataGridViewHeaderCell()
        {
            dataGridView1.Columns[0].HeaderCell.Value = "卡号";   
            dataGridView1.Columns[1].HeaderCell.Value = "会员类型";
            dataGridView1.Columns[2].HeaderCell.Value = "姓名";
            dataGridView1.Columns[3].HeaderCell.Value = "手机号";
            dataGridView1.Columns[4].HeaderCell.Value = "余额";
            dataGridView1.Columns[5].HeaderCell.Value = "创建时间";
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count==0|| dataGridView1.SelectedRows.Count>1)
            {
                MessageBox.Show("请选择一行！");
                return;
            }
            VIPManagement vipm = new VIPManagement();
            vipm.VipCard= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            vipm.UserName= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            vipm.VipType= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            vipm.PhoneNumber= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            vipm.Balance= (int)dataGridView1.CurrentRow.Cells[4].Value;
            vipm.CreateTime = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            vip = new VIPManager(vipm);
            vip.StartPosition = FormStartPosition.CenterParent;
            vip.ShowDialog();
            if (vip.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("操作成功");
                vip.Close();
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
                    VIPManagement.DeleteVipUser(dataGridView1.Rows[i].Cells[0].Value.ToString());
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
            if (dataGridView1.DataSource==null|| dataGridView1.Rows.Count==0)
            {
                MessageBox.Show("没有可以导出的数据");
                return;
            }

            string url = "";
            //没有数据的话就不往下执行  
            if (dataGridView1.Rows.Count == 0)
                return;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Excel2007（*.xls）|*.xls|Excel2015（*.xlsx）|*.xlsx";
            save.FilterIndex = 1;
            if (save.ShowDialog() == DialogResult.OK) 
            {
                url = save.FileName;
                ExcelHelper.ExportExcel((DataTable)dataGridView1.DataSource,"vip" ,url);
                MessageBox.Show($"路径：{url}", "导出成功");
            }
                
        }                
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("请选择一行！");
                return;
            }
            VIPManagement vipm = new VIPManagement();
            vipm.VipCard = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            vipm.UserName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            vipm.VipType = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            vipm.PhoneNumber = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            vipm.Balance = (int)dataGridView1.CurrentRow.Cells[4].Value;
            vipm.CreateTime = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            AddBalance ab = new AddBalance(vipm);
            ab.StartPosition = FormStartPosition.CenterParent;
            ab.ShowDialog();
            button1_Click(null, null);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            string url = "";
            //没有数据的话就不往下执行  
            if (dataGridView1.Rows.Count == 0)
                return;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF（*.pdf）|*.pdf";
            save.FilterIndex = 1;
            if (save.ShowDialog() == DialogResult.OK)
            {
                url = save.FileName;
                PDFHelper.PrintPDF((DataTable)dataGridView1.DataSource, url);
                MessageBox.Show($"路径：{url}", "打印成功");
            }
                

        }

        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 || dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("请选择一行！");
                return;
            }
            VIPManagement vipm = new VIPManagement();
            vipm.VipCard = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            vipm.UserName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            vipm.VipType = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            vipm.PhoneNumber = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            vipm.Balance = (int)dataGridView1.CurrentRow.Cells[4].Value;
            vipm.CreateTime = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            Consumption ab = new Consumption(vipm);
            ab.StartPosition = FormStartPosition.CenterParent;
            ab.ShowDialog();
            button1_Click(null, null);
        }
    }
}
