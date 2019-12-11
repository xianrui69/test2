using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace 文件搜索
{
    public partial class Form4 : Form
    {
        private List<string> _exts;
        private Dictionary<string, List<string>> _extsOnw = new Dictionary<string, List<string>>();
        List<string> 无后缀 = new List<string>();
        List<string> 分组 = new List<string>(){};
        Dictionary<string, List<string>> 自定义类别 = new Dictionary<string, List<string>>();
        public static Regex myRegex = null;
        public static string 大小的单位 = "B";
        public static decimal 左边的文件大小 = 0;
        public static decimal 右边的文件大小 = 0;
        private string[] 快捷选项 = new[] { "搜索三十天以上未动的文件" };
        public Form4(List<string> exts)
        {
            InitializeComponent();
            numericUpDown1.Maximum = numericUpDown2.Maximum = decimal.MaxValue;
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            comboBox1.SelectedIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            checkedListBox3.DataSource = 快捷选项;
            _exts = exts;
            _extsOnw.Add("all", new List<string>());
            _extsOnw.Add("无", 无后缀);
            _extsOnw.Add("分组", 分组);
            string ext;
            int i = 0;
            while (true)
            {
                ext = INI.ReadINI("分组", "分组" + i++ + "");
                if (!string.IsNullOrEmpty(ext))
                {
                    自定义类别.Add(ext, new List<string>());
                    分组.Add(ext);
                }
                else break;
            }
            foreach (var keyValuePair in 自定义类别)
            {
                i = 0;
                while (true)
                {
                    ext = INI.ReadINI(keyValuePair.Key, keyValuePair.Key + i++ + "");
                    if (!string.IsNullOrEmpty(ext))
                        keyValuePair.Value.Add(ext);
                    else break;
                }
            }
            exts.ForEach(_ext =>
            {
                if (_ext.Length > 0)
                {
                    if (!_extsOnw.ContainsKey(_ext[1] + ""))
                        _extsOnw.Add(_ext[1] + "", new List<string>());
                    _extsOnw[_ext[1] + ""].Add(_ext);
                }
                else 无后缀.Add(_ext);
            });
            
            //checkedListBox1.DataSource = exts;
            listBox1.DataSource = _extsOnw.Keys.ToList();
            //checkedListBox2.DataSource = 右边的选择数组数据源;
            listBox1.GotFocus += GetFocus;
            listBox1.LostFocus += LostFocus;
            checkedListBox1.GotFocus += GetFocus;
            checkedListBox1.LostFocus += LostFocus;
            checkedListBox2.GotFocus += GetFocus;
            checkedListBox2.LostFocus += LostFocus;
            checkedListBox1Hei = checkedListBox1.Height;
        }

        private int checkedListBox1Hei;
        void GetFocus(object sender, EventArgs e)
        {
            (sender as Control).Tag = (sender as Control).Height;
            (sender as Control).Height = Height - (sender as Control).Top - 50;
            if (sender == checkedListBox1 && listBox1.SelectedItem == "分组")
            {
                checkedListBox1.Tag = Text;
                Text = "双击控件，可为其添加子节点或同级节点";
            }
        }
        void LostFocus(object sender, EventArgs e)
        {
            try
            {
                (sender as Control).Height = int.Parse((sender as Control).Tag + "");
            }
            catch (Exception exception)
            {
                if (sender == checkedListBox1)
                    (sender as Control).Height = checkedListBox1Hei;
            }
            if ((sender as Control).Tag is string)
            {
                Text = (sender as Control).Tag + "";
                (sender as Control).Tag = null;
            }
        }

        public static string 文件名;
        public static DateTime 开始创建时间 = default(DateTime);
        public static DateTime 结束创建时间 = default(DateTime);
        public static DateTime 开始修改时间 = default(DateTime);
        public static DateTime 结束修改时间 = default(DateTime);
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                try
                {
                    myRegex = new Regex((sender as TextBox).Text, checkBox2.Checked ? RegexOptions.None : RegexOptions.IgnoreCase);
                }
                catch (Exception exception)
                {
                    myRegex = null;
                }
            }
            else myRegex = null;
            文件名 = (sender as TextBox).Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                var old = dateTimePicker1.Value;
                dateTimePicker1.Value = dateTimePicker2.Value;
                dateTimePicker2.Value = old;
            }
            开始创建时间 = (sender as DateTimePicker).Value;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                var old = dateTimePicker1.Value;
                dateTimePicker1.Value = dateTimePicker2.Value;
                dateTimePicker2.Value = old;
            }
            结束创建时间 = (sender as DateTimePicker).Value;
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value > dateTimePicker4.Value)
            {
                var old = dateTimePicker3.Value; 
                dateTimePicker3.Value = dateTimePicker4.Value;
                dateTimePicker4.Value = old;
            }
            开始修改时间 = (sender as DateTimePicker).Value;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker3.Value > dateTimePicker4.Value)
            {
                var old = dateTimePicker3.Value;
                dateTimePicker3.Value = dateTimePicker4.Value;
                dateTimePicker4.Value = old;
            }
            结束修改时间 = (sender as DateTimePicker).Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            左边的文件大小 = numericUpDown1.Value * (decimal)Math.Pow(1024, comboBox1.SelectedIndex);
            右边的文件大小 = numericUpDown2.Value * (decimal)Math.Pow(1024, comboBox1.SelectedIndex);
            _exts.Clear();
            foreach (string item in checkedListBox2.Items)
            {
                _exts.Add(item);
            }
            DialogResult = DialogResult.OK;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = 文件名;
            dateTimePicker1.Value = 开始创建时间;
            dateTimePicker2.Value = 结束创建时间;
            dateTimePicker3.Value = 开始修改时间;
            dateTimePicker4.Value = 结束修改时间;
            comboBox1.SelectedItem = 大小的单位;
            numericUpDown1.Value = 左边的文件大小 / (decimal)Math.Pow(1024, comboBox1.SelectedIndex);
            numericUpDown2.Value = 右边的文件大小 / (decimal)Math.Pow(1024, comboBox1.SelectedIndex);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.SelectedIndexChanged -= checkedListBox1_SelectedIndexChanged;
            if (listBox1.SelectedItem.ToString() == "all") checkedListBox1.DataSource = _exts;
            else if (listBox1.SelectedItem.ToString() == "无") checkedListBox1.DataSource = 无后缀;
            else checkedListBox1.DataSource = _extsOnw[(listBox1.SelectedItem.ToString())];
            for (var i = 0; i < checkedListBox1.CheckedItems.Count; i++)
            {
                checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(checkedListBox1.CheckedItems[i]), CheckState.Unchecked);
            }
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            foreach (string item in checkedListBox2.Items)
            {
                if (checkedListBox1.Items.Contains(item))
                {
                    checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(item), CheckState.Checked);
                }
            }
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemCheckState(checkedListBox1.SelectedIndex) == CheckState.Checked)
            {
                if (自定义类别.ContainsKey(checkedListBox1.SelectedItem + ""))
                {
                    foreach (var s in 自定义类别[checkedListBox1.SelectedItem + ""])
                    {
                        if (!checkedListBox2.Items.Contains(s))
                            checkedListBox2.Items.Add(s, true);
                    }
                }
                else 
                    checkedListBox2.Items.Add(checkedListBox1.SelectedItem + "", true);
                //右边的选择数组数据源.Add(checkedListBox1.SelectedItem + "");
            }
            else if (checkedListBox1.GetItemCheckState(checkedListBox1.SelectedIndex) == CheckState.Unchecked)
            {
                if (自定义类别.ContainsKey(checkedListBox1.SelectedItem + ""))
                {
                    foreach (var s in 自定义类别[checkedListBox1.SelectedItem + ""])
                    {
                        if (checkedListBox2.Items.Contains(s))
                            checkedListBox2.Items.Remove(s);
                    }
                }
                else 
                    checkedListBox2.Items.Remove(checkedListBox1.SelectedItem + "");
                //右边的选择数组数据源.Remove(checkedListBox1.SelectedItem + "");
            }
            //checkedListBox2.DataSource = _extsOnw[(listBox1.SelectedItem.ToString())];
            //checkedListBox2.ResetBindings();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(checkedListBox2.SelectedIndex == -1)return;
            var item = checkedListBox2.Items[checkedListBox2.SelectedIndex];
            if (checkedListBox2.GetItemCheckState(checkedListBox2.SelectedIndex) == CheckState.Checked)
            {
                if (checkedListBox1.Items.Contains(item))
                {
                    checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(item), CheckState.Checked);
                }
            }
            else if (checkedListBox2.GetItemCheckState(checkedListBox2.SelectedIndex) == CheckState.Unchecked)
            {
                if (checkedListBox1.Items.Contains(item))
                {
                    checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(item), CheckState.Unchecked);
                }
                checkedListBox2.Items.Remove(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var txt = Interaction.InputBox("请输入", "", "", -1, -1).Trim();
            if (txt == "") MessageBox.Show("不匹配");
            else MessageBox.Show("匹配" + (myRegex.Match(txt).Success ? "成功" : "失败"));
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            myRegex = new Regex(myRegex.ToString(), RegexOptions.None);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            大小的单位 = comboBox1.SelectedItem + "";
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 0) numericUpDown1.Value *= -1;
            if (numericUpDown1.Value > numericUpDown2.Value)
            {
                var old = numericUpDown1.Value;
                numericUpDown1.Value = numericUpDown2.Value;
                numericUpDown2.Value = old;
            }
            左边的文件大小 = numericUpDown1.Value * (decimal)Math.Pow(1024, comboBox1.SelectedIndex);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < 0) numericUpDown2.Value *= -1;
            if (numericUpDown1.Value > numericUpDown2.Value)
            {
                var old = numericUpDown1.Value;
                numericUpDown1.Value = numericUpDown2.Value;
                numericUpDown2.Value = old;
            }
            右边的文件大小 = numericUpDown2.Value * (decimal)Math.Pow(1024, comboBox1.SelectedIndex);
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (sender == checkedListBox1 && listBox1.SelectedItem == "分组")
            {
                checkedListBox2.Items.Clear();
                DialogResult dr = MessageBox.Show("是:添加子节点(必须先选择一个节点)；否：添加同级节点", "温馨提示",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    if (checkedListBox1.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("必须选择一个节点！");
                        return;
                    }
                    else if (checkedListBox1.CheckedItems.Count > 1)
                    {
                        dr = MessageBox.Show("添加子节点必须只选择一个节点，点击是则取消所有节点的选择", "温馨提示",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dr != DialogResult.Yes)
                            return;
                        for (var i = checkedListBox1.CheckedItems.Count - 1; i >= 0; i--)
                            checkedListBox1.SetItemCheckState(
                                checkedListBox1.Items.IndexOf(checkedListBox1.CheckedItems[i]),
                                CheckState.Unchecked);
                            
                        return;
                    }


                    var curKey = checkedListBox1.CheckedItems[0].ToString();
                    var txt = "";
                    while (true)
                    {
                        txt = Interaction.InputBox("请输入子节点名，首位字符必须为‘.’", "正在为【" + curKey + "】添加子节点", ".", -1, -1).Trim().ToLower();
                        if (txt == "")
                        {
                            dr = MessageBox.Show("是否取消录入节点", "温馨提示",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (dr == DialogResult.Yes)
                                return;
                        }
                        else if (自定义类别[curKey].Contains(txt))
                        {
                            MessageBox.Show("已有重名节点！");
                        }else break;
                    }
                    if (!自定义类别.ContainsKey(curKey))
                        自定义类别.Add(curKey, new List<string>());
                    INI.WriteINI(curKey,
                        curKey +
                        自定义类别[curKey].Count
                        , txt);
                    自定义类别[curKey].Add(txt);
                    
                }
                else if (dr == DialogResult.No)
                {
                    var txt = "";
                    while (true)
                    {
                        txt = Interaction.InputBox("请输入同级节点名称，不允许为空", "", "", -1, -1).Trim(); 
                        if (txt == "")
                        {
                            dr = MessageBox.Show("是否取消录入节点", "温馨提示",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (dr == DialogResult.Yes)
                                return;
                        }
                        else if (分组.Contains(txt))
                        {
                            MessageBox.Show("已有重名节点！");
                        }
                        else break;
                    }
                    INI.WriteINI("分组", "分组" + 分组.Count, txt);
                    分组.Add(txt);
                    MessageBox.Show("如需使用新节点，请重新选择左侧！");
                }
                foreach (string curKey in checkedListBox1.CheckedItems)
                    foreach (var s in 自定义类别[curKey])
                        checkedListBox2.Items.Add(s, true);
                
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = (sender as CheckBox).Checked;
            if (!dateTimePicker2.Enabled)
                开始创建时间 = 结束创建时间 = default(DateTime);
            else
            {
                开始创建时间 = dateTimePicker1.Value;
                结束创建时间 = dateTimePicker2.Value;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Enabled = dateTimePicker4.Enabled = (sender as CheckBox).Checked;
            if (!dateTimePicker3.Enabled)
                开始修改时间 = 结束修改时间 = default(DateTime);
            else
            {
                开始修改时间 = dateTimePicker3.Value;
                结束修改时间 = dateTimePicker4.Value;
            }
        }
    }
}

