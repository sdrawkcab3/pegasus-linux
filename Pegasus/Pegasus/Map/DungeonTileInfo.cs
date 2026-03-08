using System.Numerics;
using Pegasus.Database.Model;

namespace Pegasus.Map
{
    public class DungeonTileInfo
    {
        public int Id { get; }
        public short LandBlockId { get; }
        public short TileId { get; }
        public Vector3 Origin { get; }

        public DungeonTileInfo(DungeonTile dungeonTile)
        {
            Id          = dungeonTile.Id;
            LandBlockId = dungeonTile.LandBlockId;
            TileId      = dungeonTile.TileId;
            Origin      = new Vector3(dungeonTile.X, dungeonTile.Y, dungeonTile.Z);
        }
    }
}
