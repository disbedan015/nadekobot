#nullable disable
using NadekoBot.Common.Attributes;
using NadekoBot.Extensions;
using NadekoBot.Modules.Administration.Services;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace NadekoBot.Modules.Administration;

[Group]
public partial class VoiceCommands(AdministrationService service) : NadekoModule<AdministrationService>
{
    [Cmd]
    [RequireContext(ContextType.Guild)]
    [UserPerm(GuildPerm.DeafenMembers)]
    [BotPerm(GuildPerm.DeafenMembers)]
    public async Task Deafen(params IGuildUser[] users)
    {
        await service.DeafenUsers(true, users);
        await Response().Confirm(strs.deafen).SendAsync();
    }

    [Cmd]
    [RequireContext(ContextType.Guild)]
    [UserPerm(GuildPerm.DeafenMembers)]
    [BotPerm(GuildPerm.DeafenMembers)]
    public async Task UnDeafen(params IGuildUser[] users)
    {
        await service.DeafenUsers(false, users);
        await Response().Confirm(strs.undeafen).SendAsync();
    }

    [Cmd]
    [RequireContext(ContextType.Guild)]
    [UserPerm(GuildPerm.ManageChannels)]
    [BotPerm(GuildPerm.ManageChannels)]
    public async Task DelVoiChanl([Leftover] IVoiceChannel voiceChannel)
    {
        await voiceChannel.DeleteAsync();
        await Response().Confirm(strs.delvoich(Format.Bold(voiceChannel.Name))).SendAsync();
    }

    [Cmd]
    [RequireContext(ContextType.Guild)]
    [UserPerm(GuildPerm.ManageChannels)]
    [BotPerm(GuildPerm.ManageChannels)]
    public async Task CreatVoiChanl([Leftover] string channelName)
    {
        var ch = await ctx.Guild.CreateVoiceChannelAsync(channelName);
        await Response().Confirm(strs.createvoich(Format.Bold(ch.Name))).SendAsync();
    }
}