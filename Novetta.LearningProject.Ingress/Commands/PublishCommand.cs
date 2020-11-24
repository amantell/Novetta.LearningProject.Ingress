using System;
using System.Linq;
using RabbitMQ.Client;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using System.Dynamic;
using ServiceStack;

namespace Novetta.LearningProject.Ingress.Commands
{
    class PublishCommand : ACommand
    {
        ConnectionFactory _factory;
        ACommand _lowerChain;
        byte[] _encodedArrivals;
        byte[] _encodedDepartures;

        public PublishCommand(ACommand command)
        {
            _lowerChain = command;
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public override bool Validate()
        {
            bool result = false;

            if (!PrepareData()) return false;

            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                result = Publish(channel);
            }

            return result;
        }

        private bool PrepareData()
        {
            if (!(_lowerChain is DataCommand)) return false;
            if (_lowerChain is DataCommand dataCommand)
            {
                try
                {
                    var arrivals = dataCommand.Data["arrivals"];
                    var departures = dataCommand.Data["departures"];

                    _encodedArrivals = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(arrivals));
                    _encodedDepartures = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(departures));
                } catch (Exception exception)
                {
                    _lowerChain.Error = exception.Message;
                    return false;
                }
            }
            return true;
        }

        private bool Publish(IModel channel)
        {
            try
            {
                PublishArrivals(channel);
                PublishDepartures(channel);
                return true;
            }
            catch (Exception exception)
            {
                _lowerChain.Error = exception.Message;
                return false;
            }
        }

        private bool PublishArrivals(IModel channel)
        {
            try
            {

                channel.ExchangeDeclare(exchange: "flights", type: ExchangeType.Direct);
                channel.BasicPublish(exchange: "flights",
                                        routingKey: "arrivals",
                                        basicProperties: null,
                                        body: _encodedArrivals);
                Console.WriteLine(" [x] Sent {0}", "Arrivals");
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool PublishDepartures(IModel channel)
        {
            try
            {
                channel.ExchangeDeclare(exchange: "flights", type: ExchangeType.Direct);
                channel.BasicPublish(exchange: "flights",
                                        routingKey: "departures",
                                        basicProperties: null,
                                        body: _encodedDepartures);
                Console.WriteLine(" [x] Sent {0}", "Departures");

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
