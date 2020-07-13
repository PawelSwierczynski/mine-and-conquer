using System;

namespace Assets.Location
{
    public class MapCalculator
    {
        private const double MAP_WIDTH_IN_METERS = 165.443;
        private const double MAP_HEIGHT_IN_METERS = 294.121;
        private const int MAP_WIDTH_IN_PIXELS = 450;
        private const int MAP_HEIGHT_IN_PIXELS = 800;
        private const double LATITUDE_DEGREE_LENGTH = 111319.491;
        private const double MAP_HORIZONTAL_BOUND_DISTANCE = 0.001321;
        private const double MAP_VERTICAL_BOUND_DISTANCE_MODIFIER = 82.7215;

        private double CalculateCosinus(double value)
        {
            return Math.Cos(value * Math.PI / 180);
        }

        public string RetrieveMapResolution()
        {
            return MAP_WIDTH_IN_PIXELS.ToString() + "x" + MAP_HEIGHT_IN_PIXELS.ToString();
        }

        public double CalculateMapZoom(float latitude)
        {
            return Math.Log10(212896.0f * CalculateCosinus(latitude)) / 0.30103f;
        }

        public double CalculateHorizontalDistance(float playerLatitude, float resourceLatitude)
        {
            return (resourceLatitude - playerLatitude) * LATITUDE_DEGREE_LENGTH;
        }

        public double CalculateVerticalDistance(float playerLatitude, float playerLongitude, float resourceLongitude)
        {
            return (resourceLongitude - playerLongitude) * LATITUDE_DEGREE_LENGTH * CalculateCosinus(playerLatitude);
        }

        public double CalculateHorizontalBoundDistance()
        {
            return MAP_HORIZONTAL_BOUND_DISTANCE;
        }

        public double CalculateVerticalBoundDistance(float playerLatitude)
        {
            return MAP_VERTICAL_BOUND_DISTANCE_MODIFIER / (LATITUDE_DEGREE_LENGTH * CalculateCosinus(playerLatitude));
        }

        public double RetrieveLatitudeZoneIdentifier(float latitude)
        {
            return Math.Floor(latitude / 0.05 + 1800);
        }

        public double RetrieveLongitudeZoneIdentifier(float longitude)
        {
            return Math.Floor(longitude / 0.05 + 3600);
        }
    }
}