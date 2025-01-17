using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using static System.Net.Mime.MediaTypeNames;

namespace HomeWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Socket server;
        private void ConnectBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ip = IPAddress.Parse(IpAdressTB.Text);
                var serverEndPoint = new IPEndPoint(ip, int.Parse(PortTB.Text));
                //Сокет, який буде відправляти запити на сервер
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                server.Connect(serverEndPoint); //Клієнт пробує підключитися до сервера 
                MessageBox.Show("Напишіть текст, який потрібно відправити");
            }
            catch (Exception ex)
            {
            }
        }

        private void SendBT_Click(object sender, RoutedEventArgs e)
        {         
            string text = this.ResultTB.Text;
            var data = Encoding.Unicode.GetBytes(text);
            server.Send(data);//відправляємо дані на сервер
                              //очікуємо відповіді від сервера
            data = new byte[1024];
            int bytes = 0;
            do
            {
                bytes = server.Receive(data); // отримали відповідь від сервера 
                Console.WriteLine($"Сервер відповів {Encoding.Unicode.GetString(data)}");
            } while (server.Available > 0);
        }


    }
}
