using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using System.Threading;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

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

                string text = $"Date: {DateTime.Now.ToLongTimeString()}: Name: {message.Chat.FirstName} Chat Id: {message.Chat.Id} Message: {message.Text}";

                Debug.WriteLine($"{text} TypeMessage: {message.Type.ToString()}");

                var messageText = message.Text;

                switch(message.Type)
                {
                    case MessageType.Sticker: 
                        {
                            w.Dispatcher.Invoke(() =>
                            {
                                BotMessageLog.Add(
                                new MessageLog(
                                DateTime.Now.ToLongTimeString(), "*Sticker*", message.Chat.FirstName, message.Chat.Id));
                            });

                            text = $"Date: {DateTime.Now.ToLongTimeString()}: Name: {message.Chat.FirstName} Chat Id: {message.Chat.Id} Message: *Sticker*";

                            break;
                        }

                    case MessageType.Document:
                        {
                            w.Dispatcher.Invoke(() =>
                            {
                                BotMessageLog.Add(
                                new MessageLog(
                                DateTime.Now.ToLongTimeString(), "*Document*", message.Chat.FirstName, message.Chat.Id));
                            });

                            text = $"Date: {DateTime.Now.ToLongTimeString()}: Name: {message.Chat.FirstName} Chat Id: {message.Chat.Id} Message: *Document*";

                            break;
                        }

                    default:
                        {
                            w.Dispatcher.Invoke(() =>
                            {
                                BotMessageLog.Add(
                                new MessageLog(
                                DateTime.Now.ToLongTimeString(), messageText, message.Chat.FirstName, message.Chat.Id));
                            });

                            break;
                        }

                }
                
                    string jsonChat = JsonConvert.SerializeObject(text);

                    System.IO.File.AppendAllText("_chat.json", jsonChat + "\n");

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

        public void JsonBotMsg(string Text)
        {
            string jsonBot = JsonConvert.SerializeObject(Text);

            System.IO.File.AppendAllText("_chat.json", jsonBot + "\n");
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
