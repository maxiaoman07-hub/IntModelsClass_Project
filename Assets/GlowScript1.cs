using UnityEngine;

[ExecuteAlways]
public class UltimateNeonPulse : MonoBehaviour
{
    [Header("Pulse Settings")]
    [Range(0.1f, 5f)] public float pulseSpeed = 1.5f;
    [ColorUsage(true, true)] public Color pulseColor = new Color(0f, 3f, 1f); // Neon cyan
    [Range(1f, 5f)] public float minBrightness = 0.5f;
    [Range(5f, 20f)] public float maxBrightness = 25f;

    private Material _material;
    private Renderer _renderer;
    private float _pulsePhase;

    void Start() => Initialize();
    void OnEnable() => Initialize();

    void Initialize()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null) return;

        // Use sharedMaterial in edit mode to avoid leaks
        _material = Application.isPlaying ? _renderer.material : _renderer.sharedMaterial;
        if (_material == null) return;

        _material.EnableKeyword("_EMISSION");
        _material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

        _pulsePhase = 0.75f;
        UpdateGlow();
    }

    void Update()
    {
        if (_material == null) return;

        _pulsePhase += Time.deltaTime * pulseSpeed;
        if (_pulsePhase > 1f) _pulsePhase = 0f;

        UpdateGlow();
    }

    void UpdateGlow()
    {
        float t = Mathf.PingPong(_pulsePhase * 2f, 1f);
        t = Mathf.SmoothStep(0f, 1f, t);

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, t);

        _material.color = pulseColor;
        _material.SetColor("_EmissionColor", pulseColor * brightness);

        // Realtime GI update only meaningful in built-in pipeline
        if (Application.isPlaying)
            DynamicGI.SetEmissive(_renderer, pulseColor * brightness);
    }

    void OnDisable()
    {
        // Only destroy runtime-instantiated materials
        if (Application.isPlaying && _material != null)
            Destroy(_material);
    }

#if UNITY_EDITOR
    void OnValidate() => UnityEditor.EditorApplication.delayCall += _OnValidate;
    void _OnValidate()
    {
        if (this == null) return;
        if (_material != null)
        {
            UpdateGlow();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}
