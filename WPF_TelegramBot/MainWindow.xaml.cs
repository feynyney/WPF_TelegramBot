using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace WPF_TelegramBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelegramMessageClient client;

        List<string> BotMessages = new List<string>();



        DateTime TimeOfMsg = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            client = new TelegramMessageClient(this);

            logList.ItemsSource = client.BotMessageLog;

        }

        private void btnMsgSendClick(object sender, RoutedEventArgs e)
        {
            client.SendMessage(txtMsgSend.Text, TargetSend.Text);
            BotMessages.Add($"Bot says: {txtMsgSend.Text}");
            client.JsonBotMsg($"Date: {TimeOfMsg} Bot says: {txtMsgSend.Text}");
            txtMsgSend.Clear();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnMsgSend.IsEnabled = true;
        }
    }
}
