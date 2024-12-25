using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab1SISprog_Nazarov_StyleChangerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ServerIp = "127.0.0.1";
        private const int Port = 12345;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            var controlType = (controlComboBox.SelectedItem as System.Windows.Controls.ComboBoxItem)?.Content.ToString();
            var color = colorTextBox.Text;
            var fontSize = fontSizeSlider.Value;

            if (string.IsNullOrEmpty(controlType) || string.IsNullOrEmpty(color))
            {
                MessageBox.Show("Please select a control and enter a color.");
                return;
            }

            var styleData = $"{controlType};{color};Black;{fontSize}";

            try
            {
                var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(ServerIp, Port);
                clientSocket.Send(Encoding.UTF8.GetBytes(styleData));
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                MessageBox.Show("Style applied successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
