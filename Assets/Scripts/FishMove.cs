using UnityEngine;

public class StableFishMovement : MonoBehaviour
{
    [Header("Movement")]
    public float swimSpeed = 2f;
    public float turnSpeed = 2f;
    public float wanderRadius = 5f;
    public float wanderInterval = 3f;

    [Header("Wiggle Motion")]
    public Transform body;              // assign the fish mesh here
    public float wiggleAmount = 10f;
    public float wiggleSpeed = 4f;

    private Vector3 targetPos;
    private float timer;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        Swim();
        Wiggle();
        Wander();
    }

    void Swim()
    {
        // direction to target
        Vector3 dir = (targetPos - transform.position);

        // keep horizontal mostly
        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            // rotate fish toward target
            Quaternion lookRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, turnSpeed * Time.deltaTime);
        }

        // actual forward movement
        transform.position += transform.forward * swimSpeed * Time.deltaTime;
    }

    void Wiggle()
    {
        if (body == null) return;

        float wiggle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;

        // apply wiggle ONLY to the mesh, not the whole fish object
        body.localRotation = Quaternion.Euler(0, wiggle, 0);
    }

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderInterval)
        {
            PickNewTarget();
            timer = 0f;
        }
    }

    void PickNewTarget()
    {
        Vector3 randomOffset = Random.insideUnitSphere * wanderRadius;
        randomOffset.y *= 0.3f; // fish stay somewhat level

        targetPos = transform.position + randomOffset;
    }
}
