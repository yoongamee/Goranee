using System;

namespace Goranee
{
    public class MessageEx : baseMessage
    {
        public MessageExtraInfoEx ExtraInfo { get; set; }

        public MessageEx()
        {
            SwallowMSG = false;
        }

        public override void Execute()
        {
            if ( ExtraInfo != null)
            {
                ExtraInfo.Execute();
            }
        }
        public MessageEx(IMessageProc sender, IMessageProc receiver, int messageID, float dispatchTime,
            bool swallow, MessageExtraInfoEx extraInfo)
        {
			Set(sender, receiver, messageID, dispatchTime, swallow, extraInfo);
        }

        public void Set(IMessageProc sender, IMessageProc receiver, int messageID, float dispatchTime,
            bool swallow, MessageExtraInfoEx extraInfo)
        {
            Set(sender, receiver, messageID, dispatchTime, swallow);
            ExtraInfo = extraInfo;
        }
    }
}