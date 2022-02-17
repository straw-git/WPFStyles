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
    /// Ani3.xaml 的交互逻辑
    /// </summary>
    public partial class Ani3 : Page
    {
        public Ani3()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UpdateEllipse;
        }


        private void UpdateEllipse(object sender, EventArgs e)
        {
            this.GeometryText.Clip = this.MyEllipseGeometry;
        }
    }
}
