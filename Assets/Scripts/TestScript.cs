
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start()
    {
      
    }
    float targetRotation = 10f;
    float velocity;
    float smooth = 0.3f;
    private void Update()
    {
        transform.eulerAngles = Vector3.forward * Mathf.SmoothDampAngle(transform.eulerAngles.z, targetRotation,
                                    ref velocity,smooth);
    }
}
