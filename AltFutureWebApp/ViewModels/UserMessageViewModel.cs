using AltFutureWebApp.Data.Enums;

namespace AltFutureWebApp.ViewModels
{
    public class UserMessageViewModel
    {
        public UserMessageTypes UserMessageType { get; set; } = UserMessageTypes.System;
        public string UserMessage { get; set; } = String.Empty;

        public int FadeOutSeconds { get; set; } = 8;
    }
}
