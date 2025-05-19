using System;
using System.Collections.Generic;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db
{
    /// <summary>
    /// Represents a Waifu or Hubby.
    /// </summary>
    public class WaifuInfo
    {
        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Discord User ID of the user this Waifu/Hubby represents.
        /// </summary>
        public ulong UserId { get; set; }

        /// <summary>
        /// Gets or sets the Discord User ID of the user who owns this Waifu/Hubby.
        /// Null if unclaimed.
        /// </summary>
        public ulong? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the price of the Waifu/Hubby.
        /// </summary>
        public long Price { get; set; }

        /// <summary>
        /// Gets or sets the ascension level (e.g., stars).
        /// </summary>
        public int Ascension { get; set; }

        /// <summary>
        /// Gets or sets the mood stat.
        /// </summary>
        public int Mood { get; set; } = 100;

        /// <summary>
        /// Gets or sets the rest stat.
        /// </summary>
        public int Rest { get; set; } = 100;

        /// <summary>
        /// Gets or sets the food stat.
        /// </summary>
        public int Food { get; set; } = 100;
        
        /// <summary>
        /// Gets or sets whether this entity is a Hubby (true) or Waifu (false).
        /// </summary>
        public bool IsHubby { get; set; }

        /// <summary>
        /// Gets or sets the custom avatar URL for this Waifu/Hubby.
        /// </summary>
        public string? CustomAvatarUrl { get; set; }
        
        /// <summary>
        /// Gets or sets the description/bio for this Waifu/Hubby.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the quote for this Waifu/Hubby.
        /// </summary>
        public string? Quote { get; set; }

        /// <summary>
        /// Gets or sets whether this Waifu/Hubby is currently for sale by their owner.
        /// </summary>
        public bool IsForSale { get; set; } = false;

        /// <summary>
        /// Gets or sets the collection of items equipped by this Waifu/Hubby.
        /// </summary>
        public virtual ICollection<WaifuEquippedItem> EquippedItems { get; set; } = new HashSet<WaifuEquippedItem>();

        /// <summary>
        /// Gets or sets the collection of possessions (gifts) this Waifu/Hubby has.
        /// </summary>
        public virtual ICollection<WaifuPossession> Possessions { get; set; } = new HashSet<WaifuPossession>();
    }
}