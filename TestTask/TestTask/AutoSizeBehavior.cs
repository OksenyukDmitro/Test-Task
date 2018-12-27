using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms
{
    using System;
    using System.Linq;
    public class AutoSizeBehavior : Behavior<ListView>
    {
        private ListView _listView;

        public static readonly BindableProperty ExtraSpaceProperty =
            BindableProperty.Create(nameof(ExtraSpace),
                                    typeof(double),
                                    typeof(AutoSizeBehavior),
                                    0d);

        public static readonly BindableProperty DefaultRowHeightProperty =
            BindableProperty.Create(nameof(DefaultRowHeight),
                              typeof(int),
                              typeof(AutoSizeBehavior),
                              25);

        public int DefaultRowHeight
        {
            get => (int)GetValue(DefaultRowHeightProperty);
            set => SetValue(DefaultRowHeightProperty, value);
        }
        public double ExtraSpace
        {
            get { return (double)GetValue(ExtraSpaceProperty); }
            set { SetValue(ExtraSpaceProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            _listView = bindable;
           
            _listView.PropertyChanged += (s, args) =>
            {
                
                var stringTag = _listView.ItemsSource?.Cast<string[]>();
             //   _listView.ItemsSource?;
            /*    if (stringTag.Count() > 0)
                {
                    foreach(var str in stringTag)
                    {
                        if(str.Length > 40)
                        {
                            ExtraSpace += 15;
                        }
                    }
                }*/
                var count = _listView.ItemsSource?.Cast<object>()?.Count();
                if (args.PropertyName == nameof(_listView.ItemsSource)
                        && count.HasValue
                        && count.Value > 0)
                {
                    int rowHeight = _listView.RowHeight > 0 ? _listView.RowHeight : DefaultRowHeight;
                    _listView.HeightRequest = rowHeight * count.Value + ExtraSpace;
                }
            };
        }
    }
}