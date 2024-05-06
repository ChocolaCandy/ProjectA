using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject a;
    [SerializeField] private float lerpTime = 1f;
    private float currentTime;
    float smoothTime = 0.12f;
    int i = 0;
    private float startTime;
    public Camera ace;
    public float after;
    private void Start()
    {
        startTime = Time.time;
    }

    private void LateUpdate()
    {
        if (transform.position == a.transform.position)
        {
            Debug.Log($"{i} : ÀÏÄ¡");
            return;
        }
        float t = Time.deltaTime;
        ScrollUpDown();
        currentTime += Time.deltaTime * smoothTime;
        Debug.Log(currentTime);
        if (currentTime >= lerpTime)
        {
            //t = lerpTime;
        }

        i++;
        float fracComplete = (Time.time - startTime) / 1;
        Debug.Log(fracComplete);
        transform.position = Vector3.Lerp(transform.position, a.transform.position + RotateAround(), fracComplete);
        ace.fieldOfView = Mathf.Lerp(ace.fieldOfView, after, Time.deltaTime);
    }
    public Vector3 RotateAround()
    {

        float xMouseInput = Input.GetAxis("Mouse X") * 500 * Time.deltaTime;
        float yMouseInput = Input.GetAxis("Mouse Y") * 500 * Time.deltaTime;

        Vector3 ad = transform.position - a.transform.position;
        Quaternion quaternion = Quaternion.AngleAxis(xMouseInput, a.transform.up);
        Quaternion quaternion1 = Quaternion.AngleAxis(-yMouseInput, a.transform.right);
        Vector3 vector2 = quaternion * quaternion1 * ad;
        return vector2;
    }
    private void ScrollUpDown()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput == 0.0f)
        {
            return;
        }
        float afterRadius = Mathf.Clamp(ace.fieldOfView + -(wheelInput * 100), 20, 60);
        after = afterRadius;
    }

}
