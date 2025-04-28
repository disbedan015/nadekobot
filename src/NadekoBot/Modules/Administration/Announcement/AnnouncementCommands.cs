#nullable disable
using NadekoBot.Common.Attributes;
using NadekoBot.Modules.Administration.Services;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace NadekoBot.Modules.Administration.Announcement;

[Group]
public partial class AnnouncementCommands(AutoPublishService autoPubService) : NadekoModule
{
    [Cmd]
    [UserPerm(ChannelPerm.ManageMessages)]
    public async Task AutoPublish()
    {
        if (ctx.Channel.GetChannelType() != ChannelType.News)
        {
            await Response().Error(strs.req_announcement_channel).SendAsync();
            return;
        }

        var result = await autoPubService.ToggleAutoPublish(ctx.Guild.Id, ctx.Channel.Id);

        if (result)
        {
            await Response().Confirm(strs.autopublish_enable).SendAsync();
        }
        else
        {
            await Response().Confirm(strs.autopublish_disable).SendAsync();
        }
    }
}
