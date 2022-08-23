using System;
using System.Windows;
using System.Windows.Controls;

namespace Celic
{
	/// <summary>
	/// Description of SExpander.
	/// </summary>
	public class SExpander : Control
	{
		public readonly static DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(string), typeof(SExpander));
		public string Header
		{
			get {return GetValue(HeaderProperty).ToString();}
			set {SetValue(HeaderProperty, value);}
		}
		public readonly static DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(DependencyObject), typeof(SExpander));
		public DependencyObject Content
		{
			get {return (DependencyObject)GetValue(ContentProperty);}
			set {SetValue(ContentProperty, value);}
		}
		private void Expanded_Click(object sender, RoutedEventArgs e)
		{
			(this.Template.FindName("expandedBtn", this) as Button).Visibility = Visibility.Visible;
			(this.Template.FindName("collapsedBtn", this) as Button).Visibility = Visibility.Collapsed;
			(this.Template.FindName("content", this) as ContentPresenter).Visibility = Visibility.Visible;
        }
        private void Collapsed_Click(object sender, RoutedEventArgs e)
        {
            (this.Template.FindName("expandedBtn", this) as Button).Visibility = Visibility.Collapsed;
			(this.Template.FindName("collapsedBtn", this) as Button).Visibility = Visibility.Visible;
			(this.Template.FindName("content", this) as ContentPresenter).Visibility = Visibility.Collapsed;
        }
        public override void OnApplyTemplate()
        {
        	base.OnApplyTemplate();
        	var mainBtn = this.Template.FindName("collapsedBtn", this) as Button;
        	mainBtn.Click += Expanded_Click;
        	var otherBtn = this.Template.FindName("expandedBtn", this) as Button;
        	otherBtn.Click += Collapsed_Click;
        }
	}
}
