using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using System.Linq;
using MapNotepad.Controls;

namespace Next.iOS
{
    public class UITouchesGestureRecognizer : UIGestureRecognizer
    {
        #region -- Private Members --

        private ClickableContentView _element;
        private UIView _nativeView;

        #endregion

        public UITouchesGestureRecognizer(ClickableContentView element, UIView nativeView)
        {
            if (element is null)
            {
                throw new ArgumentNullException("element");
            }

            if (nativeView is null)
            {
                throw new ArgumentNullException("nativeView");
            }

            _element = element;
            _nativeView = nativeView;
        }

        private IEnumerable<Point> GetTouchPoints(Foundation.NSSet touches)
        {
            var points = new List<Point>((int)touches.Count);

            foreach (UITouch touch in touches)
            {
                CGPoint touchPoint = touch.LocationInView(_nativeView);
                _nativeView.ConvertPointToCoordinateSpace(touchPoint, _nativeView.Window.Screen.FixedCoordinateSpace);
                points.Add(new Point((double)touchPoint.X, (double)touchPoint.Y));
            }

            return points;
        }

        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (this._element.TouchesBegan(GetTouchPoints(touches)))
            {
                this.State = UIGestureRecognizerState.Began;
            }
            else
            {
                this.State = UIGestureRecognizerState.Cancelled;
            }

        }

        public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            var points = GetTouchPoints(touches);

            if (this._element.TouchesMoved(points))
            {
                this.State = UIGestureRecognizerState.Changed;
            }
        }

        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (this._element.TouchesEnded(GetTouchPoints(touches)))
            {
                this.State = UIGestureRecognizerState.Ended;
            }

        }

        public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            this._element.TouchesCancelled(GetTouchPoints(touches));

            this.State = UIGestureRecognizerState.Cancelled;
        }
    }
}
