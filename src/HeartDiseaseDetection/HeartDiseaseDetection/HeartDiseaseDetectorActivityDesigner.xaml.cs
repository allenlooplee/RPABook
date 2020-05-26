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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeartDiseaseDetection
{
    // Interaction logic for HeartDiseaseDetectorActivityDesigner.xaml
    public partial class HeartDiseaseDetectorActivityDesigner
    {
        public HeartDiseaseDetectorActivityDesigner()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".zip", // Default file extension
                Filter = "Model files (.zip)|*.zip", // Filter files by extension
                CheckPathExists = true
            };

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                ModelItem.Properties["ModelFilePath"].SetValue(new System.Activities.InArgument<string>(dlg.FileName));
            }
        }
    }
}
