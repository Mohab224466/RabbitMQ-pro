using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
    
var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://tgsjeyec:IWm5nKKnB-Hx1h5QVVaeY16_NoquIOf-@hummingbird.rmq.cloudamqp.com:5671/tgsjeyec");

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

// channel.QueueDeclare(queue:"letterbox", durable:false,
// exclusive:false, autoDelete:false,arguments:null);

var consumer = new EventingBasicConsumer(channel);
// Subscribe
consumer.Received +=(Model,ea ) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"message recieved : {message}");
};

// using (var channel2 = connection.CreateModel()){
//     var consumer2 = new EventingBasicConsumer(channel);
//     // Subscribe
//     consumer.Received +=(Model,ea ) =>
//     {
//         var body = ea.Body.ToArray();
//         var message = Encoding.UTF8.GetString(body);
//         Console.WriteLine($"message recieved : {message}");
//     };
// }

channel.BasicConsume(queue:"test_queue",autoAck:true,consumer:consumer,consumerTag:"letter");

Console.ReadKey();



