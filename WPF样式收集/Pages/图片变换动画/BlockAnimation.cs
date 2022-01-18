using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace WPF样式收集.Pages.图片变换动画
{
    class BlockAnimation
    {
        DispatcherTimer _timer = null;
        PathGeometry pathGeometry = null;
        double _rectangleSize = 80;
        double _numberofrectangles = 0;
        RandomNumberFromAGivenSetOfNumbers rdm = null;
        int _numberOfalreadyDrawnRectangles = 0;
        double _height = 0;
        double _width = 0;
        FrameworkElement _animatedElement = null;
        public event Action AnimationCompleted;
        public void MakeBlockAnimation(FrameworkElement animatedElement, double width, double height, TimeSpan timeSpan)
        {

            _animatedElement = animatedElement;
            _height = width;
            _width = height;
            if (_height > _width)
            {
                _numberofrectangles = Math.Ceiling(_height / _rectangleSize);
            }
            else
            {
                _numberofrectangles = Math.Ceiling(_width / _rectangleSize);

            }

            rdm = new RandomNumberFromAGivenSetOfNumbers(0, Convert.ToInt32(_numberofrectangles * _numberofrectangles - 1));

            double steps = _numberofrectangles * _numberofrectangles;
            double tickTime = timeSpan.TotalSeconds / steps;

            pathGeometry = new PathGeometry();
            _animatedElement.Clip = pathGeometry;
            _timer = new DispatcherTimer(DispatcherPriority.Input);
            _timer.Interval = TimeSpan.FromSeconds(tickTime);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.IsEnabled = true;

        }

        void _timer_Tick(object sender, EventArgs e)
        {

            int random = rdm.Next();
            int i = random / (int)_numberofrectangles;
            int j = random % (int)_numberofrectangles;
            RectangleGeometry myRectGeometry2 = new RectangleGeometry();
            myRectGeometry2.Rect = new Rect(j * _rectangleSize, i * _rectangleSize, _rectangleSize, _rectangleSize);
            pathGeometry = Geometry.Combine(pathGeometry, myRectGeometry2, GeometryCombineMode.Union, null);
            _animatedElement.Clip = pathGeometry;

            if (_numberOfalreadyDrawnRectangles == _numberofrectangles * _numberofrectangles)
            {
                _timer.IsEnabled = false;
                if (AnimationCompleted != null)
                {
                    AnimationCompleted();
                }
            }

            _numberOfalreadyDrawnRectangles++;

        }


    }

    class RandomNumberFromAGivenSetOfNumbers
    {
        List<int> _setOfNumbers = new List<int>();
        Random _random = new Random();
        public RandomNumberFromAGivenSetOfNumbers(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                _setOfNumbers.Add(i);
            }
        }

        public int Next()
        {
            if (_setOfNumbers.Count > 0)
            {
                int nextNumberIndex = _random.Next(_setOfNumbers.Count);
                int val = _setOfNumbers[nextNumberIndex];
                _setOfNumbers.RemoveAt(nextNumberIndex);
                return val;
            }
            return -1;

        }
    }
}
