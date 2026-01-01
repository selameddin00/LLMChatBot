using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;

namespace GeminiChatBot.Views;

/// <summary>
/// ChatMessage objesine gore mesaj balonu rengini belirler
/// </summary>
public class BooleanToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Models.ChatMessage message)
        {
            // Hata mesaji: kirmizi
            if (message.IsError)
                return new SolidColorBrush(Color.FromRgb(200, 50, 50)); // Kirmizi tonu
            
            // Kullanici mesaji: pastel soft blue
            if (message.IsUser)
                return new SolidColorBrush(Color.FromRgb(100, 149, 237)); // CornflowerBlue tonu
            
            // Bot mesaji: soft gray
            return new SolidColorBrush(Color.FromRgb(60, 60, 60)); // Koyu gri
        }
        
        // Geriye donus uyumlulugu icin bool kontrolu
        if (value is bool isUser)
        {
            if (isUser)
                return new SolidColorBrush(Color.FromRgb(100, 149, 237));
            return new SolidColorBrush(Color.FromRgb(60, 60, 60));
        }
        
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// ChatMessage objesine gore yatay hizalamayi belirler
/// </summary>
public class BooleanToHorizontalAlignmentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Models.ChatMessage message)
        {
            if (message.IsUser)
                return HorizontalAlignment.Right; // Kullanici mesaji sagda
            return HorizontalAlignment.Left; // Bot mesaji solda
        }
        
        // Geriye donus uyumlulugu icin bool kontrolu
        if (value is bool isUser && isUser)
            return HorizontalAlignment.Right;
        return HorizontalAlignment.Left;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// ChatMessage objesine gore metin rengini belirler
/// </summary>
public class BooleanToTextBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Models.ChatMessage message)
        {
            // Hata mesaji: beyaz
            if (message.IsError)
                return new SolidColorBrush(Colors.White);
            
            // Kullanici mesaji: beyaz
            if (message.IsUser)
                return new SolidColorBrush(Colors.White);
            
            // Bot mesaji: acik gri
            return new SolidColorBrush(Color.FromRgb(230, 230, 230)); // #E6E6E6
        }
        
        // Geriye donus uyumlulugu icin bool kontrolu
        if (value is bool isUser)
        {
            if (isUser)
                return new SolidColorBrush(Colors.White);
            return new SolidColorBrush(Color.FromRgb(230, 230, 230));
        }
        
        return new SolidColorBrush(Colors.White);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// ChatMessage objesine gore timestamp rengini belirler
/// </summary>
public class BooleanToTimestampBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Models.ChatMessage message)
        {
            // Hata mesaji: beyaz tonu
            if (message.IsError)
                return new SolidColorBrush(Color.FromRgb(240, 240, 240));
            
            // Kullanici mesaji: beyaz tonu
            if (message.IsUser)
                return new SolidColorBrush(Color.FromRgb(240, 240, 240));
            
            // Bot mesaji: gri tonu
            return new SolidColorBrush(Color.FromRgb(180, 180, 180));
        }
        
        // Geriye donus uyumlulugu icin bool kontrolu
        if (value is bool isUser)
        {
            if (isUser)
                return new SolidColorBrush(Color.FromRgb(240, 240, 240));
            return new SolidColorBrush(Color.FromRgb(180, 180, 180));
        }
        
        return new SolidColorBrush(Color.FromRgb(180, 180, 180));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// ChatMessage objesine gore MarkdownViewer Style'ini belirler
/// </summary>
public class BooleanToMarkdownViewerStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Models.ChatMessage message)
        {
            // Kullanici mesaji icin user style
            if (message.IsUser)
            {
                var userStyle = Application.Current.FindResource("MarkdownViewerUserStyle") as Style;
                return userStyle ?? Application.Current.FindResource("MarkdownViewerBotStyle") as Style;
            }
            
            // Bot mesaji icin bot style
            return Application.Current.FindResource("MarkdownViewerBotStyle") as Style;
        }
        
        // Varsayilan olarak bot style
        return Application.Current.FindResource("MarkdownViewerBotStyle") as Style;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

