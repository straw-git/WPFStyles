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

namespace WPF样式收集.Pages.圆形警示效果
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();

            RingControl redRing = new RingControl
            {
                MinSize = 25,
                MaxSize = 100,
                EllipseNum = 4,
                EllipseInterval = 800,
                AnimationDuration = 5,
                EllipseStroke = new SolidColorBrush(Colors.Red),
                EllipseStrokeThickness = 25,
                Margin = new Thickness(-150, -300, 0, 0),
            };
            this.mainGrid.Children.Add(redRing);
        }
    }
}
