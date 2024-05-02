using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Camera))]
public class MainCameraController : MonoBehaviour
{
    //TODO 회전과 캐릭터 후방 배치 기능 필요
    public Vector3 FocusPoint = new Vector3(0.0f, 2.0f, -5.0f);
    public float MaxFOV = 60.0f;
    public float MixFOV = 30.0f;
    public float MaxFocusZ = -7.0f;
    public float ScrollSpeed = 5.0f;
    public bool Enlarge = false;
    [SerializeField]
    private GameObject _focusObject = null;
    [SerializeField]
    private Camera _camera = null;
    private void Start()
    {
        _focusObject = GameObject.FindWithTag("Player");
        _camera = gameObject.GetOrAddComponent<Camera>();
    }

    void Scroll()
    {
        if (!_camera)
        {
            _camera = gameObject.GetOrAddComponent<Camera>();
            return;
        }

        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        float calculateY = FocusPoint.y + wheelInput * ScrollSpeed;
        float calculateZ = FocusPoint.z - wheelInput * ScrollSpeed;

        if (calculateY <= 0.5f)
            Enlarge = true;

        if (!Enlarge)
        {
            if (calculateZ < MaxFocusZ)
                return;
            FocusPoint.y = calculateY;
            FocusPoint.z = calculateZ;
        }
        else
        {
           
            if (wheelInput < 0) {
                if (_camera.fieldOfView <= MixFOV)
                    return;
                _camera.fieldOfView -= ScrollSpeed;
                }
            if (wheelInput > 0)
                _camera.fieldOfView += ScrollSpeed;
            if (_camera.fieldOfView >= MaxFOV)
                Enlarge = false;
        }
    }

    private void LateUpdate()
    {
        if (!_focusObject)
        {
            _focusObject = GameObject.FindWithTag("Player");
            return;
        }
        Scroll();
        Vector3 target = new Vector3(
             _focusObject.transform.position.x,
             _focusObject.transform.position.y + 0.75f,
             _focusObject.transform.position.z);
        gameObject.transform.position = _focusObject.transform.position +  FocusPoint;
        gameObject.transform.LookAt(target);
    }
}
