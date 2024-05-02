namespace Mqtt.Subscriber
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SubscribeManager subscribeManager = new SubscribeManager();
            subscribeManager.Run();
            Console.ReadLine();
        }
    }
}
