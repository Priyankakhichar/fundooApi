////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "MSMQ.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    using Experimental.System.Messaging;
    using System;
    public class MSMQ
    {
        public void SendEmailToQueue(string email)
        {
            MessageQueue msmq = null;
            const string QueueName = @".\private$\emailqueue";
            if (!MessageQueue.Exists(QueueName))
            {
                msmq = MessageQueue.Create(QueueName);
            }
            else
            {
                msmq = new MessageQueue(QueueName);
            }
            try
            {
                msmq.Send(email);
            }
            catch (MessageQueueException mqe)
            {
                Console.Write(mqe.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                msmq.Close();
            }

            Console.WriteLine("message Sent");
        }
    }
}
