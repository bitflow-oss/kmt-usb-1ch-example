using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace usbrelay
{
    public sealed class Communication
    {

        SerialPort sp = new SerialPort();
        private static Communication communication;

        private Communication()
        {

        }


        public static Communication Instance()
        {
            if (communication == null)
            {
                communication = new Communication();
            }

            return communication;
        }



        public void Communication_Set(int baudrate, int databits)
        {
            sp.BaudRate = baudrate;
            sp.DataBits = databits;
            sp.ReadTimeout = 500;
            sp.WriteTimeout = 500;
        }

        public string[] selectCOM()
        {
            return SerialPort.GetPortNames();
        }

        public string openCOM(string combosel)
        {
            if (!sp.IsOpen)
            {

                sp.PortName = combosel;
                try
                {
                    sp.Open();
                    return "The port has been opened!";
                }

                catch
                {
                    return "Can not open the port!";
                }
            }

            else
            {
                return "The port is already opened!";
            }
        }

        public string CloseCOM()
        {
            if (sp.IsOpen)
            {
                try
                {
                    sp.Close();
                    return "The port has been closed";
                }

                catch
                {
                    return "The port can not be closed";
                }
            }

            else
            {
                return "The port is already closed!";
            }
        }



        public bool serialWrite(byte[] dataBuff, int offset, int count)
        {
            try
            {
                if (sp.IsOpen) { sp.Write(dataBuff, offset, count); return true; }
                else { return false; }
            }

            catch
            {
                return false;
            }
        }

        public byte serialRead()
        {
            return (byte)sp.ReadByte();
        }

        /*public int serialBytesToRead()
        {
            return sp.BytesToRead;
        }*/


        public bool serialIsOpen()
        {
            if (sp.IsOpen) return true;
            else return false;
        }

    }
}
