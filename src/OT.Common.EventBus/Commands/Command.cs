﻿using System;
using OT.Common.EventBus.Events;

namespace OT.Common.EventBus.Commands
{
    public abstract class Command : Message
    {

        public DateTime Timestamp { get;protected set; }
        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
