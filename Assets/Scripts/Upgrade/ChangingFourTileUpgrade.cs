using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/ChangingFourTile", fileName = "ChangingFourTile")]
    public class ChangingFourTileUpgrade : Upgrade
    {
        public int NumberOfTiles;
        public int TileNumberValue;
        public int Multiple;
    }
}