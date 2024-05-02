using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet;
using System.Text;

namespace Mqtt.Publisher;

public class PublishManager
{
    private string BrokerAddress = ""; // Replace with your MQTT broker address
    private int BrokerPort = 1883; // Replace with your MQTT broker address
    private string Topic = "amq.topic"; // Replace with your desired topic
    private string Message = "Hello from C# MQTT app!";
    public PublishManager()
    {
        
    }
    public async void Run()
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        await mqttClient.ConnectAsync(new MqttClientOptionsBuilder()
            .WithClientId("MyPublisher")
            .WithTcpServer(BrokerAddress,BrokerPort)
            .WithCredentials("mqtt_test","mqtt_test")
            .Build());

        var messageBytes = Encoding.UTF8.GetBytes(Message);
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(Topic)
            .WithPayload(messageBytes)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();
        for (int i = 0; i < 1000; i++)
        {
            await mqttClient.PublishAsync(message);
            Thread.Sleep(1000);
        }

        await mqttClient.DisconnectAsync();

        Console.WriteLine($"Message published to topic: {Topic}");

    }
}
