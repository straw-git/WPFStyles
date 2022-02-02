using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPF样式收集.Pages.背景动画
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();
        }

        private void btnBG1_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/背景动画/BG1.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnBG2_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/背景动画/BG2.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnBG3_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/背景动画/BG3.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnBG4_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/背景动画/BG4.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnBG5_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/背景动画/BG5.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
