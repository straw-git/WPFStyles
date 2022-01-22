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

namespace WPF样式收集.Pages.Loading
{
    /// <summary>
    /// EllipsePendulum.xaml 的交互逻辑
    /// </summary>
    public partial class EllipsePendulum : UserControl
    {
        public static DependencyProperty FillColorProperty = DependencyProperty.Register("FillColor", typeof(SolidColorBrush), typeof(EllipsePendulum), new PropertyMetadata(null));
        public SolidColorBrush FillColor
        {
            get { return (SolidColorBrush)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public static DependencyProperty ShowTextProperty = DependencyProperty.Register("ShowText", typeof(string), typeof(EllipsePendulum), new PropertyMetadata(null));
        public string ShowText
        {
            get { return (string)GetValue(ShowTextProperty); }
            set { SetValue(ShowTextProperty, value); }
        }

        public EllipsePendulum()
        {
            InitializeComponent();
        }
    }
}
