using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;


namespace WPF_TelegramBot
{
    internal class TelegramMessageClient
    {
        private TelegramBotClient _botClient = new TelegramBotClient("5448195820:AAFr1OH_6camjeRqsMxilfW8AZyVajXDZRs");

        private MainWindow w;

        public ObservableCollection<MessageLog> BotMessageLog { get; set; }

        //receives messages
        public TelegramMessageClient(MainWindow W)
        {
            this.BotMessageLog = new ObservableCollection<MessageLog>();
            this.w = W;

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                if (update.Message is not { } message)
                    return;

                Console.WriteLine("---");
                Debug.WriteLine("+++---");

                string text = $"{DateTime.Now.ToLongTimeString()}: {message.Chat.FirstName} {message.Chat.Id} {message.Text}";

                Debug.WriteLine($"{text} TypeMessage: {message.Type.ToString()}");

                var messageText = message.Text;

                w.Dispatcher.Invoke(() =>
                {
                    BotMessageLog.Add(
                    new MessageLog(
                    DateTime.Now.ToLongTimeString(), messageText, message.Chat.FirstName, message.Chat.Id));
                });
            }

            _botClient.StartReceiving(
                   updateHandler: HandleUpdateAsync,
                   pollingErrorHandler: HandlePollingErrorAsync
               );
        }
        
        public void SendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            _botClient.SendTextMessageAsync(id, Text);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
