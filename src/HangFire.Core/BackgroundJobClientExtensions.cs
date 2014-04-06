﻿using System;
using System.Linq.Expressions;
using HangFire.Common;
using HangFire.Common.States;
using HangFire.States;

namespace HangFire
{
    public static class BackgroundJobClientExtensions
    {
        /// <summary>
        /// Creates a background job based on a specified static method 
        /// call expression and places it into its actual queue. 
        /// Please, see the <see cref="QueueAttribute"/> to learn how to 
        /// place the job on a non-default queue.
        /// </summary>
        /// 
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Static method call expression that will be marshalled to the Server.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Enqueue(this IBackgroundJobClient client, Expression<Action> methodCall)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(methodCall, new EnqueuedState());
        }

        /// <summary>
        /// Creates a background job based on a specified instance method 
        /// call expression and places it into its actual queue. 
        /// Please, see the <see cref="QueueAttribute"/> to learn how to 
        /// place the job on a non-default queue.
        /// </summary>
        /// 
        /// <typeparam name="T">Type whose method will be invoked during job processing.</typeparam>
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Instance method call expression that will be marshalled to the Server.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Enqueue<T>(this IBackgroundJobClient client, Expression<Action<T>> methodCall)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(methodCall, new EnqueuedState());
        }

        /// <summary>
        /// Creates a background job based on a specified static method 
        /// call expression and places it into specified queue. 
        /// Please, note that the <see cref="QueueAttribute"/> can
        /// override the specified queue.
        /// </summary>
        /// 
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Instance method call expression that will be marshalled to the Server.</param>
        /// <param name="queue">Queue to which the job will be placed.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Enqueue(this IBackgroundJobClient client, Expression<Action> methodCall, string queue)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(methodCall, new EnqueuedState(queue));
        }

        /// <summary>
        /// Creates a background job based on a specified instance method 
        /// call expression and places it into specified queue. 
        /// Please, note that the <see cref="QueueAttribute"/> can
        /// override the specified queue.
        /// </summary>
        /// 
        /// <typeparam name="T">Type whose method will be invoked during job processing.</typeparam>
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Instance method call expression that will be marshalled to the Server.</param>
        /// <param name="queue">Queue to which the job will be placed.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Enqueue<T>(
            this IBackgroundJobClient client, Expression<Action<T>> methodCall, string queue)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(methodCall, new EnqueuedState(queue));
        }

        /// <summary>
        /// Creates a new background job based on a specified static method
        /// call expression and schedules it to be enqueued after a given delay.
        /// </summary>
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Instance method call expression that will be marshalled to the Server.</param>
        /// <param name="delay">Delay, after which the job will be enqueued.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Schedule(this IBackgroundJobClient client, Expression<Action> methodCall, TimeSpan delay)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(methodCall, new ScheduledState(delay));
        }

        /// <summary>
        /// Creates a new background job based on a specified instance method
        /// call expression and schedules it to be enqueued after a given delay.
        /// </summary>
        /// 
        /// <typeparam name="T">Type whose method will be invoked during job processing.</typeparam>
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Instance method call expression that will be marshalled to the Server.</param>
        /// <param name="delay">Delay, after which the job will be enqueued.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Schedule<T>(this IBackgroundJobClient client, Expression<Action<T>> methodCall, TimeSpan delay)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(methodCall, new ScheduledState(delay));
        }

        /// <summary>
        /// Creates a new background job based on a specified static method
        /// within a given state.
        /// </summary>
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Static method call expression that will be marshalled to the Server.</param>
        /// <param name="state">Initial state of a job.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Create(
            this IBackgroundJobClient client,
            Expression<Action> methodCall,
            State state)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(Job.FromExpression(methodCall), state);
        }

        /// <summary>
        /// Creates a new background job based on a specified instance method
        /// within a given state.
        /// </summary>
        /// 
        /// <typeparam name="T">Type whose method will be invoked during job processing.</typeparam>
        /// <param name="client">A job client instance.</param>
        /// <param name="methodCall">Instance method call expression that will be marshalled to the Server.</param>
        /// <param name="state">Initial state of a job.</param>
        /// <returns>Unique identifier of the created job.</returns>
        public static string Create<T>(
            this IBackgroundJobClient client,
            Expression<Action<T>> methodCall,
            State state)
        {
            if (client == null) throw new ArgumentNullException("client");

            return client.Create(Job.FromExpression(methodCall), state);
        }
    }
}
