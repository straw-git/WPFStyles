using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF样式收集
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard stdStart;

        public MainWindow()
        {
            InitializeComponent();

            Global.MainWindow = this;

            stdStart = (Storyboard)this.Resources["start"];
            stdStart.Completed += (a, b) =>
            {
                this.root.Clip = null;
            };
            this.Loaded += mainWindow_Loaded;
        }


        private string indexPageName = "Index";//约定的页面名称
        ObservableCollection<LeftMenuInfo> LeftMenus = new ObservableCollection<LeftMenuInfo>();//左侧导航数据源

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            stdStart.Begin();

            lMenus.ItemsSource = LeftMenus;
            LoadPageMenus();

        }

        /// <summary>
        /// 加载导航
        /// </summary>
        private void LoadPageMenus()
        {
            LeftMenus.Clear();
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

                LeftMenus.Add(new LeftMenuInfo() { Name = folderName, Url = $"/Pages/{folderName}/{indexPageName}.xaml" });
            }
        }

        /// <summary>
        /// 切换导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lMenus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lMenus.SelectedItem != null)//选中导航验证
            {
                LeftMenuInfo targetItem = lMenus.SelectedItem as LeftMenuInfo;//选中的导航
                if (!string.IsNullOrEmpty(targetItem.Url))//选中导航验证
                {
                    SetBugs();
                    string url = targetItem.Url;//获取URL
                    mainFrame.Source = new Uri(url, UriKind.RelativeOrAbsolute);
                }
            }
        }

        private void btnClose_Click(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("是否确认退出？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void bMove_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 设置Bug
        /// </summary>
        /// <param name="bugStr"></param>
        public void SetBugs(string bugStr = "")
        {
            #region 显示 隐藏

            if (string.IsNullOrEmpty(bugStr))
            {
                spBugs.Children.Clear();
                lblCurrPageBugs.Visibility = Visibility.Collapsed;
                spBugs.Visibility = Visibility.Collapsed;
                return;
            }
            else
            {
                if (lblCurrPageBugs.Visibility == Visibility.Collapsed) lblCurrPageBugs.Visibility = Visibility.Visible;
                if (spBugs.Visibility == Visibility.Collapsed) spBugs.Visibility = Visibility.Visible;
            }

            #endregion 

            bugStr = $"{spBugs.Children.Count + 1}. {bugStr}";
            spBugs.Children.Add(new Label() { Content = bugStr, ToolTip = bugStr });
            lblCurrPageBugs.Content = $"Bug（{spBugs.Children.Count}）";
        }
    }
}
