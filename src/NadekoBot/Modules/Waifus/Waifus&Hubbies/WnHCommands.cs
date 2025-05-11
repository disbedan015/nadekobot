namespace NadekoBot.Modules.Waifus.Waifus_Hubbies;

public class WnHCommands : NadekoModule
{
    [Cmd]
    public async Task Waifu([Leftover] IUser? user = null)
    {
        user ??= ctx.User;
    }

    [Cmd]
    public async Task Hug([Leftover] IUser user)
    {
    }

    [Cmd]
    public async Task Pat([Leftover] IUser user)
    {
        
    }

    [Cmd]
    public async Task Kiss([Leftover] IUser user)
    {

    }
}