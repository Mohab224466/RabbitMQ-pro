using System;
using System.Text;
using RabbitMQ.Client;


//create factory
var factory = new ConnectionFactory();
// factory configuration
factory.Uri = new Uri("amqps://tgsjeyec:IWm5nKKnB-Hx1h5QVVaeY16_NoquIOf-@hummingbird.rmq.cloudamqp.com:5671/tgsjeyec");
// factory.UserName="tgsjeyec";
// factory.Password="IWm5nKKnB-Hx1h5QVVaeY16_NoquIOf-";
// factory.HostName="hummingbird.rmq.cloudamqp.com";
// create connection
using var connection = factory.CreateConnection();
// create channel (delivery boy)
using var channel = connection.CreateModel();

 channel.ExchangeDeclare(exchange:"test_exchange",type:"direct");
// queue configuration 
channel.QueueDeclare(queue:"test_queue", durable:false,
exclusive:false, autoDelete:false,arguments:null );
channel.QueueBind(queue:"test_queue", exchange:"test_exchange",routingKey:"test_key");

// message
var message = "This is my test message";


// encoding message
var encodedMessage = Encoding.UTF8.GetBytes(message);

IBasicProperties props = channel.CreateBasicProperties();
props.ContentType="text/plain";

channel.BasicPublish("test_exchange","test_key",props,encodedMessage);
//publishing
// channel.BasicPublish(exchange:"test_exchange",
//                     routingKey:"test_key",
//                     IBasicProperties:props,
//                     body: encodedMessage );

Console.WriteLine($"Published Message:{message}");

// connection.Close();
