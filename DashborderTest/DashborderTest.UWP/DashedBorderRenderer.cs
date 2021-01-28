using DashborderTest;
using DashborderTest.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;

[assembly: ExportRenderer(typeof(DashedBorder), typeof(DashedBorderRenderer))]
namespace DashborderTest.UWP
{
    public class DashedBorderRenderer : ViewRenderer<DashedBorder, DottedBorder>
    {
        DottedBorder _dottedBorder;
        FrameworkElement _navtiveContent;
        double defaultPadding = 4;
        bool isOpened;
       
        public DashedBorderRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<DashedBorder> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                _navtiveContent.Loaded -= Native_Loaded;
                _navtiveContent.SizeChanged -= Native_SizeChanged;
            }
            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    var stroke = Element.DashedStroke == 0 ? Element.DashedStroke : 2.0;
                    var borderColor = Element.BorderColor.ToWindowsColor() == null ? Element.BorderColor.ToWindowsColor() : Colors.Red;
                    Control.DashedStroke = new Windows.UI.Xaml.Media.DoubleCollection() { stroke };
                    Control.StrokeBrush = new Windows.UI.Xaml.Media.SolidColorBrush(borderColor);
                }
                else
                {
                    _dottedBorder = new DottedBorder();
                    _navtiveContent = Element.Content.GetOrCreateRenderer().GetNativeElement() as FrameworkElement;
                    _navtiveContent.Loaded += Native_Loaded;
                    _navtiveContent.SizeChanged += Native_SizeChanged;

                    var stroke = Element.DashedStroke == 0 ? 2.0 : Element.DashedStroke;
                    var borderColor = Element.BorderColor.ToWindowsColor() == null ? Element.BorderColor.ToWindowsColor() : Colors.Red;
                    _dottedBorder.DashedStroke = new Windows.UI.Xaml.Media.DoubleCollection() { stroke };
                    _dottedBorder.StrokeBrush = new Windows.UI.Xaml.Media.SolidColorBrush(borderColor);

                    SetNativeControl(_dottedBorder);
                }
            }

        }

        private void Native_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSize(sender);
        }
        private void Native_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSize(sender);
        }
        private void UpdateSize(object sender)
        {
            var content = sender as FrameworkElement;
            if (content is Windows.UI.Xaml.Controls.Image)
            {

                if (!isOpened)
                {
                    (content as Windows.UI.Xaml.Controls.Image).ImageOpened += (s, e) =>
                    {
                        isOpened = true;
                        var image = sender as Windows.UI.Xaml.Controls.Image;
                        _dottedBorder.Height = image.ActualHeight + defaultPadding;
                        _dottedBorder.Width = image.ActualWidth + defaultPadding;
                    };
                }
                else
                {
                    _dottedBorder.Height = content.ActualHeight + defaultPadding;
                    _dottedBorder.Width = content.ActualWidth + defaultPadding;
                }
                
            }
            _dottedBorder.Height = content.ActualHeight+defaultPadding;
            _dottedBorder.Width = content.ActualWidth + defaultPadding;
        }
        

     
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}
