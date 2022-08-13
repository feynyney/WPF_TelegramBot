using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TelegramBot
{
    struct MessageLog
    {
        public string Time { get; set; }

        public string Msg { get; set; }


        public MessageLog(string Time, string Msg)
        {
            this.Time = Time;
            this.Msg = Msg;
        }
    }
}
