using UnityEngine;

namespace Assets.Location
{
    public class ResourceSpawnParameters
    {
        public int Identifier { get; }
        public int Type { get; }
        public Vector2 MapCoordinates { get; }

        public ResourceSpawnParameters(int identifier, int type, Vector2 mapCoordinates)
        {
            Identifier = identifier;
            Type = type;
            MapCoordinates = mapCoordinates;
        }
    }
}