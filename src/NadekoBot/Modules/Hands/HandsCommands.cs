public partial class Utility
{
    public sealed class HandsCommands : NadekoModule
    {
        [Cmd]
        public async Task Hands()
        {
            var user = ctx.User as SocketGuildUser;

            if (user is null)
                return;

            if (user.VoiceChannel is not { } voiceChannel)
            {
                await Response().Error(strs.not_in_voice).SendAsync();
                return;
            }

            await Response()
                .Paginated()
                .PageItems((page) => Task.FromResult<IReadOnlyCollection<SocketGuildUser>>(voiceChannel.ConnectedUsers.Where(x => x.RequestToSpeakTimestamp is not null).Take(page * 10).ToList().AsReadOnly()))
                .PageSize(10)
                .AddFooter(false)
                .Page((requestedUsers, _) =>
                {
                    var embed = CreateEmbed()
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
                        if (ts is null)
                            continue;
                        embed.AddField($"#{i + 1} {ru.Username}", ts.Value.ToString("g"));
                    }

                    return embed;
                })
                .SendAsync();
        }
    }
}