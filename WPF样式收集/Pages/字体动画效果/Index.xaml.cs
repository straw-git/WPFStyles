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

namespace WPF样式收集.Pages.字体动画效果
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

        private void btnAni1_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/字体动画效果/Ani1.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnAni2_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/字体动画效果/Ani2.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnAni3_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/字体动画效果/Ani3.xaml", UriKind.RelativeOrAbsolute);
        }

        private void btnAni4_Click(object sender, RoutedEventArgs e)
        {
            bgFrame.Source = new Uri("/Pages/字体动画效果/Ani4.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
