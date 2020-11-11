namespace Message.Infrastructure.Repositories
{
    public interface IKeyGenerator
    {
        string GenerateForQueue(string senderUsername, string receiverUsername);
        string GenerateForHistory(string senderUsername, string receiverUsername);
    }
}