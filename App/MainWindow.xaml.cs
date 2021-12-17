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
using System.Threading;
using System.IO.Ports;
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
        Communication comm;
        

        public MainWindow()
        {
            this.serialPort1 = new System.IO.Ports.SerialPort(new System.ComponentModel.Container());
            InitializeComponent();
            comm = Communication.Instance();
            comm.Communication_Set(9600, 8);
            
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
            if (!serialPort1.IsOpen)
            {
                // byte[] rxByte = new byte[3];
                try
                {
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialPort1.Open();
                    //Thread.Sleep(50);\
                    serialPort1.Write(new byte[] { 0xFF, 0x01, 0x03 }, 0, 3);
                    int result1 = serialPort1.ReadByte();
                    int result2 = serialPort1.ReadByte();
                    int result3 = serialPort1.ReadByte();
                    Console.WriteLine("0 ： " + result1);
                    Console.WriteLine("1 ： " + result2);
                    Console.WriteLine("2 ： " + result3);
                    if (result3==1)
                    {
                        // ON
                        label4.Content = "Relay State is ON!";
                        panel2.BackColor = Color.Lime;

                    } else
                    {
                        // OFF
                        label4.Content = "Relay State is OFF!";
                        panel2.BackColor = Color.Red;
                    }
                    label1.Content = "The port has been opened!";
                }

                catch
                {
                    label1.Content = "First select COM port!";
                }
            }

            else
            {
                 label1.Content = "The port is already opened!";
            }
            
            
        }

       
        private void DisconnectUsb(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Close();
                    label1.Content = "Port successfully closed!";
                }

                catch
                {
                    label1.Content = "The port can not be closed";
                }
            }

            else
            {
                label1.Content = "The port is already closed!";
            }

        }

         private void RelayOpen(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(new byte[] { 0xFF, 0x01, 0x01 }, 0, 3);
                panel1.BackColor = Color.Lime;
                label2.Content = "Relay is opened successfully!";
            }

            else
            {
                label2.Content = "The port is already closed!";
            }

        }

        private void RelayClose(object sender, RoutedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(new byte[] { 0xFF, 0x01, 0x00 }, 0, 3);
                panel1.BackColor = Color.Red;
                label2.Content = "Relay successfully closed!";
            }

            else
            {
                label2.Content = "The port can not be opened!";
            }

        }

       /* 
        public int serialBytesToRead()
        {
            return serialPort1.BytesToRead;
        }
        private string ByteToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();

        }*/

    }

}
