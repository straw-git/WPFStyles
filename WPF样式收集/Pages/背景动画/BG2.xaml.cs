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
    /// BG2.xaml 的交互逻辑
    /// </summary>
    public partial class BG2 : Page
    {
        private Point lastMousePosition = new Point(0, 0);//鼠标位置
        private int triangleLength = 100;//三角形边长  边长越大 系统处理越多 就会越卡

        public BG2()
        {
            InitializeComponent();

            this.Loaded += Index_Loaded; ;
            CompositionTarget.Rendering += UpdateTriangle;
            this.container.PreviewMouseMove += UpdateLastMousePosition;
        }

        private void Index_Loaded(object sender, RoutedEventArgs e)
        {
            //将长方形容易划分成组合三角形
            int horizontalCount = (int)(this.container.ActualWidth / triangleLength);
            int verticalCount = (int)(this.container.ActualHeight / triangleLength);
            for (int i = 0; i < horizontalCount; i++)
            {
                for (int j = 0; j < verticalCount; j++)
                {
                    Path trianglePath1 = new Path();
                    var g1 = new StreamGeometry();
                    using (StreamGeometryContext context = g1.Open())
                    {
                        context.BeginFigure(new Point(i * triangleLength, j * triangleLength), true, true);
                        context.LineTo(new Point(i * triangleLength, (j + 1) * triangleLength), true, false);
                        context.LineTo(new Point((i + 1) * triangleLength, (j + 1) * triangleLength), true, false);
                    }
                    trianglePath1.Data = g1;
                    trianglePath1.Fill = new SolidColorBrush(Color.FromArgb(255, 247, 18, 65));
                    this.container.Children.Add(trianglePath1);

                    Path trianglePath2 = new Path();
                    var g2 = new StreamGeometry();
                    using (StreamGeometryContext context = g2.Open())
                    {
                        context.BeginFigure(new Point(i * triangleLength, j * triangleLength), true, true);
                        context.LineTo(new Point((i + 1) * triangleLength, j * triangleLength), true, false);
                        context.LineTo(new Point((i + 1) * triangleLength, (j + 1) * triangleLength), true, false);
                    }
                    trianglePath2.Data = g2;
                    trianglePath2.Fill = new SolidColorBrush(Color.FromArgb(255, 247, 18, 65));
                    this.container.Children.Add(trianglePath2);
                }
            }
        }
        private void UpdateTriangle(object sender, EventArgs e)
        {
            //获取子控件
            List<Path> childList = GetChildObjects<Path>(this.container);
            for (int i = 0; i < childList.Count; i++)
            {
                for (int j = 1; j < childList.Count; j++)
                {
                    string si = childList[i].Data.ToString();
                    string si1 = MidStrEx(si, "M", "L");
                    string si2 = MidStrEx(si, "L", " ");
                    string si3 = MidStrEx(si, " ", "z");
                    string sj = childList[j].Data.ToString();
                    string sj1 = MidStrEx(sj, "M", "L");
                    string sj2 = MidStrEx(sj, "L", " ");
                    string sj3 = MidStrEx(sj, " ", "z");
                    //左右三角形判断
                    if (si1 == sj1 && si3 == sj3)
                    {
                        double x = childList[i].Data.Bounds.X + (1 - Math.Pow(2, 0.5) / 2) * triangleLength - lastMousePosition.X;
                        double y = childList[i].Data.Bounds.Y + (1 - Math.Pow(2, 0.5) / 2) * triangleLength - lastMousePosition.Y;
                        double rRadio = 1 - Math.Pow(x * x + y * y, 0.5) / Math.Pow(this.container.ActualWidth * this.container.ActualWidth + this.container.ActualHeight * this.container.ActualHeight, 0.5);
                        childList[j].Fill = new SolidColorBrush(Color.FromArgb((byte)(255 * rRadio), 247, 18, 65));
                        x = childList[j].Data.Bounds.TopRight.X - (1 - Math.Pow(2, 0.5) / 2) * triangleLength - lastMousePosition.X;
                        rRadio = 1 - Math.Pow(x * x + y * y, 0.5) / Math.Pow(this.container.ActualWidth * this.container.ActualWidth + this.container.ActualHeight * this.container.ActualHeight, 0.5);
                        childList[i].Fill = new SolidColorBrush(Color.FromArgb((byte)(255 * rRadio), 247, 18, 65));
                        break;
                    }
                }
            }
        }

        private void UpdateLastMousePosition(object sender, MouseEventArgs e)
        {
            lastMousePosition = e.GetPosition(this.container);
        }

        /// <summary>
        /// 获得所有子控件
        /// </summary>
        private List<T> GetChildObjects<T>(System.Windows.DependencyObject obj) where T : System.Windows.FrameworkElement
        {
            System.Windows.DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child));
            }
            return childList;
        }

        /// <summary>
        /// 截取两个指定字符中间的字符串
        /// </summary>
        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= UpdateTriangle;
            this.container.PreviewMouseMove -= UpdateLastMousePosition;
        }

    }
}
