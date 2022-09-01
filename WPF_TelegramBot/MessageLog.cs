﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace WPF_TelegramBot
{
    struct MessageLog
    {
        
        public string Time { get; set; }

        public long Id { get; set; }

        public string Msg { get; set; }

        public string FirstName { get; set; }


        public MessageLog(string Time, string Msg, string FirstName, long Id)
        {
            this.Time = Time;
            this.Msg = Msg;
            this.FirstName = FirstName;
            this.Id = Id;
        }




        //public override string ToString()
        //{
        //    return $"{Time} {Msg} {FirstName}";
        //}
    }
}
