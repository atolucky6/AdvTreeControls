using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            RunHub();

            Console.ReadLine();
        }

        private static void RunHub()
        {
            HubConnectionBuilder builder = new HubConnectionBuilder();
            builder.WithUrl("http://localhost:5000/afs", o =>
            {
            });
            builder.WithAutomaticReconnect(new AlwaysConnectPolicy(2000));

            HubConnection connection = builder.Build();

            connection.Closed += Connection_Closed;
            connection.Reconnected += Connection_Reconnected;
            connection.Reconnecting += Connection_Reconnecting;
            Connect(connection);
        }

        private static async void Connect(HubConnection connection)
        {
            try
            {
                Console.WriteLine("Start connect");
                await connection.StartAsync().ContinueWith(t =>
                {
                    if (connection.State == HubConnectionState.Connected)
                    {
                        Console.WriteLine($"Connect successfully");
                    }
                    else
                    {

                    }
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while connect: {ex.Message}");
                Connect(connection);
            }
        }

        private static System.Threading.Tasks.Task Connection_Reconnecting(Exception arg)
        {
            Console.WriteLine($"Connection start reconnecting: {arg.Message}");
            return Task.CompletedTask;
        }

        private static System.Threading.Tasks.Task Connection_Reconnected(string arg)
        {
            Console.WriteLine($"Connection was reconnected: {arg}");
            return Task.CompletedTask;
        }

        private static System.Threading.Tasks.Task Connection_Closed(Exception arg)
        {
            Console.WriteLine($"Connection was closed: {arg.Message}");
            return Task.CompletedTask;
        }
    }

    public class AlwaysConnectPolicy : IRetryPolicy
    {
        public int Interval { get; private set; }

        public AlwaysConnectPolicy(int interval)
        {
            Interval = interval;
        }

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            return TimeSpan.FromMilliseconds(Interval);
        }
    }
}
