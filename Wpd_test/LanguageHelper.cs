using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Wpd_test
{
    class LanguageHelper
    {
        public static Dictionary<string, string> resources;

        #region 初始化多语言字典
        /// <summary>
        /// 从Json文件载入多语言
        /// </summary>
        /// <param name="language">en-US 默认位置：./lang/en-US</param>
        public static void LoadLanguageResouces(string jsonFileDir = "")
        {
            if (Directory.Exists(jsonFileDir))
            {
                resources = new Dictionary<string, string>();

                var jsonFiles = Directory.GetFiles(jsonFileDir, "*.json", SearchOption.AllDirectories);
                foreach (string file in jsonFiles)
                {
                    LoadFile(file);
                }
            }
        }

        private static void LoadFile(string file)
        {
            var content = File.ReadAllText(file, Encoding.UTF8);
            if (!string.IsNullOrEmpty(content))
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                foreach (string key in dict.Keys)
                {
                    //遍历集合如果语言资源键值不存在，则创建，否则更新
                    if (!resources.ContainsKey(key))
                    {
                        resources.Add(key, dict[key]);
                    }
                    else
                    {
                        resources[key] = dict[key];
                    }
                }
            }
        }

        /// <summary>
        /// 从已有字典载入多语言
        /// </summary>
        /// <param name="dict"></param>
        public static void LoadLanguageResouces(Dictionary<string, string> dict)
        {
            if(dict != null)
            {
                resources = dict;
            }
        }

        #endregion

        /// <summary>
        /// 初始化语言
        /// </summary>
        public static void InitLanguage(Control control)
        {
            //如果没有资源，那么不必遍历控件，提高速度
            if (resources == null)
                return;

            //使用递归的方式对控件及其子控件进行处理
            SetControlLanguage(control);
            foreach (Control ctrl in control.Controls)
            {
                InitLanguage(ctrl);
            }

            //工具栏或者菜单动态构建窗体或者控件的时候，重新对子控件进行处理
            control.ControlAdded += (sender, e) =>
            {
                InitLanguage(e.Control);
            };
        }

        private static void SetControlLanguage(Control ctrl)
        {
            ctrl.Text = GetString(ctrl.Name);
        }

        public static string GetString(string str)
        {
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (string.IsNullOrWhiteSpace(str) || language.Equals("zh-CN", StringComparison.OrdinalIgnoreCase))
            {
                return str;
            }

            if (resources == null)
            {
                return str;
            }

            return resources.FirstOrDefault(q => q.Key == str).Value;
        }

        #region 设置控件语言
        /// <summary>
        /// 内容的语言转化
        /// </summary>
        /// <param name="parent"></param>
        public static void SetControlLanguageText(System.Windows.Forms.Control parent)
        {
            if (parent.HasChildren)
            {
                foreach (System.Windows.Forms.Control ctrl in parent.Controls)
                {
                    SetContainerLanguage(ctrl);
                }
            }
            else
            {
                SetLanguage(parent);
            }
        }

        /// <summary>
        /// 设置容器类控件的语言
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private static void SetContainerLanguage(System.Windows.Forms.Control ctrl)
        {
            if (ctrl is DataGridView)
            {
                try
                {
                    DataGridView dataGridView = (DataGridView)ctrl;
                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        try
                        {
                            if (dgvc.HeaderText.ToString() != "" && dgvc.Visible)
                            {
                                dgvc.HeaderText = GetLanguageText(dgvc.HeaderText);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception)
                { }
            }
            if (ctrl is MenuStrip)
            {
                MenuStrip menuStrip = (MenuStrip)ctrl;
                foreach (ToolStripMenuItem toolItem in menuStrip.Items)
                {
                    try
                    {
                        toolItem.Text = GetLanguageText(toolItem.Text);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (toolItem.DropDownItems.Count > 0)
                        {
                            GetItemText(toolItem);
                        }
                    }
                }
            }
            else if (ctrl is TreeView)
            {
                TreeView treeView = (TreeView)ctrl;
                foreach (TreeNode node in treeView.Nodes)
                {
                    try
                    {
                        node.Text = GetLanguageText(node.Text);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (node.Nodes.Count > 0)
                        {
                            GetNodeText(node);
                        }
                    }
                }
            }
            else if (ctrl is TabControl)
            {
                TabControl tabCtrl = (TabControl)ctrl;
                try
                {
                    foreach (TabPage tabPage in tabCtrl.TabPages)
                    {
                        tabPage.Text = GetLanguageText(tabPage.Text);
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (ctrl is StatusStrip)
            {
                StatusStrip statusStrip = (StatusStrip)ctrl;
                foreach (ToolStripItem toolItem in statusStrip.Items)
                {
                    try
                    {
                        toolItem.Text = GetLanguageText(toolItem.Text);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        ToolStripDropDownButton tsDDBtn = toolItem as ToolStripDropDownButton;
                        if (tsDDBtn != null && tsDDBtn.DropDownItems.Count > 0)
                        {
                            GetItemText(tsDDBtn);
                        }
                    }
                }
            }
            else if (ctrl is ToolStrip)
            {
                ToolStrip statusStrip = (ToolStrip)ctrl;
                foreach (ToolStripItem toolItem in statusStrip.Items)
                {
                    try
                    {
                        toolItem.Text = GetLanguageText(toolItem.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else if (ctrl is CheckedListBox)
            {
                CheckedListBox chkListBox = (CheckedListBox)ctrl;
                try
                {
                    for (int n = 0; n < chkListBox.Items.Count; n++)
                    {
                        chkListBox.Items[n] = GetLanguageText(chkListBox.Items[n].ToString());
                    }
                }
                catch (Exception)
                { }
            }
            else if (ctrl is ComboBox)
            {
                ComboBox comboBox = (ComboBox)ctrl;
                try
                {
                    for (int n = 0; n < comboBox.Items.Count; n++)
                    {
                        comboBox.Items[n] = GetLanguageText(comboBox.Items[n].ToString());
                    }
                }
                catch (Exception)
                { }
            }


            if (ctrl.HasChildren)
            {
                foreach (System.Windows.Forms.Control c in ctrl.Controls)
                {
                    SetContainerLanguage(c);
                }
            }
            else
            {
                SetLanguage(ctrl);
            }

        }
        /// <summary>
        /// 设置普通控件的语言
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private static void SetLanguage(System.Windows.Forms.Control ctrl)
        {
            if (true)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)ctrl;
                    try
                    {
                        checkBox.Text = GetLanguageText(checkBox.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (ctrl is Label)
                {
                    Label label = (Label)ctrl;
                    try
                    {
                        label.Text = GetLanguageText(label.Text);
                    }
                    catch (Exception)
                    {
                    }
                }

                else if (ctrl is Button)
                {
                    Button button = (Button)ctrl;
                    try
                    {
                        button.Text = GetLanguageText(button.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (ctrl is GroupBox)
                {
                    GroupBox groupBox = (GroupBox)ctrl;
                    try
                    {
                        groupBox.Text = GetLanguageText(groupBox.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (ctrl is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)ctrl;
                    try
                    {
                        radioButton.Text = GetLanguageText(radioButton.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

        }
        /// <summary>
        /// 递归转化菜单
        /// </summary>
        /// <param name="menuItem"></param>
        private static void GetItemText(ToolStripDropDownItem menuItem)
        {
            foreach (ToolStripItem toolItem in menuItem.DropDownItems)
            {
                try
                {
                    toolItem.Text = GetLanguageText(toolItem.Text);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (toolItem is ToolStripDropDownItem)
                    {
                        ToolStripDropDownItem subMenuStrip = (ToolStripDropDownItem)toolItem;
                        if (subMenuStrip.DropDownItems.Count > 0)
                        {
                            GetItemText(subMenuStrip);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// 递归转化树
        /// </summary>
        /// <param name="menuItem"></param>
        private static void GetNodeText(TreeNode node)
        {

            foreach (TreeNode treeNode in node.Nodes)
            {
                try
                {
                    treeNode.Text = GetLanguageText(treeNode.Text);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (treeNode.Nodes.Count > 0)
                    {
                        GetNodeText(treeNode);
                    }
                }
            }
        }

        private static Dictionary<Object, string> DefaultResource = new Dictionary<Object, string>();
        private static string GetControlText(Object ctrl)
        {
            string ctrlText = "";

            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (language.Equals("zh-CN", StringComparison.OrdinalIgnoreCase)) 
            {
                // default language
                ctrlText = ((Control)ctrl).Text;
                if (DefaultResource.ContainsKey(ctrl) == false)
                {
                    DefaultResource.Add(ctrl, ctrlText);
                }
            }
            else 
            {
                // other language
                if (DefaultResource.ContainsKey(ctrl) == true)
                {
                    ctrlText = GetLanguageText(DefaultResource[ctrl]);
                }
            }

            return ctrlText;
        }

        /// <summary>
        /// 根据语言标识符得到转换后的值
        /// </summary>
        /// <param name="languageFlag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetLanguageText(string value)
        {
            string language = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if (string.IsNullOrWhiteSpace(value) || language.Equals("zh-Hans", StringComparison.OrdinalIgnoreCase))
            {
                return value;
            }

            if (resources == null)
            {
                resources = LangDict.dictEN;
            }

            return resources.FirstOrDefault(q => q.Key == value).Value;
        }

        #endregion
    }

    public class MultiLanguageFormBase : Form
    {
        public MultiLanguageFormBase()
        {
            if (!Thread.CurrentThread.CurrentUICulture.Name.Equals("zh-Hans", StringComparison.OrdinalIgnoreCase)) //如果是简体，则无需转换
            {
                base.TextChanged += MultiLanguageFormBase_TextChanged;
                base.Load += new System.EventHandler(this.MultiLanguageFormBase_Shown);
            }
        }

        private void MultiLanguageFormBase_TextChanged(object sender, EventArgs e)
        {
            this.Text = LanguageHelper.GetLanguageText(this.Text);
        }

        private void MultiLanguageFormBase_Shown(object sender, EventArgs e)
        {
            LanguageHelper.SetControlLanguageText(this);
            base.ControlAdded += MultiLanguageFormBase_ControlAdded;
        }

        private void MultiLanguageFormBase_ControlAdded(object sender, ControlEventArgs e)
        {
            LanguageHelper.SetControlLanguageText(e.Control);
        }

        /// <summary>
        /// 强制通知子控件改变消息
        /// </summary>
        /// <param name="target"></param>
        protected virtual void PerformChildrenChange(Control target)
        {
            LanguageHelper.SetControlLanguageText(target);
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="defaultButton"></param>
        /// <returns></returns>
        protected DialogResult MessageBoxShow(string text, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return MessageBox.Show(LanguageHelper.GetLanguageText(text), LanguageHelper.GetLanguageText(caption), buttons, icon, defaultButton);
        }
    }
}
