using LinqToDB.Mapping;

namespace NadekoBot.Modules.Waifus.Waifus_Hubbies.db;

public sealed class WaifuHubby
{
    [PrimaryKey]
    public Guid Id { get; set; }
    
    public ulong UserId { get; set; }
    
    public Guid? ClaimedBy { get; set; }
    
    public WaifuHubbyType Type { get; set; }
    
    public ICollection<WaifuHubby> Claims { get; set; }
    public ICollection<WHEquipmentSlot> Equipment { get; set; }
    public ICollection<WHOwnedGift> Gifts { get; set; }
    
    public int Affection { get; set; }
    public int Companionship { get; set; }
    public int Motivation { get; set; }
}

public enum WaifuHubbyType
{
    Waifu,
    Hubby
}

public enum WHEquipmentSlotType
{
    Head,
    Hair,
    Ears,
    Eyes,
    Face,
    Torso,
}

public sealed class WHEquipmentSlot
{
    public Guid WaifuHubbyId { get; set; }
    public WHEquipmentSlotType Slot { get; set; }
    public Guid ItemId { get; set; }
}

public sealed class WHEquipmentItem
{
    [PrimaryKey]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string ImageUrl { get; set; }
    
    // todo add stats
}

public sealed class WHOwnedGift
{
    public Guid OwnerId { get; set; }
    public Guid GiftId { get; set; }
    
    public long Count { get; set; }
}

public sealed class WHGift
{
    [PrimaryKey]
    public Guid Id { get; set; }
    
    public string Emoji { get; set; }
    
    public string Name { get; set; }
    
    public long Price { get; set; }
}

public sealed class WHStats
{
    
}