using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF样式收集.Pages.菜单折叠效果
{
    public class MyButton : Button
    {
        public static readonly DependencyProperty DisplayImageProperty = DependencyProperty.Register("DisplayImage", typeof(ImageSource), typeof(MyButton), new PropertyMetadata(null));
        public ImageSource DisplayImage
        {
            get { return (ImageSource)GetValue(DisplayImageProperty); }
            set { SetValue(DisplayImageProperty, value); }
        }
    }
}
