using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.Client;
using HipchatApiV2;
using HipchatApiV2.Enums;
using HipchatApiV2.Requests;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hipApi = new HipchatClient("9MOu2XAKyDn6XRTvUQRxpc8TegMN1TiLI2fQlex8");
            var afApi = new AnytimeFitnessApiClient("http://api.anytimefitness.com", Guid.Parse("2314833F-CFC9-4C84-BB10-5574507DCB4A"));
            var room = hipApi.GetRoom("Anytimefitness");


            var lastTx = DateTime.UtcNow;
            var lastMessage = "";
            while (true)
            {
                var history = hipApi.GetRoomHistory(room.Name);

                var first = history.items[0];
                if (first.message == lastMessage)
                {
                    Thread.Sleep(30000);
                    continue;
                }
                if (first.from == "scotch_bot")
                {
                    hipApi.SendNotification(room.Id, new SendRoomNotificationRequest
                    {
                        Color = RoomColors.Purple,
                        Message = "@scotchbot I know you are, but what am I?",
                        MessageFormat = HipchatMessageFormat.Html,
                        Notify = false
                    });
                    Thread.Sleep(10000);
                }
            }
        }
    }
}
