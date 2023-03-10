using AltFutureWebApp.Data.Enums;
using AltFutureWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace AltFutureWebApp.Helpers
{
    public class UserMessagePartial
    {
        private readonly ITempDataDictionary _tempData;

        public UserMessagePartial(ITempDataDictionary tempData)
        {
            _tempData = tempData;
        }

        public void SetUserMessage(UserMessageTypes messageType, string message, int fadeOutSeconds = 8) {


            var userMessage = new UserMessageViewModel()
            {
                UserMessageType = messageType,
                UserMessage = message,
                FadeOutSeconds = fadeOutSeconds
            };

            _tempData["UserMessagePartial"] = JsonConvert.SerializeObject(userMessage);
        }

    }
}
