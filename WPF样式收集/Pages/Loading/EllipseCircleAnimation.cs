using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WPF样式收集.Pages.Loading
{
    internal class EllipseCircleAnimation : Grid
    {
        private int ellipseCount = 5;
        private Color[] ellipseColors = { (Color)ColorConverter.ConvertFromString("#DB2F00"), (Color)ColorConverter.ConvertFromString("#FF6D37"), (Color)ColorConverter.ConvertFromString("#FFA489"),
        (Color)ColorConverter.ConvertFromString("#56BEBF"), (Color)ColorConverter.ConvertFromString("#13A3A5")};

        public void InitEllipse(Point beginPoint)
        {
            for (int i = 0; i < ellipseCount; i++)
            {
                Ellipse e = new Ellipse()
                {
                    Fill = new SolidColorBrush(ellipseColors[i]),
                    Width = 10 + i * 5,
                    Height = 10 + i * 5,
                    RenderTransform = new TranslateTransform(beginPoint.X, beginPoint.Y),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                };
                this.Children.Add(e);
            }
        }

        public void BeginPathAnimation(PathGeometry ellipsePath, int duration)
        {
            Storyboard sb = new Storyboard();
            //动画完成事件 再次设置此动画
            sb.Completed += (S, E) =>
            {
                BeginPathAnimation(ellipsePath, duration);
            };
            for (int i = 0; i < this.Children.Count; i++)
            {
                DoubleAnimationUsingPath dX = new DoubleAnimationUsingPath()
                {
                    PathGeometry = ellipsePath,
                    Source = PathAnimationSource.X,
                    Duration = new Duration(TimeSpan.FromSeconds(duration)),
                    BeginTime = TimeSpan.FromMilliseconds((this.Children.Count - i) * 200),
                    FillBehavior = FillBehavior.Stop,
                };
                DependencyProperty[] propertyChainX = new DependencyProperty[]
                {
                    Ellipse.RenderTransformProperty,
                    TranslateTransform.XProperty
                };
                Storyboard.SetTarget(dX, (Ellipse)this.Children[i]);
                Storyboard.SetTargetProperty(dX, new PropertyPath("(0).(1)", propertyChainX));

                DoubleAnimationUsingPath dY = new DoubleAnimationUsingPath()
                {
                    PathGeometry = ellipsePath,
                    Source = PathAnimationSource.Y,
                    Duration = new Duration(TimeSpan.FromSeconds(duration)),
                    BeginTime = TimeSpan.FromMilliseconds((this.Children.Count - i) * 200),
                    FillBehavior = FillBehavior.Stop,
                };
                DependencyProperty[] propertyChainY = new DependencyProperty[]
                {
                    Ellipse.RenderTransformProperty,
                    TranslateTransform.YProperty
                };
                Storyboard.SetTarget(dY, (Ellipse)this.Children[i]);
                Storyboard.SetTargetProperty(dY, new PropertyPath("(0).(1)", propertyChainY));

                sb.Children.Add(dX);
                sb.Children.Add(dY);
            }
            sb.Begin();
        }
    }
}
