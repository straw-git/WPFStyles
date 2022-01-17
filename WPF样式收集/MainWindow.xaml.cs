using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF样式收集
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string indexPageName = "Index";//约定的页面名称

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPageMenus();
        }

        /// <summary>
        /// 加载导航
        /// </summary>
        private void LoadPageMenus()
        {
            Assembly currAssembly = Assembly.GetExecutingAssembly();//获取当前程序集
            string pagesNamespace = $"{currAssembly.GetName().Name}.Pages";
            //查找所有页面的命名空间
            var pageNsps = (from t in currAssembly.GetTypes()
                            where t.IsClass && t.Namespace != null
                            && t.Namespace != pagesNamespace
                            && t.Namespace.StartsWith(pagesNamespace)
                            && !t.Namespace.StartsWith("<")
                            select t.Namespace).GroupBy(c => c).ToList();
            foreach (var currPageNsp in pageNsps)//currPageNsp=当前的页面命名空间
            {
                var currPage = (from t in currAssembly.GetTypes()
                                where t.IsClass && t.Namespace != null
                                && t.Namespace == currPageNsp.Key
                                select t.FullName).ToList();
                if (!currPage.Any(c => c.EndsWith(indexPageName)))
                {
                    //没有Index文件 不参与
                    continue;
                }

                //从上级文件夹中获取到文件夹名称
                string folderName = currPageNsp.Key.Substring(currPageNsp.Key.LastIndexOf('.') + 1);
                //如果文件名以_开头 去掉_
                if (folderName.StartsWith("_")) folderName = folderName.Substring(1);

                //加入到Treeview
                TreeViewItem item = new TreeViewItem();
                item.Header = folderName;//Content
                item.Tag = $"/Pages/{folderName}/{indexPageName}.xaml" ;
                tvPages.Items.Add(item);
            }
        }

        /// <summary>
        /// 切换导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPages_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (tvPages.SelectedItem != null)//选中导航验证
            {
                TreeViewItem targetItem = tvPages.SelectedItem as TreeViewItem;//选中的导航
                if (targetItem.Tag != null && !string.IsNullOrEmpty(targetItem.Tag.ToString()))//选中导航验证
                {
                    string url = targetItem.Tag.ToString();//获取URL
                    mainFrame.Source = new Uri(url, UriKind.RelativeOrAbsolute);//转至URL
                }
            }
        }
    }
}
