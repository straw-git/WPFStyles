using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace WPF样式收集.Pages.背景动画
{
    public class Particle
    {
        /// <summary>
        /// 形状
        /// </summary>
        public Ellipse Shape;
        /// <summary>
        /// 坐标
        /// </summary>
        public Point Position;
        /// <summary>
        /// 速度
        /// </summary>
        public Vector Velocity;
        /// <summary>
        /// 粒子和线段的集合
        /// </summary>
        public Dictionary<Particle, Line> ParticleLines;
    }
}
