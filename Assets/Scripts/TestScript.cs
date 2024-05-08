using System;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject a;
    public Vector3 _endPointPosition;
    float ads = 0.0f;

    float min = -89.0f;
    float max = 89.0f;
    public float value = 0.0f;
    public float _distance = 5.0f;
    private void Start()
    {
        _endPointPosition = (transform.position - a.transform.position).normalized * _distance;
        value = 0.0f;
        transform.LookAt(a.transform);
    }
    public float RotationSmoothTime = 0.12f;
    public bool isChanged = false;
    //private void LateUpdate()
    //{
    //   /* cc();
    //        transform.position = a.transform.position + _endPointPosition;
    //        //LookAtPlayer();
    //    float ats = Mathf.Atan2(transform.position.y - a.transform.position.y, (transform.position - a.transform.position).magnitude) * Mathf.Rad2Deg;
    //    Debug.Log($"after :" + ats);*/
    //}

    public void LookAtPlayer()
    {
        Quaternion ad = Quaternion.LookRotation((a.transform.position - transform.position).normalized);
        transform.rotation = ad;
        //transform.LookAt(a.transform);
    }

    //public void cc()
    //{
    //    aa();
    //    dd();
    //    Vector3 CheckV = a.transform.position + _endPointPosition;
    //    float ats = Mathf.Atan2(CheckV.y - a.transform.position.y, (CheckV - a.transform.position).magnitude) * Mathf.Rad2Deg;
    //    Debug.Log($"before :" + ats);
    //}

    //public void aa()
    //{
    //    float xMouseInput = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
    //    if (xMouseInput == 0)
    //        return;
    //    Quaternion quaternion = Quaternion.AngleAxis(xMouseInput, a.transform.up);
    //    Vector3 vector2 = quaternion * _endPointPosition;
    //    _endPointPosition = vector2;
    //}

    //public void dd()
    //{
    //    float yMouseInput = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;
    //    if (yMouseInput == 0)
    //        return;
    //    Quaternion quaternion1 = Quaternion.AngleAxis(-yMouseInput, transform.right);
    //    Vector3 afterY = quaternion1 * _endPointPosition;
    //    value += -yMouseInput;
    //    _endPointPosition = afterY;
    //}

    private void LateUpdate()
    {
        newaa();
    }

    public void newaa()
    {
        float xMouseInput = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        if (xMouseInput == 0)
            return;
        Quaternion quaternion = Quaternion.AngleAxis(xMouseInput, a.transform.up);
        Vector3 beforeVector = transform.position;
        Vector3 afterVerctor = quaternion * beforeVector;
        //float ats = Mathf.Atan2(transform.position.x - a.transform.position.y, (transform.position - a.transform.position).magnitude) * Mathf.Rad2Deg;
        Vector3 vector2 = quaternion * _endPointPosition;
        _endPointPosition = vector2;
    }

    public void dd()
    {
        float yMouseInput = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;
        if (yMouseInput == 0)
            return;
        Quaternion quaternion1 = Quaternion.AngleAxis(-yMouseInput, transform.right);
        Vector3 afterY = quaternion1 * _endPointPosition;
        value += -yMouseInput;
        _endPointPosition = afterY;
    }
}
