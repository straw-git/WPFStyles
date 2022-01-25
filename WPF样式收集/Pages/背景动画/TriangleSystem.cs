using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF样式收集.Pages.背景动画
{
    public class TriangleSystem
    {
        /// <summary>
        /// 三角形列表
        /// </summary>
        private List<Triangle> triangles;

        /// <summary>
        /// 点和与其对应三角形字典
        /// </summary>
        public Dictionary<Point, PointClass> pointTriangles;

        /// <summary>
        /// 容器
        /// </summary>
        private Canvas containerCanvas;

        /// <summary>
        /// 三角形宽
        /// </summary>
        private int triangleWidth = 100;

        /// <summary>
        /// 三角形高
        /// </summary>
        private int triangleHeight = 100;

        /// <summary>
        /// 三角形横向数量
        /// </summary>
        private int horizontalCount = 10;

        /// <summary>
        /// 三角形纵向数量
        /// </summary>
        private int verticalCount = 5;

        /// <summary>
        /// X坐标运动范围
        /// </summary>
        private int XRange = 100;

        /// <summary>
        /// Y坐标运动范围
        /// </summary>
        private int YRange = 10;

        /// <summary>
        /// 坐标运动速度
        /// </summary>
        private int speed = 10;

        /// <summary>
        /// 三角形颜色深度
        /// </summary>
        private double zIndex = 10.0;

        /// <summary>
        /// 随机数
        /// </summary>
        private Random random;

        public TriangleSystem(Canvas ca)
        {
            containerCanvas = ca;
            random = new Random();
            triangles = new List<Triangle>();
            pointTriangles = new Dictionary<Point, PointClass>();

            SpawnTriangle();
        }

        /// <summary>
        /// 三角形初始化
        /// </summary>
        private void SpawnTriangle()
        {
            //清空队列
            triangles.Clear();

            for (int i = 0; i < horizontalCount; i++)
            {
                for (int j = 0; j < verticalCount; j++)
                {
                    Point A = new Point(i * triangleWidth, j * triangleHeight);
                    Point B = new Point(i * triangleWidth, (j + 1) * triangleHeight);
                    Point C = new Point((i + 1) * triangleWidth, (j + 1) * triangleHeight);
                    Point D = new Point((i + 1) * triangleWidth, j * triangleHeight);

                    double index = (i * horizontalCount / zIndex + j * verticalCount / zIndex) / zIndex;
                    index = index > 1 ? 1 : index < 0.1 ? 0.1 : index;
                    Triangle t1 = new Triangle(A, B, C, GetTriangleColor(index), index);
                    Triangle t2 = new Triangle(A, D, C, GetTriangleColor(index - 0.1), index - 0.1);

                    //公共点和三角形集合键值对
                    AddPointTriangles(A, t1, t2);
                    AddPointTriangles(B, t1, t2);
                    AddPointTriangles(C, t1, t2);
                    AddPointTriangles(D, t1, t2);

                    //添加三角形
                    this.containerCanvas.Children.Add(t1.trianglePath);
                    this.containerCanvas.Children.Add(t2.trianglePath);
                    this.triangles.Add(t1);
                    this.triangles.Add(t2);
                }
            }
        }

        /// <summary>
        /// 添加公共点和三角形集合键值对
        /// </summary>
        private void AddPointTriangles(Point p, Triangle t1, Triangle t2)
        {
            if (!this.pointTriangles.Keys.Contains(p))
            {
                List<Triangle> ts = new List<Triangle>();
                ts.Add(t1);
                ts.Add(t2);
                PointClass pc = new PointClass
                {
                    triangles = ts,
                    vector = new Vector(random.Next(-speed, speed) * 0.05, random.Next(-speed, speed) * 0.05),
                };
                this.pointTriangles.Add(p, pc);
            }
            else
            {
                if (!this.pointTriangles[p].triangles.Contains(t1))
                    this.pointTriangles[p].triangles.Add(t1);
                if (!this.pointTriangles[p].triangles.Contains(t2))
                    this.pointTriangles[p].triangles.Add(t2);
            }
        }

        /// <summary>
        /// 获取三角形颜色
        /// </summary>
        private Color GetTriangleColor(double index)
        {
            return Color.FromArgb((byte)(255 * index), 230, 18, 65);
        }

        /// <summary>
        /// 更新三角形
        /// </summary>
        public void Update()
        {
            foreach (var pt in pointTriangles)
            {
                foreach (var t in pt.Value.triangles)
                {
                    if (t.A == pt.Key)
                        t.VA = GetPointValue(t.VA, t.A, ref pt.Value.vector, ref t.triangleColor, ref t.ColorIndex);
                    if (t.B == pt.Key)
                        t.VB = GetPointValue(t.VB, t.B, ref pt.Value.vector, ref t.triangleColor, ref t.ColorIndex);
                    if (t.C == pt.Key)
                        t.VC = GetPointValue(t.VC, t.C, ref pt.Value.vector, ref t.triangleColor, ref t.ColorIndex);
                    t.Draw();
                }
            }
        }

        /// <summary>
        /// 计算顶点值
        /// </summary>
        private Point GetPointValue(Point p1, Point p2, ref Vector v, ref Color c, ref double index)
        {
            Point getPoint = new Point();
            if (p1.X + v.X < p2.X + XRange && p1.X + v.X > p2.X - XRange)
                getPoint.X = p1.X + v.X;
            else
            {
                v.X = -v.X;
                index = index > 1 ? index - 0.01 : index < 0.01 ? index + 0.01 : index - 0.01;
                c = GetTriangleColor(index);
                getPoint.X = p1.X + v.X;
            }

            if (p1.Y + v.Y < p2.Y + YRange && p1.Y + v.Y > p2.Y - YRange)
                getPoint.Y = p1.Y + v.Y;
            else
            {
                v.Y = -v.Y;
                getPoint.Y = p1.Y + v.Y;
            }
            return getPoint;
        }
    }

    public class PointClass
    {
        public List<Triangle> triangles;
        public Vector vector;
    }
}
