namespace 文件搜索
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.打开目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.筛选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.扩展名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大小ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.最小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改时间ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.最小ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.最大ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.创建时间ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.最小时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最大时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.复制并解密节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(2, 40);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(796, 680);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.LightCoral;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(804, 40);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "搜索";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.搜索ToolStripMenuItem,
            this.刷新ToolStripMenuItem,
            this.分组ToolStripMenuItem,
            this.筛选ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 136);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开目录ToolStripMenuItem,
            this.打开文件ToolStripMenuItem,
            this.复制并解密节点ToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "打开";
            // 
            // 打开目录ToolStripMenuItem
            // 
            this.打开目录ToolStripMenuItem.Name = "打开目录ToolStripMenuItem";
            this.打开目录ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开目录ToolStripMenuItem.Text = "打开目录";
            this.打开目录ToolStripMenuItem.Click += new System.EventHandler(this.打开目录ToolStripMenuItem_Click);
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 搜索ToolStripMenuItem
            // 
            this.搜索ToolStripMenuItem.Name = "搜索ToolStripMenuItem";
            this.搜索ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.搜索ToolStripMenuItem.Text = "搜索";
            this.搜索ToolStripMenuItem.Click += new System.EventHandler(this.button3_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 分组ToolStripMenuItem
            // 
            this.分组ToolStripMenuItem.Name = "分组ToolStripMenuItem";
            this.分组ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.分组ToolStripMenuItem.Text = "分组";
            // 
            // 筛选ToolStripMenuItem
            // 
            this.筛选ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.扩展名ToolStripMenuItem,
            this.大小ToolStripMenuItem1,
            this.修改时间ToolStripMenuItem1,
            this.创建时间ToolStripMenuItem1});
            this.筛选ToolStripMenuItem.Name = "筛选ToolStripMenuItem";
            this.筛选ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.筛选ToolStripMenuItem.Text = "筛选";
            // 
            // 扩展名ToolStripMenuItem
            // 
            this.扩展名ToolStripMenuItem.Name = "扩展名ToolStripMenuItem";
            this.扩展名ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.扩展名ToolStripMenuItem.Text = "扩展名";
            // 
            // 大小ToolStripMenuItem1
            // 
            this.大小ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最小ToolStripMenuItem,
            this.最大ToolStripMenuItem});
            this.大小ToolStripMenuItem1.Name = "大小ToolStripMenuItem1";
            this.大小ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.大小ToolStripMenuItem1.Text = "大小";
            // 
            // 最小ToolStripMenuItem
            // 
            this.最小ToolStripMenuItem.Name = "最小ToolStripMenuItem";
            this.最小ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.最小ToolStripMenuItem.Text = "最小";
            // 
            // 最大ToolStripMenuItem
            // 
            this.最大ToolStripMenuItem.Name = "最大ToolStripMenuItem";
            this.最大ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.最大ToolStripMenuItem.Text = "最大";
            // 
            // 修改时间ToolStripMenuItem1
            // 
            this.修改时间ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最小ToolStripMenuItem1,
            this.最大ToolStripMenuItem1});
            this.修改时间ToolStripMenuItem1.Name = "修改时间ToolStripMenuItem1";
            this.修改时间ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.修改时间ToolStripMenuItem1.Text = "修改时间";
            // 
            // 最小ToolStripMenuItem1
            // 
            this.最小ToolStripMenuItem1.Name = "最小ToolStripMenuItem1";
            this.最小ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.最小ToolStripMenuItem1.Text = "最小";
            // 
            // 最大ToolStripMenuItem1
            // 
            this.最大ToolStripMenuItem1.Name = "最大ToolStripMenuItem1";
            this.最大ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.最大ToolStripMenuItem1.Text = "最大";
            // 
            // 创建时间ToolStripMenuItem1
            // 
            this.创建时间ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最小时间ToolStripMenuItem,
            this.最大时间ToolStripMenuItem});
            this.创建时间ToolStripMenuItem1.Name = "创建时间ToolStripMenuItem1";
            this.创建时间ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.创建时间ToolStripMenuItem1.Text = "创建时间";
            // 
            // 最小时间ToolStripMenuItem
            // 
            this.最小时间ToolStripMenuItem.Name = "最小时间ToolStripMenuItem";
            this.最小时间ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.最小时间ToolStripMenuItem.Text = "最小";
            // 
            // 最大时间ToolStripMenuItem
            // 
            this.最大时间ToolStripMenuItem.Name = "最大时间ToolStripMenuItem";
            this.最大时间ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.最大时间ToolStripMenuItem.Text = "最大";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(772, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // 复制并解密节点ToolStripMenuItem
            // 
            this.复制并解密节点ToolStripMenuItem.Name = "复制并解密节点ToolStripMenuItem";
            this.复制并解密节点ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.复制并解密节点ToolStripMenuItem.Text = "复制并解密节点";
            this.复制并解密节点ToolStripMenuItem.Click += new System.EventHandler(this.复制并解密节点ToolStripMenuItem_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 721);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 打开目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分组ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 筛选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 扩展名ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大小ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 最小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改时间ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 最小ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 最大ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 创建时间ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 最小时间ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大时间ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem 搜索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem 复制并解密节点ToolStripMenuItem;
    }
}
