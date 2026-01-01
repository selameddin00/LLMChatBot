using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GeminiChatBot.Views;

/// <summary>
/// ScrollViewer icin auto-scroll davranisi saglayan Attached Property
/// </summary>
public static class ScrollViewerBehavior
{
    public static readonly DependencyProperty AutoScrollProperty =
        DependencyProperty.RegisterAttached(
            "AutoScroll",
            typeof(bool),
            typeof(ScrollViewerBehavior),
            new PropertyMetadata(false, OnAutoScrollChanged));

    public static bool GetAutoScroll(DependencyObject obj)
    {
        return (bool)obj.GetValue(AutoScrollProperty);
    }

    public static void SetAutoScroll(DependencyObject obj, bool value)
    {
        obj.SetValue(AutoScrollProperty, value);
    }

    private static void OnAutoScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ScrollViewer scrollViewer)
        {
            if ((bool)e.NewValue)
            {
                // ScrollViewer yuklendiginde ItemsControl'i bul ve event'leri bagla
                scrollViewer.Loaded += (sender, args) =>
                {
                    AttachCollectionChanged(scrollViewer);
                    scrollViewer.ScrollToEnd();
                };

                // Eger zaten yuklenmis ise hemen bagla
                if (scrollViewer.IsLoaded)
                {
                    AttachCollectionChanged(scrollViewer);
                    scrollViewer.ScrollToEnd();
                }
            }
        }
    }

    private static void AttachCollectionChanged(ScrollViewer scrollViewer)
    {
        var itemsControl = FindItemsControl(scrollViewer);
        if (itemsControl != null && itemsControl.ItemsSource is INotifyCollectionChanged collection)
        {
            collection.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    // UI thread'de calis
                    scrollViewer.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        scrollViewer.ScrollToEnd();
                    }), System.Windows.Threading.DispatcherPriority.Loaded);
                }
            };
        }
    }

    private static ItemsControl? FindItemsControl(DependencyObject parent)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is ItemsControl itemsControl)
            {
                return itemsControl;
            }
            var result = FindItemsControl(child);
            if (result != null)
                return result;
        }
        return null;
    }
}

