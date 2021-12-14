using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

/**
 * KMtronic Usb 1ch relay control example
 */
namespace usbrelay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.IO.Ports.SerialPort serialPort1;

        public MainWindow()
        {
            this.serialPort1 = new System.IO.Ports.SerialPort(new System.ComponentModel.Container());
            InitializeComponent();
            panel1.BackColor = Color.Red;
            label3.Content = "Please select COM port";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames()) 
            comboBox1.Items.Add(com);
        }
        
        private void ConnectUsb(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
            }
            else
            {
                serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                serialPort1.Open();
                if (serialPort1.IsOpen)
                {
                    label1.Content = "Port is opened successfully!";
                }
                else
                {
                    label1.Content = "Impossible to open port!";
                }
            }
        }

        private void DisconnectUsb(object sender, RoutedEventArgs e)
        {
            serialPort1.Close();
            label1.Content = "Port successfully closed!";
        }

        private void RelayOpen(object sender, RoutedEventArgs e)
        {
            serialPort1.Write(new byte[] { 0xFF, 0x01, 0x01 }, 0, 3);
            panel1.BackColor = Color.Lime;
            label2.Content = "Relay is opened successfully!";
        }

        private void RelayClose(object sender, RoutedEventArgs e)
        {
            serialPort1.Write(new byte[] { 0xFF, 0x01, 0x00 }, 0, 3);
            panel1.BackColor = Color.Red;
            label2.Content = "Relay successfully closed!";
        }

    }

}
