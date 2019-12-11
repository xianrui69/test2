#define UseUIThreads//使用ui线程则保留这句，否则注释这句

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace 文件搜索
{
    public delegate Task GetInDirectoryFiles(TreeNode a);

    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)] public string lpData;
    }

    public partial class Form3 : Form
    {
        #region 垃圾code

        private abstract class 形象展示
        {
            public List<string> a = new List<string>();

            public abstract void Show();
        }

        private abstract class 修饰 : 形象展示
        {
            public 形象展示 _形象展示;

            public void Set(形象展示 形象展示)
            {
                _形象展示 = 形象展示;
            }

            public override void Show()
            {
                if (_形象展示 != null)
                {
                    _形象展示.Show();
                }
            }
        }

        private class bu : 形象展示
        {
            public override void Show()
            {
                //
            }
        }

        private class 穿秋库 : 修饰
        {
            public 穿秋库()
            {
                a.Add("秋裤");
            }

            private string name = "qiuku";

            public void Set(形象展示 形象展示)
            {
                _形象展示 = 形象展示;
            }

            public override void Show()
            {
                if (_形象展示 != null)
                {
                    _形象展示.a.AddRange(this.a);
                    _形象展示.Show();
                }
            }
        }

        private class 穿秋库1 : 修饰
        {
            public 穿秋库1()
            {
                a.Add("秋裤1");
            }

            public void Set(形象展示 形象展示)
            {
                _形象展示 = 形象展示;
            }

            public override void Show()
            {
                if (_形象展示 != null)
                {
                    _形象展示.a.AddRange(this.a);
                    _形象展示.Show();
                }
            }
        }

        #endregion 垃圾code

        private enum MyEnum : uint
        {
            a = 0x01,
            b = 0x02,
            c = 0x03,
            d = 0x04
        }

        private struct MyStruct
        {
            internal int x;
            internal int y;
            internal Form f;
        }

        private object b;

        public Form3()
        {
            InitializeComponent();

            #region 旧 用于学习的代码

            /*
             int[][,] ss = new int[4][,];
            int[,] arr = new int[,] { { 10, 11 }, { 12, 13 } };
            var a1a = arr[0, 0];
            a1a = arr[0, 1];
            a1a = arr[1, 0];
            a1a = arr[1, 1];
            MyEnum s = MyEnum.a | MyEnum.a | MyEnum.c |
                MyEnum.b;
            bool e = s.HasFlag(MyEnum.a | MyEnum.d | MyEnum.c | MyEnum.a);
            var c = s &= MyEnum.d;//得到默认值0 全部关闭状态
            var n = Enum.GetName(typeof(MyEnum), 0);
            object a = 3;
            b = a;
            a = 3;
            bu a1 = new bu();
            穿秋库 a2 = new 穿秋库();
            穿秋库1 a3 = new 穿秋库1();
            a2.Set(a1);
            a3.Set(a2);
            a3.Show();
             */

            #endregion 旧 用于学习的代码
        }

        private TreeNodeCollection 搜索前的节点集合 = null;

        public enum FileType
        {
            磁盘 = 0,
            文件夹 = 1,
            文件 = 2
        }

        public enum OrderType
        {
            名称 = 0,
            修改时间 = 1,
            创建时间 = 2,
            大小 = 3,
            类型 = 4,
            默认 = 5,
        }

        public static List<KeyValuePair<FileInfo, TreeNode>> allFileInfos = new List<KeyValuePair<FileInfo, TreeNode>>();

        public class myNodeTag
        {
            private TreeNode _node = null;

            public myNodeTag(TreeNode node)
            {
                _node = node;
            }

            private object _tag = null;
            public FileType FileType = FileType.文件;
            public OrderType OrderType = OrderType.类型;

            /// <summary>
            /// 在追加子节点时，对Tag进行更新
            /// </summary>
            /// <param name="file"></param>
            private void 更新信息(FileInfo file)
            {
                allFileInfos.Add(new KeyValuePair<FileInfo, TreeNode>(file, _node));
                FileType = FileType.文件;
                //只管父级
                string exestr = Path.GetExtension(file.FullName).ToLower();
                myNodeTag parent = (_node.Parent ?? new TreeNode() { Tag = null }).Tag as myNodeTag;
                while (parent != null)
                {
                    if (!parent.extCount.ContainsKey(exestr)) parent.extCount.Add(exestr, new List<FileInfo>());
                    parent.extCount[exestr].Add(file);
                    if (parent.MaxFile == null || parent.MaxFile.Length < file.Length) parent.MaxFile = file;
                    if (parent.MinFile == null || parent.MinFile.Length > file.Length) parent.MinFile = file;
                    if (parent.LastEditFile == null || parent.LastEditFile.LastWriteTime < file.LastWriteTime)
                        parent.LastEditFile = file;
                    if (parent.LastCreateFile == null || parent.LastCreateFile.CreationTime > file.CreationTime)
                        parent.LastCreateFile = file;
                    if (parent.LastOpenFile == null || parent.LastOpenFile.LastAccessTime > file.LastAccessTime)
                        parent.LastOpenFile = file;
                    parent = (parent._node.Parent ?? new TreeNode() { Tag = null }).Tag as myNodeTag;
                }
            }

            /// <summary>
            /// 在追加子节点时，对Tag进行更新
            /// </summary>
            /// <param name="file"></param>
            private void 更新信息(DirectoryInfo Dir)
            {
                FileType = FileType.文件夹;
                //只管父级
                myNodeTag parent = (_node.Parent ?? new TreeNode() { Tag = null }).Tag as myNodeTag;
                while (parent != null)
                {
                    if (parent.LastOpenDir == null || parent.LastOpenDir.LastAccessTime > Dir.LastAccessTime) parent.LastOpenDir = Dir;
                    parent = (parent._node.Parent ?? new TreeNode() { Tag = null }).Tag as myNodeTag;
                }
            }

            public object Tag
            {
                get { return _tag; }
                set
                {
                    _tag = value;
                    if (_tag is FileInfo)
                    {
                        更新信息(_tag as FileInfo);
                    }
                    else if (_tag is DirectoryInfo)
                    {
                        更新信息(_tag as DirectoryInfo);
                    }
                }
            }

            /// <summary>
            /// 存储每种后缀有多少个
            /// </summary>
            public Dictionary<string, List<FileInfo>> extCount = new Dictionary<string, List<FileInfo>>();

            /// <summary>
            /// 最大文件
            /// </summary>
            public FileInfo MaxFile = null;

            /// <summary>
            /// 最小文件
            /// </summary>
            public FileInfo MinFile = null;

            /// <summary>
            /// 最后修改文件
            /// </summary>
            public FileInfo LastEditFile = null;

            /// <summary>
            /// 最后创建文件
            /// </summary>
            public FileInfo LastCreateFile = null;

            /// <summary>
            /// 最后访问目录
            /// </summary>
            public DirectoryInfo LastOpenDir = null;

            /// <summary>
            /// 最后访问文件
            /// </summary>
            public FileInfo LastOpenFile = null;

            public class 文件筛选条件
            {
                public string[] 扩展名集合;
                public long? 最大文件 = null;
                public long? 最小文件 = null;
                public DateTime? 最大修改时间 = null;
                public DateTime? 最小修改时间 = null;
                public DateTime? 最大创建时间 = null;
                public DateTime? 最小创建时间 = null;
            }

            /// <summary>
            /// 筛选同时
            /// </summary>
            /// <param name="file"></param>
            /// <returns></returns>
            public bool 筛选(FileInfo file)
            {
                return true;
            }

            public bool 筛选(DirectoryInfo file)
            {
                return true;
            }

            private 文件筛选条件 _筛选条件;

            public 文件筛选条件 筛选条件
            {
                get { return _筛选条件; }
                set
                {
                    _筛选条件 = value;
                    if (Tag is FileInfo)
                    {
                        筛选(Tag as FileInfo);
                    }
                    else if (Tag is DirectoryInfo)
                    {
                        筛选(Tag as DirectoryInfo);
                    }
                }
            }

            private DirectoryInfo[] 筛选后的子文件夹集合 = null;
            private FileInfo[] 筛选后的子文件集合 = null;

            //空文件夹组
            private void 字节单位转换(string 字节类型)
            {
            }

            public void Order(OrderType type, bool auto)
            {
                OrderType = type;
                switch (type)
                {
                    case OrderType.创建时间: break;
                    case OrderType.大小: break;
                    case OrderType.类型:
                        {
                            var _nodes = new List<TreeNode>();
                            foreach (var extCountKey in extCount.Keys)
                            {
                                _nodes.Add(new TreeNode() { Text = extCountKey, Tag = extCount[extCountKey] });
                            }
                            break;
                        }
                    case OrderType.名称: break;
                    case OrderType.修改时间: break;
                    case OrderType.默认: break;
                }
            }

            //分组后标签上具备Count
            /// <summary>
            /// 通过算法对文件进行分组
            /// 根据名称
            /// 参数为：算法划分区间 还是选择区间
            /// </summary>
            public void bySizeOrderNode(bool 自动)
            {
                OrderType = OrderType.大小;
            }

            /// <summary>
            /// 通过算法对文件进行分组
            /// 根据名称
            /// 参数为：算法划分时间区间 还是选择分组区间
            /// </summary>
            public void byNameOrderNode(bool 自动)
            {
                OrderType = OrderType.名称;
            }

            /// <summary>
            /// 通过算法对文件进行分组
            /// 根据扩展名
            /// </summary>
            public void byExeOrderNode()
            {
                OrderType = OrderType.类型;
            }

            /// <summary>
            /// 通过算法对文件进行分组
            /// 根据大小
            /// 参数为：算法划分大小区间 还是选择大小区间
            /// </summary>
            public void bySizeOrderNode_创建时间(bool 自动)
            {
                OrderType = OrderType.创建时间;
            }

            /// <summary>
            /// 通过算法对文件进行分组
            /// 根据大小
            /// 参数为：算法划分大小区间 还是选择大小区间
            /// </summary>
            public void bySizeOrderNode_修改时间(bool 自动)
            {
                OrderType = OrderType.修改时间;
            }

            /// <summary>
            /// 不分组
            /// </summary>
            public void noneOrder()
            {
                OrderType = OrderType.默认;
            }
        }

        private DateTime 开始执行搜索盘符内容的时间;
        private DateTime 开始执行的时间;

        /// <summary>
        /// 重置一个节点
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="acbCallback"></param>
        /// <param name="state"></param>
        private void ReLoadNode(TreeNode tn, AsyncCallback acbCallback = null, object state = null)
        {
            FileType ft = FileType.磁盘;
            var mytag = tn.Tag as myNodeTag;
            if (mytag != null)
                ft = mytag.FileType;
            var parent = tn.Parent;
            var idx = tn.Index;
            tn.Remove();
            tn.Nodes.Clear();
            if (acbCallback == null)
            {
                state = tn;
                acbCallback = ar =>
                {
                    if (parent == null)
                        SetNodeIdx(tn, 3);
                    else AddChildNode(parent, tn, ft == FileType.文件, idx);
                };
            }
            if (ft == FileType.文件)
            {
                var fi = ((tn.Tag as myNodeTag).Tag as FileInfo);
                fi.Refresh();
                if (!fi.Exists)
                {
                    ReLoadNode(parent);//不存在就刷新父节点
                    return;
                }
                AddChildNode(parent, tn, ft == FileType.文件, idx);
                return;
            }
            GetInDirectoryFiles handler = new GetInDirectoryFiles(GetInDirectoryFiles);
            IAsyncResult result = handler.BeginInvoke(tn, acbCallback, state);
        }

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
            int hWnd, // handle to destination window
            int Msg, // message
            int wParam, // first message parameter
            ref COPYDATASTRUCT lParam // second message parameter
        );

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string
            lpWindowName);

        private const int WM_COPYDATA = 0x004A;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostThreadMessage(int threadId, uint msg, IntPtr wParam, IntPtr lParam);

        private string selPath(string key)
        {
            var src = "";
            folderBrowserDialog1.SelectedPath = INI.ReadINI("上次选择路径", key);
            while (!Directory.Exists(src))
            {
                var result = folderBrowserDialog1.ShowDialog();
                src = folderBrowserDialog1.SelectedPath;
            }
            INI.WriteINI("上次选择路径", key, src);
            return src;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            /*
             *遍历文件系统：得到一个树；堆栈数组好像用不到
             *通过遍历，判断能否往下延伸，进而知道是否为根节点
             *首先得到所有盘符，其次得到一个盘底下的所有内容
             *刷新文件内容则交给右键刷新功能？或者隔几秒读取一次当前内容吧；不读取子级节点
             *节点展开时读取一次变化；
             *所以查询的基础建立在第一次扫描
             *实际使用发现递归会很慢
             *所以也许需要限定范围 以及异步加载
             *再有就是优化写法
             */

            #region 添加下标集合

            var url = Application.StartupPath;

            var _idx = 0;
            if ((_idx = url.LastIndexOf("\\")) != -1) //往上两级
            {
                url = url.Substring(0, _idx);
                if ((_idx = url.LastIndexOf("\\")) != -1)
                {
                    url = url.Substring(0, _idx);
                }
            }
            treeView1.ImageList = new ImageList();
            treeView1.ImageList.Images.Add(new Icon(url + "\\文件查找器资源文件\\Image\\bitbug_favicon.ico"));
            treeView1.ImageList.Images.Add(new Icon(url + "\\文件查找器资源文件\\Image\\文件夹.ico"));
            treeView1.ImageList.Images.Add(new Icon(url + "\\文件查找器资源文件\\Image\\文件.ico"));
            treeView1.ImageList.Images.Add(new Icon(url + "\\文件查找器资源文件\\Image\\磁盘.ico"));

            #endregion 添加下标集合

            List<TreeNode> tns = new List<TreeNode>();
            Dictionary<TreeNode, string> times = new Dictionary<TreeNode, string>();
            DialogResult result = MessageBox.Show("搜索全部吗？", "问询", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //查询全部
                var lt = GetRemovableDeviceID(); //D://

                foreach (var s in lt)
                {
                    var curNode = new TreeNode() { Name = s + "//", Text = s, Tag = new DirectoryInfo(s + "//") };
                    curNode.Tag = new myNodeTag(curNode) { Tag = curNode.Tag };
                    (curNode.Tag as myNodeTag).FileType = FileType.磁盘;
                    var time = INI.ReadINI("盘符上次遍历时间", s + "//");
                    treeView1.Nodes.Add(new TreeNode() { Name = "临时占据：" + s + "//", Text = s + "上次加载时间：" + time });
                    //GetInDirectoryFiles(curNode);
                    tns.Add(curNode);
                    times.Add(curNode, time);
                }
                tns.Clear();
                foreach (var keyValuePair in times.OrderBy(k => k.Value))
                {
                    tns.Add(keyValuePair.Key);
                }
            }
            else
            {
                button3.Enabled = false;
                var src = selPath("上次选择路径");
                var curNode = new TreeNode() { Name = src, Text = src, Tag = new DirectoryInfo(src) };
                curNode.Tag = new myNodeTag(curNode) { Tag = curNode.Tag };
                (curNode.Tag as myNodeTag).FileType = FileType.文件夹;
                var time = INI.ReadINI("盘符上次遍历时间", src);
                treeView1.Nodes.Add(new TreeNode() { Name = "临时占据：" + src + "//", Text = src + "上次加载时间：" + time });
                tns.Add(curNode);
                times.Add(curNode, time);
            }
            开始执行搜索盘符内容的时间 = DateTime.Now;
            开始执行的时间 = DateTime.Now;
            timer1.Start();
            label2.Text = "";
            加载子节点(tns);
            foreach (int myCode in Enum.GetValues(typeof(OrderType)))
            {
                string strName = Enum.GetName(typeof(OrderType), myCode); //获取名称
                string strVaule = myCode.ToString(); //获取值
                var item = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = strName,
                    Name = strName,
                    Tag = strVaule
                };
                分组ToolStripMenuItem.DropDownItems.Add(item);
                item.Click += (o, args) =>
                {
                    if (contextMenuStrip1.Tag is TreeNode)
                    {
                        var node = contextMenuStrip1.Tag as TreeNode;
                        if (node != null && node.Tag is myNodeTag)
                        {
                            (node.Tag as myNodeTag).Order(
                                (OrderType)int.Parse((o as ToolStripMenuItem).Tag.ToString()), true);
                        }
                    }
                };
            }
        }

        private async void 加载子节点(List<TreeNode> tns, bool 使用ui线程 = false)
        {
            GetInDirectoryFiles handler = new GetInDirectoryFiles(GetInDirectoryFiles);
            //IAsyncResult: 异步操作接口(interface)
            //BeginInvoke: 委托(delegate)的一个异步方法的开始
            //                                        委托参数，回调函数，回调函数中的result.AsyncState对象

            if (!使用ui线程)
            {
                handler.BeginInvoke(tns[0], new AsyncCallback(异步回调函数), tns); //后台线程异步
                return;
            }
            while (tns.Count > 0)
            {
                await GetInDirectoryFiles(tns[0]);//主线程上异步
                真回调函数(tns);
            }
        }

        private void 真回调函数(List<TreeNode> curNodes)
        {
            var curNode = curNodes[0];
            INI.WriteINI("盘符上次遍历时间", curNode.Name + "", (DateTime.Now - 开始执行搜索盘符内容的时间).ToString("c"));
            //treeView1.Nodes.Remove(tn);
            //treeView1.Nodes.Add(tn);
            //AddChildNode(treeView1.Nodes[0].Parent, curNode, true);
            SetNodeIdx(curNode, 3);
            curNodes.Remove(curNode);
            if (curNodes.Count == 0)
            {
                timer1.Stop();
                lab2set("");
                //label2.Text = "";//异步操作控件
                return;
            }

            //IAsyncResult: 异步操作接口(interface)
            //BeginInvoke: 委托(delegate)的一个异步方法的开始
            //                                        委托参数，回调函数，回调函数中的result.AsyncState对象
            开始执行搜索盘符内容的时间 = DateTime.Now;
        }

        private void 异步回调函数(IAsyncResult result)
        {
            try
            {
                //result 是委托方法的返回值
                //AsyncResult 是IAsyncResult接口的一个实现类，空间：System.Runtime.Remoting.Messaging
                //AsyncDelegate 属性可以强制转换为用户定义的委托的实际类。
                GetInDirectoryFiles handler = null;
                if (result != null)
                {
                    handler = (GetInDirectoryFiles)((AsyncResult)result).AsyncDelegate;
                    handler.EndInvoke(result).Wait();
                }
                // Console.WriteLine(result.AsyncState);
                var curNodes = (result.AsyncState as List<TreeNode>);
                真回调函数(curNodes);
                IAsyncResult result1 = handler.BeginInvoke(curNodes[0], new AsyncCallback(异步回调函数), curNodes);
            }
            catch (Exception)
            {
            }
        }

        private int i = 0;

        #region 异步操作控件

        private delegate void label2Set(string txt);

        private void lab2set(string txt)
        {
            if (this.label2.InvokeRequired)
            {
                label2Set stcb = new label2Set(lab2set);
                this.Invoke(stcb, new object[] { txt });
            }
            else
            {
                label2.Text = txt;
            }
        }

        private delegate void delSetNodeIdx(TreeNode chid, int idx);

        private void SetNodeIdx(TreeNode curtn, int idx)
        {
            if (this.treeView1.InvokeRequired)
            {
                delSetNodeIdx stcb = new delSetNodeIdx(SetNodeIdx);
                this.Invoke(stcb, new object[] { curtn, idx });
            }
            else
            {
                curtn.SelectedImageIndex = curtn.ImageIndex = idx;
                if (idx == 3)
                {
                    TreeNode needDelNode = null;
                    foreach (TreeNode item in treeView1.Nodes)
                    {
                        if (item.Name == "临时占据：" + curtn.Name)
                        {
                            needDelNode = item;
                        }
                    }
                    try
                    {
                        if (needDelNode != null)
                        {
                            treeView1.Nodes.Insert(needDelNode.Index, curtn);
                            treeView1.Nodes.Remove(needDelNode);
                        }
                        else
                            treeView1.Nodes.Insert(treeView1.Nodes.Count, curtn);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private delegate void delAddChildNode(TreeNode curtn, TreeNode chid, bool isFile, int? idx = null);

        /// <summary>
        /// 添加节点之前 修改Tag
        /// </summary>
        /// <param name="curtn"></param>
        /// <param name="chid"></param>
        /// <param name="isFile"></param>
        private void AddChildNode(TreeNode curtn, TreeNode chid, bool isFile, int? idx = null)
        {
            if (this.treeView1.InvokeRequired)
            {
                delAddChildNode stcb = new delAddChildNode(AddChildNode);
                this.Invoke(stcb, new object[] { curtn, chid, isFile, idx });
            }
            else
            {
                //this.txt_a.Text = text;
                var oldTag = chid.Tag;
                try
                {
                    if (idx == null)
                        curtn.Nodes.Add(chid);
                    else curtn.Nodes.Insert(idx ?? 0, chid);
                    var newTag = new myNodeTag(chid);
                    newTag.Tag = oldTag;
                    if (oldTag is myNodeTag) ;
                    else
                        chid.Tag = newTag;
                    chid.SelectedImageIndex = chid.ImageIndex = isFile ? 2 : 1;
                    chid.ToolTipText = chid.Name;
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion 异步操作控件

        /// <summary>
        /// 遍历加载子节点 使用堆栈
        /// </summary>
        /// <param name="curtn"></param>
        private async Task GetInDirectoryFiles(TreeNode curtn)
        {
            //使用后进先出的堆栈来解决递归产生的低效率问题
            Stack<TreeNode> myStack = new Stack<TreeNode>();
            myStack.Push(curtn);
            i++;
            //name 需要是路劲
            //txt是显示的文本
            //子节点是底下的内容
            int iiii = await Task.Run(() => 1);
            while (myStack.Count > 0)
            {
                var _curtn = myStack.Pop();

                if (_curtn.Name == "") return;
                DirectoryInfo curdir = null;
                try
                {
                    curdir = new DirectoryInfo(_curtn.Name);
                }
                catch (Exception e)
                {
                    continue;//文件名太长时 不加载
                }
                try
                {
                    foreach (var fileInfo in curdir.GetFiles())
                    {
                        var curNode = new TreeNode()
                        {
                            Name = fileInfo.FullName,
                            Text = fileInfo.Name,
                            Tag = fileInfo
                        };
                        AddChildNode(_curtn, curNode, true);
                    }
                }
                catch (Exception e)
                {
                    AddChildNode(_curtn, new TreeNode()
                    {
                        Name = "",
                        Text = "无法访问"
                    }, true);
                }
                try
                {
                    foreach (var directoryInfo in curdir.GetDirectories())
                    {
                        var curNode = new TreeNode()
                        {
                            Name = directoryInfo.FullName,
                            Text = directoryInfo.Name,
                            Tag = directoryInfo
                        };
                        AddChildNode(_curtn, curNode, false);
                        myStack.Push(curNode);
                        //GetInDirectoryFiles(curNode);
                        //GetInDirectoryFiles handler = new GetInDirectoryFiles(GetInDirectoryFiles);

                        ////IAsyncResult: 异步操作接口(interface)
                        ////BeginInvoke: 委托(delegate)的一个异步方法的开始
                        //IAsyncResult result = handler.BeginInvoke(curNode, new AsyncCallback(回调函数), "AsycState:OK");
                    }
                }
                catch (Exception e)
                {
                    AddChildNode(_curtn, new TreeNode()
                    {
                        Name = "",
                        Text = "无法访问"
                    }, true);
                }
            }
        }

        /// <summary>
        /// 遍历加载子节点 使用递归
        /// </summary>
        /// <param name="_curtn"></param>
        private void GetInDirectoryFiles1(TreeNode _curtn)
        {
            //使用后进先出的堆栈来解决递归产生的低效率问题

            i++;
            //name 需要是路劲
            //txt是显示的文本
            //子节点是底下的内容

            if (_curtn.Name == "") return;
            var curdir = new DirectoryInfo(_curtn.Name);
            try
            {
                foreach (var fileInfo in curdir.GetFiles())
                {
                    var curNode = new TreeNode()
                    {
                        Name = fileInfo.FullName,
                        Text = fileInfo.Name,
                        Tag = fileInfo
                    };
                    AddChildNode(_curtn, curNode, true);
                }
            }
            catch (Exception e)
            {
                AddChildNode(_curtn, new TreeNode()
                {
                    Name = "",
                    Text = "无法访问"
                }, true);
            }
            try
            {
                foreach (var directoryInfo in curdir.GetDirectories())
                {
                    var curNode = new TreeNode()
                    {
                        Name = directoryInfo.FullName,
                        Text = directoryInfo.Name,
                        Tag = directoryInfo
                    };
                    AddChildNode(_curtn, curNode, false);
                    GetInDirectoryFiles1(curNode);
                    //GetInDirectoryFiles(curNode);
                    //GetInDirectoryFiles handler = new GetInDirectoryFiles(GetInDirectoryFiles);

                    ////IAsyncResult: 异步操作接口(interface)
                    ////BeginInvoke: 委托(delegate)的一个异步方法的开始
                    //IAsyncResult result = handler.BeginInvoke(curNode, new AsyncCallback(回调函数), "AsycState:OK");
                }
            }
            catch (Exception e)
            {
                AddChildNode(_curtn, new TreeNode()
                {
                    Name = "",
                    Text = "无法访问"
                }, true);
            }
        }

        /// <summary>
        /// 得到所有盘符
        /// </summary>
        /// <returns></returns>
        public List<string> GetRemovableDeviceID()
        {
            List<string> deviceIDs = new List<string>();
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT  *  From  Win32_LogicalDisk ");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                switch (int.Parse(mo["DriveType"].ToString()))
                {
                    case (int)DriveType.Removable:   //可以移动磁盘
                        {
                            //MessageBox.Show("可以移动磁盘");
                            deviceIDs.Add(mo["DeviceID"].ToString());
                            break;
                        }
                    case (int)DriveType.Fixed:   //本地磁盘
                        {
                            //MessageBox.Show("本地磁盘");
                            deviceIDs.Add(mo["DeviceID"].ToString());
                            break;
                        }
                    case (int)DriveType.CDRom:   //CD   rom   drives
                        {
                            //MessageBox.Show("CD   rom   drives ");
                            break;
                        }
                    case (int)DriveType.Network:   //网络驱动
                        {
                            //MessageBox.Show("网络驱动器 ");
                            break;
                        }
                    case (int)DriveType.Ram:
                        {
                            //MessageBox.Show("驱动器是一个 RAM 磁盘 ");
                            break;
                        }
                    case (int)DriveType.NoRootDirectory:
                        {
                            //MessageBox.Show("驱动器没有根目录 ");
                            break;
                        }
                    default:   //defalut   to   folder
                        {
                            //MessageBox.Show("驱动器类型未知 ");
                            break;
                        }
                }
            }
            return deviceIDs;
        }

        private int num(int x)
        {
            if (x <= 2)
                return 1;
            else return num(x - 2) + num(x - 1);
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeCollection tns = (sender as TreeView).Nodes;
            label1.Text = "路径：" + e.Node.Name;
        }

        /// <summary>
        /// 分组 对选中节点进行分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text == "无法访问")
            {
                treeView1.SelectedNode = e.Node.Parent;
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                //右击
                contextMenuStrip1.Tag = e.Node;
                contextMenuStrip1.Show(treeView1, e.Location);
                //                contextMenuStrip1.ContextMenuStrip.
                var mytag = (contextMenuStrip1.Tag as TreeNode).Tag as myNodeTag;
                if (mytag == null)
                {
                    搜索ToolStripMenuItem.Visible = 刷新ToolStripMenuItem.Visible = 打开文件ToolStripMenuItem.Visible = 筛选ToolStripMenuItem.Visible = 分组ToolStripMenuItem.Visible =
                        false;
                }
                else
                {
                    搜索ToolStripMenuItem.Visible = true;
                    筛选ToolStripMenuItem.Visible = 分组ToolStripMenuItem.Visible =
                        mytag.FileType == FileType.文件夹 || mytag.FileType == FileType.磁盘;
                    打开文件ToolStripMenuItem.Visible = mytag.FileType == FileType.文件;
                    if (treeView1.SelectedNode.Name.IndexOf("临时占据") != -1 || treeView1.SelectedNode.Name.IndexOf("搜索结果") != -1)
                    {
                        刷新ToolStripMenuItem.Visible = false;
                    }
                    else 刷新ToolStripMenuItem.Visible = true;
                    var node = (treeView1.SelectedNode.Parent);
                    while (node != null)
                    {
                        if (node.Name.IndexOf("搜索结果") != -1) 刷新ToolStripMenuItem.Visible = false;
                        node = node.Parent;
                    }
                }
            }
            label1.Text = "路径：" + e.Node.Name.Replace("临时占据：", "");
        }

        private void 打开目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var curNode = contextMenuStrip1.Tag as TreeNode;
            var curTag = curNode.Tag as myNodeTag;
            var curSrc = curNode.Name.Replace("临时占据：", "");
            try
            {
                if (curTag.FileType == FileType.文件)
                {
                    try
                    {
                        ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", string.Format("/Select, {0}", curSrc));
                        Process.Start(pfi);
                    }
                    catch (Exception exception)
                    {
                        //针对搜索结果
                        try
                        {
                            if ((contextMenuStrip1.Tag as TreeNode).Parent.Name == "搜索结果")
                            {
                                ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", string.Format("/Select, {0}", curSrc));
                                Process.Start(pfi);
                            }
                        }
                        catch (Exception e1)
                        {
                            ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", string.Format("{0}", curNode.Parent.Name));
                            Process.Start(pfi);
                        }
                    }
                    return;
                }
            }
            catch (Exception exception)
            {
            }
            try
            {
                System.Diagnostics.Process.Start(curSrc);
            }
            catch (Exception exception)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "已搜索：" + (DateTime.Now - 开始执行的时间).ToString("c").Substring(0, 8);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sender == button3)
            {
                treeView1.SelectedNode = null;
            }
            var oldsel = treeView1.SelectedNode;
            List<string> exts = new List<string>();
            foreach (TreeNode treeView1Node in treeView1.Nodes)
            {
                if (treeView1Node.Tag is myNodeTag)
                {
                    exts.AddRange((treeView1Node.Tag as myNodeTag).extCount.Keys);
                }
            }
            exts = exts.Distinct().ToList();
            exts.Sort();
            Form4 f = new Form4(exts);
            f.Text = (sender == button3 ? "全局搜索" : "局部搜索") + ":" + f.Text;
            if (f.ShowDialog() != DialogResult.OK) return;
            treeView1.SelectedNode = oldsel;
            var 搜索三十天以上未动的文件 = f.checkedListBox3.CheckedItems.IndexOf("搜索三十天以上未动的文件") != -1;
            //"搜索三十天以上未动的文件", "搜索一百八十天以上未动的文件", "搜索一年以上未动的文件"
            var jgs = allFileInfos.FindAll(kv =>
            {
                var reg = true;
                if (Form4.myRegex != null)
                    reg = Form4.myRegex.Match(kv.Key.Name).Success;
                else if (!string.IsNullOrEmpty(Form4.文件名))
                    reg = kv.Key.Name.Contains(Form4.文件名);
                if (reg && exts.Count > 0)
                    reg = exts.Contains(kv.Key.Extension.ToLower());
                if (reg && Form4.开始创建时间 != default(DateTime))
                    reg = kv.Key.CreationTime >= Form4.开始创建时间;
                if (reg && Form4.结束创建时间 != default(DateTime))
                    reg = kv.Key.CreationTime <= Form4.结束创建时间;
                if (reg && Form4.开始修改时间 != default(DateTime))
                    reg = kv.Key.LastWriteTime >= Form4.开始修改时间;
                if (reg && Form4.结束修改时间 != default(DateTime))
                    reg = kv.Key.LastWriteTime <= Form4.结束修改时间;
                if (reg && Form4.左边的文件大小 != default(decimal))
                    reg = kv.Key.Length >= Form4.左边的文件大小;
                if (reg && Form4.右边的文件大小 != default(decimal))
                    reg = kv.Key.Length <= Form4.右边的文件大小;
                if (reg && 搜索三十天以上未动的文件)
                    reg = (kv.Key.LastWriteTime - kv.Key.CreationTime).Days >= 30;
                return reg;
            });
            //搜索前的节点集合 = treeView1.Nodes;
            foreach (TreeNode treeView1Node in treeView1.Nodes)
            {
                if (treeView1Node.Name == "搜索结果") treeView1Node.Nodes.Remove(treeView1Node);
            }
            var sels = new TreeNode() { Name = "搜索结果", Text = "搜索结果(未加载完成的无法搜索到)" };
            jgs.ForEach(kv =>
            {
                var curNode = kv.Value.Clone() as TreeNode;
                if (treeView1.SelectedNode != null)
                {
                    TreeNode tn = kv.Value.Parent;
                    bool regState = false;
                    while (tn != null && regState == false)
                    {
                        if (tn == treeView1.SelectedNode)
                        {
                            regState = true;
                        }
                        tn = tn.Parent;
                    }
                    if (!regState) return;
                }
                curNode.Text = curNode.Name;
                sels.Nodes.Add(curNode);
            });
            treeView1.Nodes.Add(sels);
            //sels.Expand();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null) return;

            ReLoadNode(treeView1.SelectedNode);
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", (contextMenuStrip1.Tag as TreeNode).Name);
            }
            catch (Exception exception)
            {
            }
        }

        private void 复制并解密节点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var copyPath = "";
            while (!Directory.Exists(copyPath))
            {
                copyPath = selPath("上次要复制到的目录");
                if (Directory.GetFiles(copyPath).Length > 0)
                {
                    MessageBox.Show("请选择空文件夹！");
                    copyPath = "";
                }
            }
            Dictionary<string, Encoding> keyValuePairs = new Dictionary<string, Encoding>();
            keyValuePairs.Add(".cs", Encoding.UTF8);
            Func<string, Encoding> getEncoding = (ext) =>
                keyValuePairs.ContainsKey(ext.ToLower()) ? keyValuePairs[ext.ToLower()] : Encoding.UTF8;
            var key = ".myType";
            List<string> ens = new List<string>();//所有加密过的后缀名
            List<string> needens = new List<string>() { ".cs" };//所有需要处理的后缀名
            //  后缀      是否加密
            Func<string, bool, string> getNewExt = (ext, isEn) =>
            {
                if (isEn)
                {
                    if (!needens.Contains(ext.ToLower())) return ext;
                    ext = key + ext.Substring(1);//加密
                    if (!ens.Any(el => el == ext))
                        ens.Add(ext);
                }
                else if (ext.IndexOf(key) == 0)
                {
                    ext = "." + ext.Substring(key.Length);
                }
                return ext;
            };
            var dirPath = "";//所有文件的父目录
            Func<string, string> getCopyPath = (path) => copyPath + path.Substring(dirPath.Length);//old 移除 + new + 剩下
            Func<string, string> copyFile = (fileSrc) =>
            {
                var newFile = getCopyPath(fileSrc);
                if (File.Exists(fileSrc) && !File.Exists(newFile))
                {
                    var ext = Path.GetExtension(fileSrc);
                    var ecd = getEncoding(ext);
                    newFile = newFile.Substring(0, newFile.LastIndexOf(ext)) + getNewExt(ext, true);
                    using (var newFileStream = new FileStream(newFile, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamReader sr = new StreamReader(fileSrc))
                        {
                            var txt = sr.ReadToEnd();
                            using (StreamWriter nsr = new StreamWriter(newFileStream))
                            {
                                nsr.WriteLine(txt);//开始写入值
                                nsr.Flush();
                            }
                        }
                    }
                }
                return newFile;
            };
            Action setBat = () =>
            {
                var batPath = copyPath + "\\encode.bat";
                using (var newFileStream = new FileStream(batPath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter nsr = new StreamWriter(newFileStream))
                    {
                        ens.ForEach(ext =>
                        {
                            nsr.WriteLine(string.Format("for /f \"delims=\" %%a in ('dir /b/s/a-d *{0}') do rename \"%%a\" *{1}",
                                ext, getNewExt(ext, false)));//开始写入值
                        });
                        nsr.Flush();
                    }
                }
                ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", string.Format("/Select, {0}", batPath));
                Process.Start(pfi);
            };
            //得到目标路径 copyPath
            //复制文件夹目录
            //复制单个文件
            //后缀与编码映射
            //bat文件编写
            //打开文件夹选中bat
            var curNode = contextMenuStrip1.Tag as TreeNode;
            var curTag = curNode.Tag as myNodeTag;
            var curPath = curNode.Name.Replace("临时占据：", "");
            try
            {
                if (curTag.FileType == FileType.文件)
                {
                    dirPath = Path.GetDirectoryName(curPath);
                    var newFile = copyFile(curPath);
                }
                else
                {
                    dirPath = curPath;
                    Stack<TreeNode> treeNodes = new Stack<TreeNode>();
                    treeNodes.Push(curNode);
                    while (treeNodes.Count > 0)
                    {
                        curNode = treeNodes.Pop();
                        curPath = curNode.Name.Replace("临时占据：", "");
                        curTag = curNode.Tag as myNodeTag;
                        if (curTag.FileType == FileType.磁盘)
                        {
                            MessageBox.Show("禁止复制磁盘！");
                            continue;
                        }
                        else if (curTag.FileType == FileType.文件)
                        {
                            copyFile(curPath);
                        }
                        else
                        {
                            Directory.CreateDirectory(getCopyPath(curPath));
                            if (curNode.Nodes != null)
                                foreach (TreeNode item in curNode.Nodes)
                                    treeNodes.Push(item);
                        }
                    }
                }
                try
                {
                    setBat();
                    return;
                }
                catch (Exception exception)
                {
                    ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", string.Format("{0}", copyPath));
                    Process.Start(pfi);
                }
            }
            catch (Exception exception)
            {
            }
            try
            {
                System.Diagnostics.Process.Start(copyPath);
            }
            catch (Exception exception)
            {
            }
        }
    }
}
