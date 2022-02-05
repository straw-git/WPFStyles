using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF样式收集.Pages.DNA螺旋粒子动画
{
    class ParticleSystem
    {
        /// <summary>
        /// 粒子步数
        /// </summary>
        private int particleCount = 60;

        /// <summary>
        /// 间隔
        /// </summary>
        private readonly int SEPARATION = 30;

        /// <summary>
        /// Y振幅
        /// </summary>
        private int YMax = 50;

        /// <summary>
        /// 粒子最大尺寸
        /// </summary>
        private int sizeMax = 8;

        /// <summary>
        /// 粒子列表
        /// </summary>
        private List<Particle> particleList1, particleList2;

        /// <summary>
        /// 粒子容器
        /// </summary>
        private Canvas containerParticles;

        /// <summary>
        /// 计算量
        /// </summary>
        private double count = 0;

        public ParticleSystem(Canvas cv)
        {
            containerParticles = cv;
            particleList1 = new List<Particle>();
            particleList2 = new List<Particle>();
            SpawnParticle();
        }

        /// <summary>
        /// 初始化粒子
        /// </summary>
        private void SpawnParticle()
        {
            //清空粒子队列
            particleList1.Clear();
            particleList2.Clear();
            containerParticles.Children.Clear();

            // 初始化粒子位置和大小
            for (int i = 0; i < particleCount; i++)
            {
                var p1 = new Particle
                {
                    Shape = new Ellipse
                    {
                        Width = (Math.Sin(i * 0.5) + 2) * sizeMax,
                        Height = (Math.Sin(i * 0.5) + 2) * sizeMax,
                        Stretch = System.Windows.Media.Stretch.Fill,
                        Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                    },
                    Position = new Point(i * SEPARATION - ((particleCount * SEPARATION) / 2), Math.Sin(i * 0.5) * YMax)
                };
                particleList1.Add(p1);
                Canvas.SetLeft(p1.Shape, p1.Position.X);
                Canvas.SetTop(p1.Shape, p1.Position.Y);
                containerParticles.Children.Add(p1.Shape);

                var p2 = new Particle
                {
                    Shape = new Ellipse
                    {
                        Width = (Math.Cos(i * (-0.5)) + 2) * sizeMax,
                        Height = (Math.Cos(i * (-0.5)) + 2) * sizeMax,
                        Stretch = System.Windows.Media.Stretch.Fill,
                        Fill = new SolidColorBrush(Color.FromArgb(255, 255, 0, 255)),
                    },
                    Position = new Point(i * SEPARATION - ((particleCount * SEPARATION) / 2), -Math.Sin(i * 0.5) * YMax)
                };
                particleList2.Add(p2);
                Canvas.SetLeft(p2.Shape, p2.Position.X);
                Canvas.SetTop(p2.Shape, p2.Position.Y);
                containerParticles.Children.Add(p2.Shape);
            }
        }

        /// <summary>
        /// 更新粒子位置及大小
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < particleCount; i++)
            {
                var p1 = particleList1[i];
                p1.Position.Y = Math.Sin((i + count) * 0.5) * YMax;
                p1.Shape.Width = p1.Shape.Height = (Math.Sin((i + count) * 0.5) + 2) * sizeMax;
                Canvas.SetTop(p1.Shape, p1.Position.Y);

                var p2 = particleList2[i];
                p2.Position.Y = -Math.Sin((i + count) * 0.5) * YMax;
                p2.Shape.Width = p2.Shape.Height = (Math.Cos((i + count) * (-0.5)) + 2) * sizeMax;
                Canvas.SetTop(p2.Shape, p2.Position.Y);
            }
            count += 0.05;
        }
    }
}
