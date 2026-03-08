using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pegasus.Database.Model
{
    [Table("dungeon")]
    public partial class Dungeon
    {
        public Dungeon()
        {
            DungeonTile = new HashSet<DungeonTile>();
        }

        [Column("landblockid")]
        public short LandBlockId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<DungeonTile> DungeonTile { get; set; }
    }
}
