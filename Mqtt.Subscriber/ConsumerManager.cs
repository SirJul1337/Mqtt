using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using System.Text;
using MQTTnet.Packets;

namespace Mqtt.Subscriber
{
    public class SubscribeManager
    {
        private string BrokerAddress = ""; // Replace with your MQTT broker address
        private int BrokerPort = 1883; // Replace with your MQTT broker address
        private string Topic = "amq.topic"; // Replace with your desired topic

        public async void Run()
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            mqttClient.ConnectedAsync += async (e) =>
            {
                await mqttClient.SubscribeAsync(new MqttTopicFilter { Topic = Topic });
                Console.WriteLine($"Subscribed to topic: {Topic}");
            };

            mqttClient.ApplicationMessageReceivedAsync += async (e) =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                Console.WriteLine($"Received message: {message}");
            };
           

            await mqttClient.ConnectAsync(new MqttClientOptionsBuilder()
                .WithClientId("MySubscriber")
                .WithTcpServer(BrokerAddress, BrokerPort)
                .WithCredentials("mqtt_test", "mqtt_test")
                .Build());

            Console.WriteLine("Press any key to disconnect...");
            Console.ReadKey(true);

            await mqttClient.DisconnectAsync();
        }
    }
}
