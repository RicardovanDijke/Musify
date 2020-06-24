using User_Service.Entities;

namespace User_Service.Message
{
    public interface IUserUpdateSender
    {
        public void SendUpdate(string queueName, User user);
    }
}
