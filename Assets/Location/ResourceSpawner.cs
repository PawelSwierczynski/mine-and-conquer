using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Location
{
    public class ResourceSpawner : MonoBehaviour
    {
        public Canvas ui;
        public Button[] resources;

        private void ClearResources()
        {
            foreach (Transform element in ui.transform)
            {
                if (element.tag == "Resource")
                {
                    DestroyImmediate(element.gameObject);
                }
            }
        }

        private bool IsResourceInstantiated(int identifier)
        {
            foreach (Transform element in ui.transform)
            {
                if (element.name == identifier.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private void SpawnResource(ResourceSpawnParameters resourceSpawnParameters)
        {
            if (!IsResourceInstantiated(resourceSpawnParameters.Identifier))
            {
                Instantiate(resources[resourceSpawnParameters.Type], new Vector3(resourceSpawnParameters.MapCoordinates.x, resourceSpawnParameters.MapCoordinates.y), ui.transform.rotation, ui.transform).name = resourceSpawnParameters.Identifier.ToString();
            }
        }

        public void SpawnResources(IEnumerable<ResourceSpawnParameters> resourceSpawnParameters)
        {
            ClearResources();

            foreach (var resource in resourceSpawnParameters)
            {
                SpawnResource(resource);
            }
        }
    }
}