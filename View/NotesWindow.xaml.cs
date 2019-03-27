using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace NotesApp.View
{
    /// <summary>
    /// Interaction logic for Notes_Window.xaml
    /// </summary>
    public partial class Notes_Window : Window
    {
        SpeechRecognitionEngine recognizer;
        public Notes_Window()
        {
            InitializeComponent();

            //var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
            //                     where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
            //                     select r).FirstOrDefault();
            //recognizer = new SpeechRecognitionEngine(currentCulture);

            //recognizer.SpeechRecognized += Recognize_SpeechRegognizer;

            //GrammarBuilder builder = new GrammarBuilder();
            //builder.AppendDictation();
            //Grammar grammar = new Grammar(builder);
            //recognizer.LoadGrammar(grammar);
            //recognizer.SpeechRecognized += Recognize_SpeechRegognizer;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyCombobox.ItemsSource = fontFamilies;

            List<double> fontSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 28, 48, 72 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        private void Recognize_SpeechRegognizer(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;
            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountofCharacters = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document length: {ammountofCharacters} characters";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            //var textToBold = new TextRange(contentRichTextBox.Selection.Start,contentRichTextBox.Selection.End);
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (!isButtonEnabled)
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                recognizer.RecognizeAsyncStop();
            }
        }

        private void ContentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedState = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            var selectedStateItalic = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            var selectedUnderline = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            boldButton.IsChecked = (selectedState != DependencyProperty.UnsetValue) && (selectedState.Equals(FontWeights.Bold));
            ItalicButton.IsChecked = (selectedStateItalic != DependencyProperty.UnsetValue) && (selectedStateItalic.Equals(FontStyles.Italic));
            Underline.IsChecked = (selectedUnderline != DependencyProperty.UnsetValue) && (selectedUnderline.Equals(TextDecorations.Underline));

            fontFamilyCombobox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            //fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;
            if (isButtonEnabled)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Baseline);
        }

        private void FontFamilyCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyCombobox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyCombobox.SelectedItem);
            }
        }

        private void FontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                LoginWindow1 loginWindow = new LoginWindow1();
                loginWindow.ShowDialog();
            }
        }

        private void FontFamilyCombobox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
