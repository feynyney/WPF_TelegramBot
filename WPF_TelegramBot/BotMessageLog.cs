using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TelegramBot
{
    struct BotMessageLog
    {
        public string Time { get; set; }


        public string Msg { get; set; }


        public BotMessageLog(string Time, string Msg)
        {
            this.Time = Time;
            this.Msg = Msg;
        }
    }
}
