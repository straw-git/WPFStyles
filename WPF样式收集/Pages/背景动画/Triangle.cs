using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF样式收集.Pages.背景动画
{
    public class Triangle
    {
        public Point A, B, C;//初始三个顶点
        public Point VA, VB, VC;//运动的三个顶点
        public Path trianglePath;//三角形路径
        public Color triangleColor;//填充
        public double ColorIndex;//颜色深度

        public Triangle(Point a, Point b, Point c, Color co, double z)
        {
            A = VA = a;
            B = VB = b;
            C = VC = c;
            triangleColor = co;
            ColorIndex = z;
            trianglePath = new Path();
            Draw();
        }

        /// <summary>
        /// 绘制三角形
        /// </summary>
        public void Draw()
        {
            var g = new StreamGeometry();
            using (StreamGeometryContext context = g.Open())
            {
                context.BeginFigure(VA, true, true);
                context.LineTo(VB, true, false);
                context.LineTo(VC, true, false);
            }
            trianglePath.Data = g;
            trianglePath.Fill = new SolidColorBrush(triangleColor);
        }
    }
}
