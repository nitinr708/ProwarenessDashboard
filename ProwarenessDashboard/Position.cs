using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ProwarenessDashboard
{
     public static class Position
    {
        public enum Corner
        {
            LeftTop,
            LeftBottom,
            RightTop,
            RightBottom
        }
        public static Point GetAbsolutePosition(FrameworkElement e)
        {
            return GetAbsolutePosition(e, Corner.LeftTop);
        }
        public static Point GetAbsolutePosition(FrameworkElement e, Corner p)
        {
            return GetRelativePosition(e, Application.Current.RootVisual as FrameworkElement, p);
        }
        public static Point GetRelativePosition(FrameworkElement e, FrameworkElement relativeTo)
        {
            return GetRelativePosition(e, relativeTo, Corner.LeftTop);
        }
        public static Point GetRelativePosition(FrameworkElement e, FrameworkElement relativeTo, Corner p)
        {
            GeneralTransform gt = e.TransformToVisual(relativeTo);
            Point po = new Point();
            if (p == Corner.LeftTop)
                po = gt.Transform(new Point(0, 0));
            if (p == Corner.LeftBottom)
                po = gt.Transform(new Point(0, e.ActualHeight));
            if (p == Corner.RightTop)
                po = gt.Transform(new Point(e.ActualWidth, 0));
            if (p == Corner.RightBottom)
                po = gt.Transform(new Point(e.ActualWidth, e.ActualHeight));
            return po;
        }
    }
}
