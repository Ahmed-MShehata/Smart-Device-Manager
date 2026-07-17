namespace SDM.Domain.Enums;

/// <summary>
/// Represents the synchronization status of an item in the local offline queue.
/// Used by the SQLite client database only — not persisted on the server.
/// </summary>
public enum SyncStatus
{
    /// <summary>Item is queued and waiting to be synchronized.</summary>
    Pending = 0,

    /// <summary>Synchronization is currently in progress.</summary>
    InProgress = 1,

    /// <summary>Item has been successfully synchronized with the server.</summary>
    Synced = 2,

    /// <summary>Synchronization failed — will be retried.</summary>
    Failed = 3
}
