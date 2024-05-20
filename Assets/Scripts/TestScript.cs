
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Rigidbody body;
    private void Start()
    {
      body = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(transform.forward * 2.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(-transform.right * 2.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(-transform.forward * 2.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * 2.0f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            body.AddForce(transform.up * 5.0f, ForceMode.VelocityChange);
        }
    }


}
