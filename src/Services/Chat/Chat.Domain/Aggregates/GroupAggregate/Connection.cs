﻿namespace Chat.Domain.Aggregates.GroupAggregate;

public class Connection : BaseEntity
{
    public Connection()
    {
        
    }

    public Connection(string connectionId, string username)
    {
        ConnectionId = connectionId;
        Username = username;
    }
    public string ConnectionId { get; set; }
    public string Username { get; set; }
}