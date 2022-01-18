using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

namespace WPF样式收集.Pages.图片变换动画
{
    class RadialAnimation
    {
        DispatcherTimer _timer = null;
        Ellipse _grdMain = null;
        ContentPresenter _circleContentPresenter = null;
        LineSegment LineSegment2 = null;
        PathFigure PathFigure1 = null;
        bool ISIncrementdirectionX = true;
        bool IsIncrementX = true;
        bool IsIncrementY = true;
        LineSegment LineSegmentFirstcorner = null;
        LineSegment LineSegmentseconcorner = null;
        LineSegment LineSegmentThirdcorner = null;
        LineSegment LineSegmentFourthcorner = null;
        double _ofsetOfAnimation = 1;
        double _height = 0;
        double _width = 0;
        public event Action AnimationCompleted;

        public void MakeRadiaAnimation(FrameworkElement animatedElement, double width, double height, TimeSpan timeSpan)
        {

            _height = height;
            _width = width;
            double steps = 2 * (_height + _width);
            double tickTime = timeSpan.TotalSeconds / steps;
            string topCentre = Convert.ToString(_width / 2) + ",0";
            string centre = Convert.ToString(_width / 2) + "," + Convert.ToString(_height / 2);

            int totalanimationtime = 32;



            PathGeometry PathGeometry1 = new PathGeometry();
            //Path1.Data = PathGeometry1;

            PathFigure1 = new PathFigure();
            PathFigure1.StartPoint = ((Point)new PointConverter().ConvertFromString(centre));
            PathGeometry1.Figures.Add(PathFigure1);

            LineSegment LineSegmentdummy = new LineSegment();
            LineSegmentdummy.Point = ((Point)new PointConverter().ConvertFromString(topCentre));
            PathFigure1.Segments.Add(LineSegmentdummy);


            LineSegmentseconcorner = new LineSegment();
            LineSegmentseconcorner.Point = ((Point)new PointConverter().ConvertFromString(topCentre));
            PathFigure1.Segments.Add(LineSegmentseconcorner);

            LineSegmentThirdcorner = new LineSegment();
            LineSegmentThirdcorner.Point = ((Point)new PointConverter().ConvertFromString(topCentre));
            PathFigure1.Segments.Add(LineSegmentThirdcorner);

            LineSegmentFourthcorner = new LineSegment();
            LineSegmentFourthcorner.Point = ((Point)new PointConverter().ConvertFromString(topCentre));
            PathFigure1.Segments.Add(LineSegmentFourthcorner);



            LineSegmentFirstcorner = new LineSegment();
            LineSegmentFirstcorner.Point = ((Point)new PointConverter().ConvertFromString(topCentre));
            PathFigure1.Segments.Add(LineSegmentFirstcorner);

            LineSegment2 = new LineSegment();
            LineSegment2.Point = ((Point)new PointConverter().ConvertFromString(topCentre));
            PathFigure1.Segments.Add(LineSegment2);

            animatedElement.Clip = PathGeometry1;

            PointAnimationUsingKeyFrames pointAnimationUsingKeyFrames = new PointAnimationUsingKeyFrames();
            DiscretePointKeyFrame discretePointKeyFrame = new DiscretePointKeyFrame();
            discretePointKeyFrame.Value = new Point(0, 0);

            TimeSpan keyTime = new TimeSpan(0, 0, 1);
            discretePointKeyFrame.KeyTime = keyTime;


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(tickTime);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.IsEnabled = true;



        }


        void _timer_Tick(object sender, EventArgs e)
        {
            if ((LineSegment2.Point.X <= 0) && (LineSegment2.Point.Y <= 0))
            {

                LineSegmentFirstcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                ISIncrementdirectionX = true;
                IsIncrementX = true;
            }
            else if (((LineSegment2.Point.X >= _width) && (LineSegment2.Point.Y <= 0)))
            {
                LineSegmentseconcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                LineSegmentThirdcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                LineSegmentFourthcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                LineSegmentFirstcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                ISIncrementdirectionX = false;
                IsIncrementY = true;
            }

            else if ((LineSegment2.Point.X >= _width) && (LineSegment2.Point.Y >= _height))
            {
                LineSegmentThirdcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                LineSegmentFourthcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                LineSegmentFirstcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                ISIncrementdirectionX = true;
                IsIncrementX = false;
            }

            else if ((LineSegment2.Point.X <= 0) && (LineSegment2.Point.Y >= _height))
            {
                LineSegmentFourthcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                LineSegmentFirstcorner.Point = new Point(LineSegment2.Point.X, LineSegment2.Point.Y);
                ISIncrementdirectionX = false;
                IsIncrementY = false;
            }

            double x = 0, y = 0;

            if (ISIncrementdirectionX == true)
            {
                if (IsIncrementX)
                {
                    x = LineSegment2.Point.X + _ofsetOfAnimation;
                    y = LineSegment2.Point.Y;
                }
                else
                {
                    x = LineSegment2.Point.X - _ofsetOfAnimation;
                    y = LineSegment2.Point.Y;
                }
            }
            else
            {
                if (IsIncrementY)
                {
                    x = LineSegment2.Point.X;
                    y = LineSegment2.Point.Y + _ofsetOfAnimation;
                }
                else
                {
                    x = LineSegment2.Point.X;
                    y = LineSegment2.Point.Y - _ofsetOfAnimation;
                }

            }

            LineSegment2.Point = new Point(x, y);

            if ((LineSegment2.Point.X == _width / 2) && (LineSegment2.Point.Y == 0))
            {
                _timer.IsEnabled = false;
                if (AnimationCompleted != null)
                {
                    AnimationCompleted();
                }
            }

        }
    }
}
