namespace Goranee
{
    public class Message
    {

        public IMessageProc<Message> Sender { get; protected set; }
        public IMessageProc<Message> Receiver { get; protected set; }
        public double DispatchTime { get; set; }
        public int ID { get; set; }
            
        public MessageExtraInfo ExtraInfo { get; set; }

        public Message()
        {

        }

        public void SetReceiver(IMessageProc<Message> newReceiver)
        {
            Receiver = newReceiver;
        }


        public Message(IMessageProc<Message> sender, IMessageProc<Message> receiver, int messageID, float dispatchTime,
            MessageExtraInfo extraInfo)
        {
			Set(sender, receiver, messageID, dispatchTime, extraInfo);
        }

        public void Set(IMessageProc<Message> sender, IMessageProc<Message> receiver, int messageID, float dispatchTime)
        {
            Sender = sender;
            Receiver = receiver;
            DispatchTime = dispatchTime;
            ID = messageID;
        }

        public void Set(IMessageProc<Message> sender, IMessageProc<Message> receiver, int messageID, float dispatchTime,
            MessageExtraInfo extraInfo)
        {
            Sender = sender;
            Receiver = receiver;
            DispatchTime = dispatchTime;
            ExtraInfo = extraInfo;
            ID = messageID;
        }
    }
}