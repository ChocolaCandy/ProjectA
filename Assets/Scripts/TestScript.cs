
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

    public float RotationSmoothTime = 0.12f;
    public bool isChanged = false;

    public Camera _camera;
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

    bool adds = false;
    Quaternion _endRotation = Quaternion.identity;
    private void Start()
    {
        //Vector3 initPosition = a.transform.position + -(a.transform.forward) * (Mathf.Abs(_distance));
        //transform.position = initPosition;
        Vector3 dir = transform.position - a.transform.position;
        _endPointPosition = dir.normalized;
        _endRotation = Quaternion.LookRotation(-_endPointPosition);
        _cinemachineTargetPitch = _endRotation.eulerAngles.y;
        _cinemachiney = _endRotation.eulerAngles.x;
        value = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void LateUpdate()
    {
        Rotated();
        //newdd();

        transform.position = a.transform.position + _endPointPosition * _distance;
        //if (Mathf.Abs(value) <= 89)
        //    transform.LookAt(a.transform);

    }

    float _cinemachineTargetPitch = 0.0f;
    float _cinemachiney = 0.0f;
    public void Rotated()
    {
        newaa();
        newdd();
        transform.rotation = Quaternion.Euler(_cinemachiney, _cinemachineTargetPitch, 0.0f);
    }

    public void newaa()
    {
        float xMouseInput = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        if (xMouseInput == 0)
            return;
        Quaternion quaternion = Quaternion.AngleAxis(xMouseInput, a.transform.up);
        Vector3 afterVerctor = quaternion * _endPointPosition;
        _endPointPosition = afterVerctor;
        _endRotation = quaternion * _endRotation;
        _cinemachineTargetPitch += xMouseInput;
        adds = true;
    }

    public void newdd()
    {
        float yMouseInput = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;
        yMouseInput = Mathf.Abs(Mathf.Clamp(value - yMouseInput, -90, 90)) == 90 ? 0 : yMouseInput;
        if (yMouseInput == 0)
            return;
        Quaternion quaternion1 = Quaternion.AngleAxis(-yMouseInput, transform.right);
        Vector3 afterY = quaternion1 * _endPointPosition;
        _endPointPosition = afterY; ;
        _endRotation = quaternion1 * _endRotation;
        value -= yMouseInput;
        adds = true;
    }
}
