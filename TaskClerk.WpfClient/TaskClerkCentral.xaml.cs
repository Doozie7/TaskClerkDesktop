using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskClerk.WpfClient
{
    /// <summary>
    /// Interaction logic for TaskClerkCentral.xaml
    /// </summary>
    public partial class TaskClerkCentral : Window
    {
		private bool isDefault;
		FlowDocument startingFlowDoc = new FlowDocument();
		
        public TaskClerkCentral()
        {
            InitializeComponent();
   			Paragraph para = new Paragraph();
   			para.Inlines.Add(new Run("Comment on current task"));
    		startingFlowDoc.Blocks.Add(para);
			this.CommentText.Document = startingFlowDoc;
			isDefault = true;
		}

        private void CommentText_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
			if(isDefault)
			{
        		this.CommentText.Document.Blocks.Clear();
				isDefault = false;
			}
        }

        private void CommentText_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
			TextRange documentRange = new TextRange(this.CommentText.Document.ContentStart, this.CommentText.Document.ContentEnd);
        	if(documentRange.Text.Length < 3)
			{
                Paragraph para = new Paragraph();
                para.Inlines.Add(new Run("Comment on current task"));
                startingFlowDoc.Blocks.Add(para);
				this.CommentText.Document = startingFlowDoc;
				isDefault = true;
			}			
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }
    }
}
