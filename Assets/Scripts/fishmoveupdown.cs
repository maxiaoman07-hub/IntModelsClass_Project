using UnityEngine;

public class FishSpinAndDive : MonoBehaviour
{
    [Header("Spin Settings")]
    public float spinSpeed = 90f;        // degrees per second
    public float spinDuration = 2f;      // how long the fish spins in place

    [Header("Dive Settings")]
    public float diveAmplitude = 1.5f;   // how far up/down
    public float diveSpeed = 1f;         // how fast up/down motion
    public float forwardSpeed = 2f;      // optional: forward swimming during dive

    private float timer;
    private float startY;
    private bool spinning = true;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (spinning)
        {
            HandleSpin();

            if (timer >= spinDuration)
            {
                spinning = false;
                timer = 0f;
            }
        }
        else
        {
            HandleDive();
        }
    }

    void HandleSpin()
    {
        // Rotate around Y axis in place
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    void HandleDive()
    {
        // Smooth sine-wave dive up/down
        float newY = startY + Mathf.Sin(Time.time * diveSpeed) * diveAmplitude;

        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;

        // Optional forward swimming
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;

        // After one full dive cycle, return to spinning
        if (timer >= Mathf.PI * 2f / diveSpeed)
        {
            spinning = true;
            timer = 0f;
        }
    }
}
