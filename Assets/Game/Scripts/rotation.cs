using UnityEngine;

public class rotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Скорость вращения

    private void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed * Time.fixedDeltaTime, 0);
    }
}
