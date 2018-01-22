namespace Goranee
{
    public interface IMessageProc<T>
    {
        void ReceiveMessage(T message);
    }
}
