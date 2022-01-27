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

namespace WPF样式收集.Pages.手风琴效果
{
    /// <summary>
    /// Expander.xaml 的交互逻辑
    /// </summary>
    public partial class Expander : UserControl
    {
        List<ExpanderClass> itemList;

        public Expander()
        {
            InitializeComponent();

            itemList = new List<ExpanderClass>();
            itemList.Add(new ExpanderClass("1", "/Resources/images1.jpg"));
            itemList.Add(new ExpanderClass("2", "/Resources/images1.jpg"));
            itemList.Add(new ExpanderClass("3", "/Resources/images1.jpg"));
            this.ExpanderItemBox.ItemsSource = itemList;
        }
    }
}
