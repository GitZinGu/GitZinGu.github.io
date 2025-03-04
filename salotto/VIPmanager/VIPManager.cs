﻿using System;
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
    public partial class VIPManager : Form
    {
        public VIPManagement vip = null;
        private string Type=string.Empty;
        public VIPManager()
        {
            InitializeComponent();
            vip = new VIPManagement();
            Type = "新增";
        }
       public VIPManager(VIPManagement _vip)
        {
            InitializeComponent();
            vip = _vip;
            Type = "修改";
            textBox1.Text = vip.VipCard.ToString();
            textBox2.Text = vip.UserName.ToString();
            comboBox1.Text = vip.VipType.ToString();  
            textBox5.Text = vip.PhoneNumber.ToString();
            textBox6.Text = vip.Balance.ToString();
            textBox1.Enabled = false;
            textBox6.Enabled = false;       
        }

        private void button1_Click(object sender, EventArgs e)
        {              
            vip.VipCard = textBox1.Text.Trim();
            vip.UserName = textBox2.Text.Trim();
            vip.VipType =  comboBox1.SelectedItem.ToString().Trim();
            vip.PhoneNumber = textBox5.Text.Trim();
            vip.Balance = Convert.ToInt32(textBox6.Text.Trim());    
            if (Type=="新增")
            {
                vip.CreateTime = DateTime.Now.ToLocalTime().ToString();
                vip.AddVipUser(vip);
                Banlance bl = new Banlance(vip);
                bl.ExecAdd(Convert.ToInt32(textBox6.Text.Trim()),BanlanceType.办理);
            }
            else
            {                                     
                vip.EditVipUser(vip);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VIPManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar))&&e.KeyChar!=(char)8)
            {
                e.Handled = true;
            }
            else
            {
                return;
            }  
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
                return;
            }

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
                return;
            }
        }
    }
}
