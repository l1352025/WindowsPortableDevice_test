using ElectricPowerDebuger.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Wpd_test
{
    public partial class Form1 : Form
    {
        WpdHelper dev;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dev = new WpdHelper();

            textBox1.Text += dev.Demo_getDeviceInfo();

           
            MultiLanguage.InitLanguage(this);
            if ("zh-CN" == System.Globalization.CultureInfo.InstalledUICulture.Name)
            {
                combLang.SelectedIndex = 0;
            }
            else if ("en-US" == System.Globalization.CultureInfo.InstalledUICulture.Name)
            {
                combLang.SelectedIndex = 1;
            }
            
        }

        private void OnMsgUpdate(object sender, EventArgs e)
        {
            if(InvokeRequired)
            {
                Invoke(new EventHandler(OnMsgUpdate), sender, e);
                return;
            }
            textBox1.Text += (string)sender;
        }

        private void btFileRead_Click(object sender, EventArgs e)
        {
            textBox1.Text += dev.Demo_DirAndReadFile("UHFMsg.xls", "data\\UHFMsg.xls");
        }

        private void btFileWrite_Click(object sender, EventArgs e)
        {
            textBox1.Text += dev.Demo_DirAndWriteFile("data\\UHFMsg.xls", "/");
        }

        private void btFileDelete_Click(object sender, EventArgs e)
        {
            textBox1.Text += dev.Demo_DeleteFile("UHFMsg.xls");
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            textBox1.Text += MultiLanguage.GetCurrentText("这是一个字符串") + "\r\n";
        }

        private void combLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combLang.SelectedIndex < 0) return;

            string oldname = Thread.CurrentThread.CurrentUICulture.Name;

            if (combLang.SelectedIndex == 0)
            {
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("zh-CN");
            }
            else if (combLang.SelectedIndex == 1)
            {
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            }

            if (oldname != Thread.CurrentThread.CurrentUICulture.Name)
            {
                MultiLanguage.SetControlLanguageText(this);
            }
        }
    }
}
