using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Linq;

//*****************************************************************************************
//                           LICENSE INFORMATION
//*****************************************************************************************
//   PCCom.SerialCommunication Version 1.0.0.0
//   Class file for managing serial port communication
//
//   Copyright (C) 2007  
//   Richard L. McCutchen 
//   Email: richard@psychocoder.net
//   Created: 20OCT07
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.
//*****************************************************************************************
namespace BluetoothTagEmulator
{
    public delegate void NewMessageRecived(List<byte> msg);
    class ComPortManager
    {
        #region Manager Enums
        /// <summary>
        /// enumeration to hold our transmission types
        /// </summary>
        public enum TransmissionType { Text, Hex }
        #endregion

        #region Manager Variables
        //property variables
        private int _baudRate;
        private Parity _parity;
        private StopBits _stopBits;
        private int _dataBits;
        private string _portName = string.Empty;
        private TransmissionType _transType = TransmissionType.Hex;
        //global manager variables
        private SerialPort comPort = new SerialPort();

        #endregion

        public event NewMessageRecived NewMessageRecived;

        #region Manager Properties
        /// <summary>
        /// Property to hold the BaudRate
        /// of our manager class
        /// </summary>
        public int BaudRate
        {
            get { return this._baudRate; }
            set { this._baudRate = value; }
        }

        /// <summary>
        /// property to hold the Parity
        /// of our manager class
        /// </summary>
        public Parity Parity
        {
            get { return this._parity; }
            set { this._parity = value; }
        }

        /// <summary>
        /// property to hold the StopBits
        /// of our manager class
        /// </summary>
        public StopBits StopBits
        {
            get { return this._stopBits; }
            set { this._stopBits = value; }
        }

        /// <summary>
        /// property to hold the DataBits
        /// of our manager class
        /// </summary>
        public int DataBits
        {
            get { return this._dataBits; }
            set { this._dataBits = value; }
        }

        /// <summary>
        /// property to hold the PortName
        /// of our manager class
        /// </summary>
        public string PortName
        {
            get { return this._portName; }
            set { this._portName = value; }
        }

        /// <summary>
        /// property to hold our TransmissionType
        /// of our manager class
        /// </summary>
        public TransmissionType CurrentTransmissionType
        {
            get { return this._transType; }
            set { this._transType = value; }
        }

        #endregion

        #region Manager Constructors
        public ComPortManager(ComPortSettings settings)
        {
            this._baudRate = settings.BaudRate;
            this._parity = settings.Parity;
            this._stopBits = settings.StopBits;
            this._dataBits = settings.DataBits;
            this._portName = settings.PortName;
            //now add an event handler
            this.comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);
        }
        #endregion

        #region WriteData
        public void WriteData(string msg)
        {
            try
            {
                msg = "FFFF" + msg + "FFFF";

                byte[] newMsg = HexToByte(msg);

                this.comPort.Write(newMsg, 0, newMsg.Length);

                Globals.DisplayData(Globals.MessageType.Outgoing, "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:ffff") + "] " + ByteToHex(newMsg));
            }
            catch (FormatException ex)
            {
                Globals.DisplayData(Globals.MessageType.Error, "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:ffff") + "] " + ex.Message);
            }
        }
        #endregion

        #region HexToByte
        /// <summary>
        /// method to convert hex string into a byte array
        /// </summary>
        /// <param name="msg">string to convert</param>
        /// <returns>a byte array</returns>
        public byte[] HexToByte(string msg)
        {
            //remove any spaces from the string
            msg = msg.Replace(" ", "");
            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            //loop through the length of the provided string
            for (int i = 0; i < msg.Length - 1; i += 2)
                //convert each set of 2 characters to a byte
                //and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            //return the array
            return comBuffer;
        }
        #endregion

        #region ByteToHex
        /// <summary>
        /// method to convert a byte array into a hex string
        /// </summary>
        /// <param name="comByte">byte array to convert</param>
        /// <returns>a hex string</returns>
        public string ByteToHex(byte[] comByte)
        {
            //create a new StringBuilder object
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            //loop through each byte in the array
            foreach (byte data in comByte)
                //convert the byte to a string and add to the stringbuilder
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0').PadRight(3, ' '));
            //return the converted value
            return builder.ToString().ToUpper();
        }
        #endregion

        #region OpenPort

        public bool OpenPort()
        {
            try
            {
                //first check if the port is already open
                //if its open then close it
                if (this.comPort.IsOpen == true) this.comPort.Close();

                //set the properties of our SerialPort Object
                this.comPort.BaudRate = this._baudRate; //BaudRate
                this.comPort.DataBits = this._dataBits; //DataBits
                this.comPort.StopBits = this._stopBits; //StopBits
                this.comPort.Parity = this._parity; //Parity
                this.comPort.PortName = this._portName; //PortName
                //now open the port
                this.comPort.Open();
                //display message
                Globals.DisplayData(Globals.MessageType.Normal, "Port opened at " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:ffff"));
                //return true
                return true;
            }
            catch (Exception ex)
            {
                Globals.DisplayData(Globals.MessageType.Error, ex.Message);
                return false;
            }
        }

        public void ClosePort()
        {
            this.comPort.Close();
            Globals.DisplayData(Globals.MessageType.Error, "Port closed at " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:ffff"));
        }

        #endregion

        #region comPort_DataReceived

        /// <summary>
        /// method that will be called when theres data waiting in the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var bytes = this.comPort.BytesToRead;
            var comBuffer = new byte[bytes];

            this.comPort.Read(comBuffer, 0, bytes);

            var msg = ByteToHex(comBuffer);

            Globals.DisplayData(Globals.MessageType.Incoming, "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:ffff") + "] " + msg);

            var messageList = new List<ReciveMessage> { new ReciveMessage { BodyBytes = new List<byte>() } };

            var length = comBuffer.Length;
            for (int i = 0; i < length; i++)
            {
                var part = comBuffer[i];
                var nextPart = i < length - 1 ? (byte?)comBuffer[i + 1] : null;

                var message = messageList.LastOrDefault();
                if (part == Globals.ComRequRespHeader && nextPart.HasValue && nextPart.Value == Globals.ComRequRespHeader)
                {
                    if (!message.StartHeader)
                    {
                        message.StartHeader = true;
                        i++;
                    }
                    else if (!message.EndHeader)
                    {
                        message.EndHeader = true;
                        i++;
                        if (i < length - 1)
                            messageList.Add(new ReciveMessage { BodyBytes = new List<byte>() });
                    }
                }
                else
                {
                    message.BodyBytes.Add(part);
                }
            }

            foreach (var m in messageList)
            {
                var body = m.BodyBytes.ToList();
                if (m.StartHeader && m.EndHeader && body.Any())
                    this.NewMessageRecived?.Invoke(body);
                else
                    Globals.DisplayData(Globals.MessageType.Warning, "Получен некорректный запрос");
            }
        }

        #endregion

        private class ReciveMessage
        {
            public bool StartHeader { get; set; }
            public List<byte> BodyBytes { get; set; }
            public bool EndHeader { get; set; }
        }
    }

    public class ComPortSettings
    {
        public int BaudRate;
        public int DataBits;
        public StopBits StopBits;
        public Parity Parity;
        public string PortName;
    }


}
