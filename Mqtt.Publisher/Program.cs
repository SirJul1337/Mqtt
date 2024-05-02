using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;

namespace Mqtt.Publisher;

public class Program
{

    static async Task Main(string[] args)
    {
        PublishManager publishManager = new PublishManager();
        publishManager.Run();
        Console.ReadLine();
    }

}
