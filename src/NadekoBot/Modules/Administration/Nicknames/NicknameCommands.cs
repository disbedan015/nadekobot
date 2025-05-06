#nullable disable
namespace NadekoBot.Modules.Administration.Nicknames;

[Group]
public partial class NicknameCommands : NadekoModule
{
    [Cmd]
    [UserPerm(GuildPerm.ManageNicknames)]
    [BotPerm(GuildPerm.ChangeNickname)]
    [Priority(0)]
    public async Task SetNick([Leftover] string newNick = null)
    {
        if (string.IsNullOrWhiteSpace(newNick))
            return;
        var curUser = await ctx.Guild.GetCurrentUserAsync();
        await curUser.ModifyAsync(u => u.Nickname = newNick);

        await Response().Confirm(strs.bot_nick(Format.Bold(newNick) ?? "-")).SendAsync();
    }

    [Cmd]
    [UserPerm(GuildPerm.ManageNicknames)]
    [BotPerm(GuildPerm.ManageNicknames)]
    [Priority(1)]
    public async Task SetUserNick(IGuildUser gu, [Leftover] string newNick = null)
    {
        var sg = (SocketGuild)ctx.Guild;
        if (sg.OwnerId == gu.Id
            || gu.GetRoles().Max(r => r.Position) >= sg.CurrentUser.GetRoles().Max(r => r.Position))
        {
            await Response().Error(strs.insuf_perms_i).SendAsync();
            return;
        }

        await gu.ModifyAsync(u => u.Nickname = newNick);

        await Response()
              .Confirm(strs.user_nick(Format.Bold(gu.ToString()), Format.Bold(newNick) ?? "-"))
              .SendAsync();
    }

    [Cmd]
    [RequireContext(ContextType.Guild)]
    [UserPerm(GuildPerm.ManageNicknames)]
    [BotPerm(GuildPerm.ManageNicknames)]
    public async Task RemoveNick([Leftover] IGuildUser user)
    {
        await user.ModifyAsync(x => x.Nickname = null);
        await Response().Confirm(strs.nick_removed(user.Mention)).SendAsync();
    }
}
