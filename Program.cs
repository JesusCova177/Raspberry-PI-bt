using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Text;
using InTheHand.Net;
using System;

class BluetoothClientExample
{
    public void SendData(string serverMacAddress, string data)
    {
        BluetoothClient client = new BluetoothClient();
        BluetoothAddress addr = BluetoothAddress.Parse(serverMacAddress);
        BluetoothEndPoint ep = new BluetoothEndPoint(addr, BluetoothService.SerialPort);

        try
        {
            client.Connect(ep);
            var stream = client.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            Console.WriteLine("Datos enviados: " + data);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al enviar datos: " + ex.Message);
        }
        finally
        {
            if (client.Connected)
                client.Close();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BluetoothClientExample client = new BluetoothClientExample();
        client.SendData("001122334455", "¡Hola desde Raspberry Pi!");
    }
}
