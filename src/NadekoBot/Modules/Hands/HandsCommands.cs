public partial class Utility
{
    public sealed class HandsCommands : NadekoModule
    {
        [Cmd]
        public async Task Hands(int page = 1)
        {
            if (--page < 0)
                return;

            var user = ctx.User as SocketGuildUser;

            if (user is null)
                return;

            if (user.VoiceChannel is not SocketStageChannel stageChannel)
            {
                await Response().Error(strs.not_in_voice).SendAsync();
                return;
            }

            await Response()
                .Paginated()
                .PageItems((page) => Task.FromResult<IReadOnlyCollection<SocketGuildUser>>(stageChannel.ConnectedUsers
                    .Where(x => x.RequestToSpeakTimestamp is not null).Take(10).Skip(page * 10).ToList().AsReadOnly()))
                .PageSize(10)
                .AddFooter(false)
                .Page((requestedUsers, _) =>
                {
                    var embed = CreateEmbed()
                        .WithTitle(GetText(strs.raised_hands))
                        .WithOkColor();

                    if (requestedUsers.Count == 0)
                    {
                        embed.WithDescription(GetText(strs.empty_page));
                        return embed;
                    }

                    for (var i = 0; i < requestedUsers.Count; i++)
                    {
                        var ru = requestedUsers[i];
                        var ts = ru.RequestToSpeakTimestamp;
                        if (ts is not DateTimeOffset dto)
                            continue;

                        embed.AddField($"#{i + 1} {ru.Username}",
                            TimestampTag.FromDateTimeOffset(dto, TimestampTagStyles.Relative));
                    }

                    return embed;
                })
                .SendAsync();
        }
    }
}