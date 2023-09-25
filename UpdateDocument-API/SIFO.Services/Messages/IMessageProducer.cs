﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIFO.Services.Messages
{
    public interface IMessageProducer
    {
        public void SendingMessage<T>(T message);
    }
}