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
    public partial class Ani4 : Page
    {
        public Ani4()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UpdateGeometry;
        }

        private void UpdateGeometry(object sender, EventArgs e)
        {
            this.myBgImage.Clip = this.myRectangleGeometry3;
            this.myGeometryImage1.Clip = this.myRectangleGeometry1;
            this.myGeometryImage2.Clip = this.myRectangleGeometry2;
            this.myText2.Clip = this.myTextRectangleGeometry;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= UpdateGeometry;
        }
    }
}
