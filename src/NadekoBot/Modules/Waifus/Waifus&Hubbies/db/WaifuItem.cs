using System.ComponentModel.DataAnnotations;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

/// <summary>
/// Represents an item that can be gifted to or equipped by a Waifu/Hubby.
/// </summary>
public class WaifuItem
{
    /// <summary>
    /// Gets or sets the primary key for the WaifuItem.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    [MaxLength(100)]
    public string Name { get; set; }
    
    /// <summary>
    /// Store a value that may or may not be used. not shown anywhere.
    /// </summary>
    [MaxLength(100)]
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the price of the item.
    /// </summary>
    public long Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the item.
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the type of the item (e.g., Gift, AppearanceSlotItem).
    /// </summary>
    public WaifuItemType ItemType { get; set; }

    /// <summary>
    /// Gets or sets the appearance slot this item occupies, if applicable.
    /// Defaults to NotApplicable.
    /// </summary>
    public WaifuItemSlot SlotType { get; set; } = WaifuItemSlot.NotApplicable;
}