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
    /// LoadingCircle.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingCircle : UserControl
    {
        public LoadingCircle()
        {
            InitializeComponent();
            InitCircle();
        }

        private void InitCircle()
        {
            //右上角
            EllipseCircleAnimation rightUpCircle = new EllipseCircleAnimation();
            rightUpCircle.InitEllipse(new Point(0, 0));
            rightUpCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(141, 141), 100, true, SweepDirection.Clockwise), 2);
            //右下角
            EllipseCircleAnimation rightDownCircle = new EllipseCircleAnimation();
            rightDownCircle.InitEllipse(new Point(0, 0));
            rightDownCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(141, -141), 100, true, SweepDirection.Clockwise), 2);
            //左上角
            EllipseCircleAnimation leftUpCircle = new EllipseCircleAnimation();
            leftUpCircle.InitEllipse(new Point(0, 0));
            leftUpCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(-141, 141), 100, true, SweepDirection.Clockwise), 2);
            //左下角
            EllipseCircleAnimation leftDownCircle = new EllipseCircleAnimation();
            leftDownCircle.InitEllipse(new Point(0, 0));
            leftDownCircle.BeginPathAnimation(CircleGeometry(new Point(0, 0), new Point(-141, -141), 100, true, SweepDirection.Clockwise), 2);

            this.mainGrid.Children.Add(rightUpCircle);
            this.mainGrid.Children.Add(rightDownCircle);
            this.mainGrid.Children.Add(leftUpCircle);
            this.mainGrid.Children.Add(leftDownCircle);
        }

        /// <summary>
        /// 获取环形路径
        /// </summary>
        private PathGeometry CircleGeometry(Point firstPoint, Point secondPoint, double radius, bool isLargeArc, SweepDirection direction)
        {
            PathFigure pathFigure = new PathFigure { IsClosed = true };
            pathFigure.StartPoint = firstPoint;
            pathFigure.Segments.Add(
              new ArcSegment
              {
                  Point = secondPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(radius, radius),
                  SweepDirection = direction
              });
            pathFigure.Segments.Add(
              new ArcSegment
              {
                  Point = firstPoint,
                  IsLargeArc = isLargeArc,
                  Size = new Size(radius, radius),
                  SweepDirection = direction
              });

            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }
    }
}
