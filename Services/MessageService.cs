namespace Blazor_StudentInfoApp.Services
{
    public class MessageService : IMessageService
    {
        public string GetWelcomeMessage()
        {
            return "Welcome to Blazor with dependency injection.";
        }
    }
}
