using UnityEngine;

public class PulsingPortal : MonoBehaviour
{
    public Material portalMaterial;  // The material that you will animate
    public float pulseSpeed = 0.7f; // Speed of the pulse animation
    public float pulseIntensity = 1.5f; // Maximum brightness of the pulse
    
    void Update()
    {
        // Animate the emission intensity using Mathf.PingPong, which gives a smooth back-and-forth motion
        float emission = Mathf.PingPong(Time.time * pulseSpeed, pulseIntensity);
        
        // Set the emission color dynamically
        portalMaterial.SetColor("_EmissionColor", new Color(emission, emission, emission));
    }
}
