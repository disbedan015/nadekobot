namespace NadekoBot.Modules.Administration.Cleanup;

public sealed class KeepReport
{
    public required int ShardId { get; init; }
    public required ulong[] GuildIds { get; init; }
}