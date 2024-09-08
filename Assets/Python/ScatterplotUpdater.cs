using UnityEngine;

namespace IATK
{
    [ExecuteInEditMode]
    public class ScatterplotUpdater : MonoBehaviour
    {
        [SerializeField] private ScatterplotVisualisation scatterplotVisualisation; // Reference to the ScatterplotVisualisation

        private void Start()
        {
            if (scatterplotVisualisation != null)
            {
                Debug.LogError("ScatterplotVisualisation reference is present.");
            }
            else
            {
                Debug.LogError("ScatterplotVisualisation reference is missing.");
            }
        }
    }
}

