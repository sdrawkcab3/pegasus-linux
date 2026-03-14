using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pegasus.Database.Model
{
    [Table("dungeon_tile")]
    public partial class DungeonTile
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("landblockid")]
        public int LandBlockId { get; set; }
        [Column("tileid")]
        public int TileId { get; set; }
        [Column("x")]
        public float X { get; set; }
        [Column("y")]
        public float Y { get; set; }
        [Column("z")]
        public float Z { get; set; }

        public virtual Dungeon LandBlock { get; set; }
    }
}
