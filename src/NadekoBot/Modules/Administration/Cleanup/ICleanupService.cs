namespace NadekoBot.Modules.Administration.Cleanup;

public interface ICleanupService
{
    Task<KeepResult?> DeleteMissingGuildDataAsync();
    Task<bool> KeepGuild(ulong guildId);
    Task<int> GetKeptGuildCount();
    Task StartLeavingUnkeptServers(int shardId);
}