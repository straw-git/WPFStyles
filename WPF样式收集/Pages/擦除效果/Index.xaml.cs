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

namespace WPF样式收集.Pages.擦除效果
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Page
    {
        PathGeometry gridGeometry = new PathGeometry();

        public Index()
        {
            InitializeComponent();

            RectangleGeometry rg = new RectangleGeometry();
            rg.Rect = new Rect(0, 0, this.Width, this.Height);
            gridGeometry = Geometry.Combine(gridGeometry, rg, GeometryCombineMode.Union, null);
            gridShadow.Clip = gridGeometry;
            CompositionTarget.Rendering += UpdateEllipseGeometry;
        }

        private void UpdateEllipseGeometry(object sender, EventArgs e)
        {
            EllipseGeometry rg = new EllipseGeometry();
            rg.Center = new Point(this.myEllipseGeometry.Transform.Value.OffsetX, this.myEllipseGeometry.Transform.Value.OffsetY);
            rg.RadiusX = this.myEllipseGeometry.RadiusX;
            rg.RadiusY = this.myEllipseGeometry.RadiusY;
            gridGeometry = Geometry.Combine(gridGeometry, rg, GeometryCombineMode.Exclude, null);
            gridShadow.Clip = gridGeometry;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            gridGeometry.Clear();
            CompositionTarget.Rendering -= UpdateEllipseGeometry;
        }
    }
}
