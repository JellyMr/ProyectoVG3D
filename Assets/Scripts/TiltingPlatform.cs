using UnityEngine;

public class TiltingPlatform : MonoBehaviour
{
    public float tiltSpeed = 2f;
    public float maxAngle = 15f;

    private float currentAngle = 0f;
    private bool tiltingForward = true;

    void Update()
    {
        if (tiltingForward)
        {
            currentAngle += tiltSpeed * Time.deltaTime;
            if (currentAngle >= maxAngle)
                tiltingForward = false;
        }
        else
        {
            currentAngle -= tiltSpeed * Time.deltaTime;
            if (currentAngle <= -maxAngle)
                tiltingForward = true;
        }

        transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
    }
}
