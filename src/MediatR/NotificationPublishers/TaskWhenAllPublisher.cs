﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.NotificationPublishers;

/// <summary>
/// Uses Task.WhenAll with the list of Handler tasks:
/// 将 Task.WhenAll 与处理程序任务列表一起使用
/// <code>
/// var tasks = handlers
///                .Select(handler => handler.Handle(notification, cancellationToken))
///                .ToList();
/// 
/// return Task.WhenAll(tasks);
/// </code>
/// </summary>
public class TaskWhenAllPublisher : INotificationPublisher
{
    public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
    {
        var tasks = handlerExecutors
            .Select(handler => handler.HandlerCallback(notification, cancellationToken))
            .ToArray();

        return Task.WhenAll(tasks);
    }
}