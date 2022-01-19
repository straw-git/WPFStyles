using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPF样式收集.Pages.雪花效果
{
    class MM : Control
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MM()
        {
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100); ;
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var currentcolor = Colors.White;
            Brush brush = new RadialGradientBrush(currentcolor,
                    Color.FromArgb(0, currentcolor.R, currentcolor.G, currentcolor.B));
            Random r = new Random();

            for (int i = 0; i < 530; i++)
            {
                var w = 35 * r.NextDouble();
                var rect =
                new RectangleGeometry(
                    new Rect(new Point(r.Next(10, (int)this.Width), r.Next(10, (int)this.Height)),
                        new Size(w, w)));
                drawingContext.DrawGeometry(brush, null, rect);
            }

        }
    }
}
