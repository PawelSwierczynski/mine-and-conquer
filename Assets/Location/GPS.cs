using System;
using System.Collections;
using UnityEngine;

namespace Assets.Location
{
    public class GPS : MonoBehaviour
    {
        private float LOCATION_CHANGE_THRESHOLD = 0.00025f;

        private bool isRunning;
        private bool isLocationChanged;
        public float Longitude { get; private set; }
        public float Latitude { get; private set; }

        public bool IsLocationChanged()
        {
            if (isLocationChanged)
            {
                isLocationChanged = false;

                return true;
            }
            else
            {
                return false;
            }
        }

        void Start()
        {
            isRunning = false;
            isLocationChanged = false;

            Latitude = 52.38f;
            Longitude = 16.88f;

            StartCoroutine(EnableLocationRetrieval());
        }

        void Update()
        {
            if (isRunning)
            {
                UpdateLocationIfNecessary();
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.W)) {
                    Latitude += LOCATION_CHANGE_THRESHOLD;
                    isLocationChanged = true;
                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    Latitude -= LOCATION_CHANGE_THRESHOLD;
                    isLocationChanged = true;
                }

                if (Input.GetKeyUp(KeyCode.A))
                {
                    Longitude -= LOCATION_CHANGE_THRESHOLD;
                    isLocationChanged = true;
                }
                else if (Input.GetKeyUp(KeyCode.D))
                {
                    Longitude += LOCATION_CHANGE_THRESHOLD;
                    isLocationChanged = true;
                }
            }
        }

        private IEnumerator EnableLocationRetrieval()
        {
            if (!Input.location.isEnabledByUser)
            {
                isLocationChanged = true;

                yield break;
            }

            Input.location.Start();

            int timeoutThreshold = 20;

            while (Input.location.status == LocationServiceStatus.Initializing && timeoutThreshold > 0)
            {
                yield return new WaitForSeconds(1);
                timeoutThreshold--;
            }

            if (Input.location.status == LocationServiceStatus.Failed || timeoutThreshold <= 0)
            {
                yield break;
            }

            isRunning = true;
            isLocationChanged = true;
        }

        private void UpdateLocationIfNecessary()
        {
            float currentLongitude = Input.location.lastData.longitude;
            float currentLatitude = Input.location.lastData.latitude;

            if (DoesChangeExceedThreshold(Longitude, currentLongitude) || DoesChangeExceedThreshold(Latitude, currentLatitude))
            {
                Longitude = currentLongitude;
                Latitude = currentLatitude;

                isLocationChanged = true;
            }
        }

        private bool DoesChangeExceedThreshold(float previousCoordinate, float currentCoordinate)
        {
            return Math.Abs(previousCoordinate - currentCoordinate) >= LOCATION_CHANGE_THRESHOLD;
        }
    }
}