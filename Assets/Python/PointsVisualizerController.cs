using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IATK; // Ensure this matches the namespace where VRVisualisation is defined

public class PointVisualizerController : MonoBehaviour
{
    
    [SerializeField] private VRVisualisation vrVisualisation; // Reference to the VRVisualisation component

    public Button yourButton; // Reference to the button that triggers the color change
    public Color highlightColor = Color.red; // Color for the highlighted points
    public List<GameObject> pointsToHighlight = new List<GameObject>(); // List of points to highlight

    private Material defaultMaterial;
    private Material highlightMaterial;

    void Start()
    {
        if (vrVisualisation == null)
        {
            Debug.LogError("VRVisualisation reference is not assigned.");
            return;
        }

        // Create highlight material
        highlightMaterial = new Material(Shader.Find("Standard"));
        highlightMaterial.color = highlightColor;

        // Find the default material (assuming the points use a single material)
        if (pointsToHighlight.Count > 0)
        {
            Renderer renderer = pointsToHighlight[0].GetComponent<Renderer>();
            if (renderer != null)
            {
                defaultMaterial = renderer.material;
            }
        }

        // Add a listener to the button to call the method when clicked
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(HighlightPoints);
        }
    }

    public void HighlightPoints()
    {
        // Reset all points to default material
        foreach (Renderer renderer in vrVisualisation.GetComponentsInChildren<Renderer>())
        {
            if (renderer != null)
            {
                renderer.material = defaultMaterial;
            }
        }

        // Apply highlight material to the specific points
        foreach (GameObject point in pointsToHighlight)
        {
            if (point != null)
            {
                Renderer renderer = point.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = highlightMaterial;
                }
            }
        }
    }
}
