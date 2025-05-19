namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

/// <summary>
/// Represents an item equipped by a Waifu/Hubby in a specific slot.
/// </summary>
public class WaifuEquippedItem
{
    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the WaifuInfo who equipped this item.
    /// </summary>
    public int WaifuInfoId { get; set; }
    /// <summary>
    /// Gets or sets the navigation property to the WaifuInfo.
    /// </summary>
    public virtual WaifuInfo WaifuInfo { get; set; }

    /// <summary>
    /// Gets or sets the ID of the WaifuItem that is equipped.
    /// </summary>
    public int WaifuItemId { get; set; }
    /// <summary>
    /// Gets or sets the navigation property to the equipped WaifuItem.
    /// </summary>
    public virtual WaifuItem WaifuItem { get; set; }

    /// <summary>
    /// Gets or sets the slot this item occupies for the Waifu.
    /// This should be consistent with the WaifuItem's SlotType.
    /// </summary>
    public WaifuItemSlot Slot { get; set; }
}
