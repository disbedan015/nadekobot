namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

public class WaifuPossession
{
    public int Id { get; set; }

    public int WaifuInfoId { get; set; }
    public WaifuInfo? WaifuInfo { get; set; }

    public int WaifuItemId { get; set; }
    public WaifuItem? WaifuItem { get; set; }

    public int Quantity { get; set; }
}