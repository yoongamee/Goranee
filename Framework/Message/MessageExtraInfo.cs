// empty class 
// it's base class to handle extra infos. you need to inherit to use this class
namespace Goranee
{
    public class MessageExtraInfo
    {
        public MessageExtraInfo()
        {
        }

        public MessageExtraInfo(int ID)
        {
            MessageID = ID;
        }

        public int MessageID { get; set; }

        public virtual bool Executer()
        {
            return true;
        }
    }
}