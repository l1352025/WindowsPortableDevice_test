namespace Wpd_test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btFileRead = new System.Windows.Forms.Button();
            this.btFileWrite = new System.Windows.Forms.Button();
            this.btFileDelete = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btTest = new System.Windows.Forms.Button();
            this.lbLang = new System.Windows.Forms.Label();
            this.combLang = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btFileRead
            // 
            resources.ApplyResources(this.btFileRead, "btFileRead");
            this.btFileRead.Name = "btFileRead";
            this.btFileRead.UseVisualStyleBackColor = true;
            this.btFileRead.Click += new System.EventHandler(this.btFileRead_Click);
            // 
            // btFileWrite
            // 
            resources.ApplyResources(this.btFileWrite, "btFileWrite");
            this.btFileWrite.Name = "btFileWrite";
            this.btFileWrite.UseVisualStyleBackColor = true;
            this.btFileWrite.Click += new System.EventHandler(this.btFileWrite_Click);
            // 
            // btFileDelete
            // 
            resources.ApplyResources(this.btFileDelete, "btFileDelete");
            this.btFileDelete.Name = "btFileDelete";
            this.btFileDelete.UseVisualStyleBackColor = true;
            this.btFileDelete.Click += new System.EventHandler(this.btFileDelete_Click);
            // 
            // listBox1
            // 
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // btTest
            // 
            resources.ApplyResources(this.btTest, "btTest");
            this.btTest.Name = "btTest";
            this.btTest.UseVisualStyleBackColor = true;
            this.btTest.Click += new System.EventHandler(this.btTest_Click);
            // 
            // lbLang
            // 
            resources.ApplyResources(this.lbLang, "lbLang");
            this.lbLang.Name = "lbLang";
            // 
            // combLang
            // 
            this.combLang.FormattingEnabled = true;
            this.combLang.Items.AddRange(new object[] {
            resources.GetString("combLang.Items"),
            resources.GetString("combLang.Items1")});
            resources.ApplyResources(this.combLang, "combLang");
            this.combLang.Name = "combLang";
            this.combLang.SelectedIndexChanged += new System.EventHandler(this.combLang_SelectedIndexChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.combLang);
            this.Controls.Add(this.lbLang);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btTest);
            this.Controls.Add(this.btFileDelete);
            this.Controls.Add(this.btFileWrite);
            this.Controls.Add(this.btFileRead);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btFileRead;
        private System.Windows.Forms.Button btFileWrite;
        private System.Windows.Forms.Button btFileDelete;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btTest;
        private System.Windows.Forms.Label lbLang;
        private System.Windows.Forms.ComboBox combLang;
    }
}

