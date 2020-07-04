using System;

namespace Assets.Location
{
    public class MapCalculator
    {
        private const double MAP_WIDTH_IN_METERS = 165.443;
        private const double MAP_HEIGHT_IN_METERS = 294.121;
        private const int MAP_WIDTH_IN_PIXELS = 450;
        private const int MAP_HEIGHT_IN_PIXELS = 800;

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

        //1 degree of latitude is equal to 111.32 km
        //1 degree of longitude is equal to about 111.319491 * cos(latitude) km
    }
}