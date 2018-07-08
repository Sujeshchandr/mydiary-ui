using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;


namespace MyDiary.RabbitMq
{
    class Send
    {
        static void Main(string[] args)
        {
            RabbitMQ.Client.ConnectionFactory factory = new RabbitMQ.Client.ConnectionFactory() { HostName = "localhost" }; //localhost:5672 {Default RabbitMQ Port}
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("Heloo", false, false, false, null);

                    string message = "Hello World!";
                    byte[] body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "hello", null, body);
                    Console.WriteLine(" [Sender] Sent {0}", message);

                }
            }
            Console.ReadLine();
        }
    }
}
