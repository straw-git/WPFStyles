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
    /// BG3.xaml 的交互逻辑
    /// </summary>
    public partial class BG3 : Page
    {
        private TriangleSystem ts;

        public BG3()
        {
            InitializeComponent();
            ts = new TriangleSystem(this.mainCanvas);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        /// <summary>
        /// 帧渲染事件
        /// </summary>
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            ts.Update();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= CompositionTarget_Rendering;
            ts = null;
        }
    }
}
