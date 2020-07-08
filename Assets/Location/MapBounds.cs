using UnityEngine;

namespace Assets.Location
{
    public class MapBounds
    {
        public static float UI_HALF_WIDTH_IN_PIXELS = 360.0f;
        public static float UI_HALF_HEIGHT_IN_PIXELS = 640.0f;

        private readonly double north;
        private readonly double east;
        private readonly double south;
        private readonly double west;
        private readonly double horizontalBoundDifference;
        private readonly double verticalBoundDifference;

        public MapBounds(float playerLongitude, float playerLatitude)
        {
            MapCalculator mapCalculator = new MapCalculator();

            horizontalBoundDifference = mapCalculator.CalculateHorizontalBoundDistance();
            verticalBoundDifference = mapCalculator.CalculateVerticalBoundDistance(playerLatitude);

            north = playerLatitude + horizontalBoundDifference;
            east = playerLongitude + verticalBoundDifference;
            south = playerLatitude - horizontalBoundDifference;
            west = playerLongitude - verticalBoundDifference;
        }

        public bool IsInBounds(ResourceLocation resourceLocation)
        {
            return resourceLocation.Latitude >= south && resourceLocation.Latitude <= north && resourceLocation.Longitude >= west && resourceLocation.Longitude <= east;
        }

        public Vector2 CalculateResourcePosition(ResourceLocation resourceLocation)
        {
            return new Vector2((float)(720.0f - 720.0f * (east - resourceLocation.Longitude) / (verticalBoundDifference * 2)), (float)(1280.0f - 1280.0f * (north - resourceLocation.Latitude) / (horizontalBoundDifference * 2)));
        }
    }
}