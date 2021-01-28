using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DashborderTest
{
    public class DashedBorder :Frame
    {
        public static readonly BindableProperty DashedStrokeProperty = BindableProperty.Create(
        propertyName: "DashedStroke",
        returnType: typeof(double),
        declaringType: typeof(DashedBorder),
        defaultValue: default(double));

        public double DashedStroke
        {
            get { return (double)GetValue(DashedStrokeProperty); }
            set { SetValue(DashedStrokeProperty, value); }
        }

        public static readonly BindableProperty StrokeBrushProperty = BindableProperty.Create(
        propertyName: "StrokeBrush",
        returnType: typeof(Color),
        declaringType: typeof(DashedBorder),
        defaultValue: default(Color));

        public Color StrokeBrush
        {
            get { return (Color)GetValue(StrokeBrushProperty); }
            set { SetValue(StrokeBrushProperty, value); }
        }
    }
}
