using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent (typeof (Camera))]
public class MainCameraController : MonoBehaviour
{
    
    public Vector3 FocusPoint = new Vector3(0.0f, 2.0f, -5.0f);
    public float MaxFOV = 60.0f;
    public float MixFOV = 30.0f;
    public float MaxFocusZ = -7.0f;
    public float ScrollSpeed = 5.0f;
    public bool Enlarge = false;


    private float MaxAngle = 0.0f; //Todo
    private float minAngle = -1.0f;

    public float SmoothTime = 0.05f;

    [SerializeField]
    [Tooltip("Transform to focus")]
    private Transform _focusObject = null;

    [SerializeField]
    [Tooltip("Main Camera Component")]
    private Camera _camera = null;

    float currentTime = 0.0f;
    private void Start()
    {
        _camera = gameObject.GetOrAddComponent<Camera>();
        _focusObject = GetFocusObject();
        if (_focusObject)
        {
            return;
            //Todo
        }
        gameObject.transform.position = _focusObject.transform.position + FocusPoint;
    }

    void Scroll()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        float calculateY = FocusPoint.y + wheelInput * ScrollSpeed;
        float calculateZ = FocusPoint.z - wheelInput * ScrollSpeed;

        if (calculateY <= minAngle)
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
                _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _camera.fieldOfView -ScrollSpeed, Time.deltaTime);
                }
            if (wheelInput > 0)
                _camera.fieldOfView += ScrollSpeed;
            if (_camera.fieldOfView >= MaxFOV)
                Enlarge = false;
        }
    }
    Vector3 a = Vector3.zero;
    private void LateUpdate()
    {
        //이동
        //gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, _focusObject.transform.position + FocusPoint, ref a, SmoothTime);
        
        //확대
        Scroll();

        //회전
        float a = Input.GetAxis("Mouse X");
        transform.RotateAround(_focusObject.transform.position, Vector3.up, a * 500 * Time.deltaTime);
        gameObject.transform.LookAt(_focusObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            SmoothTime += 0.01f;
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SmoothTime -= 0.01f;
        }
        Debug.Log(Input.GetAxis("Mouse X"));
    }

    /// <summary>
    /// Player오브젝트의 FocusPoint자식 오브젝트를 검색하고 생성 및 Transform 반환 메서드
    /// </summary>
    /// <returns>Transform/Null</returns>
    Transform GetFocusObject()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (!player)
            return null;
        Transform focusPoint = player.transform.Find("FocusPoint");
        if (!focusPoint)
        {
            CapsuleCollider collider;
            if (player.TryGetComponent<CapsuleCollider>(out collider))
            {
                focusPoint = new GameObject("FocusPoint").transform;
                focusPoint.SetParent(player.transform);
                focusPoint.position = new Vector3 (collider.bounds.center.x, collider.bounds.max.y, collider.bounds.center.z);
            }
        }
        return focusPoint;
    }

    //private void LateUpdate()
    //{
    //    if (!_focusObject)
    //    {
    //        _focusObject = GameObject.FindWithTag("Player");
    //        return;
    //    }
    //    Scroll();
    //    Vector3 target = new Vector3(
    //         _focusObject.transform.position.x,
    //         _focusObject.transform.position.y + 0.75f,
    //         _focusObject.transform.position.z);
    //    gameObject.transform.position = _focusObject.transform.position +  FocusPoint;
    //    gameObject.transform.LookAt(target);
    //}
}
