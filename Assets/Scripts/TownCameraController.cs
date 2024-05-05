using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using System;
using UnityEngine.UIElements;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

[RequireComponent (typeof (Camera))]
public class MainCameraController : MonoBehaviour
{
    [Header("Setting value")]
    public float ScrollSensitivity = 10.0f;
    //private
    private Vector3 _endPoint = Vector3.zero;
    private float _radius = 0.0f;

    [Header("Default value")]
    [SerializeField]
    [Tooltip("Init distance between player and camera")]
    private float _initDistance = 2.0f;
    [SerializeField]
    [Tooltip("Default distance between player and camera")]
    private float _defaultDistance = 5.0f;

    [Header("Object")]
    [SerializeField]
    [Tooltip("Transform to focus")]
    private Transform _focusObject = null;
    [SerializeField]
    [Tooltip("Main Camera Component")]
    private Camera _camera = null;

    private void Start()
    {
        _camera = gameObject.GetOrAddComponent<Camera>();
        _focusObject = GetFocusObject(); ;
        if (!_focusObject)
        {
            return;
            //Todo
        }
        InitCameraPosition();
    }

    private void LateUpdate()
    {
        Scroll();

        //회전
        //RotateAround();

        //이동

        //카메라 트랜스폼 변경
        transform.position = Vector3.Lerp(transform.position, _focusObject.position + _endPoint, Time.deltaTime);
        transform.LookAt(_focusObject);
        _radius = (transform.position - _focusObject.position).magnitude;
    }

    #region Methods
    /// <summary>
    /// Player오브젝트의 FocusPoint오브젝트 Transform 반환 메서드
    /// </summary>
    /// <returns>Transform or Null</returns>
    Transform GetFocusObject()
    {
        GameObject player = GameObject.FindWithTag(TagName.Player);
        if (!player)
            return null;
        Transform focusPoint = player.transform.Find(UtilityName.FocusPoint);
        if (!focusPoint)
        {
            CapsuleCollider collider;
            if (player.TryGetComponent<CapsuleCollider>(out collider))
            {
                focusPoint = new GameObject(UtilityName.FocusPoint).transform;
                focusPoint.position = new Vector3(collider.bounds.center.x, collider.bounds.max.y, collider.bounds.center.z);
                focusPoint.SetParent(player.transform);
                focusPoint.localRotation = Quaternion.identity;
            }
        }
        return focusPoint;
    }

    /// <summary>
    /// 카메라 초기화 메서드
    /// </summary>
    void InitCameraPosition()
    {
        Vector3 initPosition = _focusObject.position + _focusObject.forward * -(Mathf.Abs(_initDistance));
        transform.position = initPosition;
        Vector3 defaultPoint = _focusObject.forward * -(Mathf.Abs(_defaultDistance));
        _endPoint = defaultPoint;
    }

    /// <summary>
    /// 스크롤휠 확대/축소 메서드
    /// </summary>
    void Scroll()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput == 0.0f)
        {
            return;
        }
        float afterRadius = _radius + -(wheelInput * ScrollSensitivity);
        Vector3 afterPoint = (transform.position - _focusObject.position).normalized * afterRadius;
        _endPoint = afterPoint;
    } 

    #endregion

 
    /*
    public void RotateAround()
    {
        float a = Input.GetAxis("Mouse X");
        Vector3 vector = gameObject.transform.position;
        Quaternion quaternion = Quaternion.AngleAxis(a * 100 * Time.deltaTime + 0.0000001f, Vector3.up);
        Vector3 vector2 = vector - _focusObject.position;
        vector2 = quaternion * vector2;
        FocusPoint = quaternion * FocusPoint;
        //transform.position = Vector3.MoveTowards(transform.position, player.position, -(rotationDistance - Vector3.Distance(transform.position, player.position)));
        if (Mathf.Abs((int)FocusPoint.x) < 3 && Mathf.Abs((int)FocusPoint.z) < 3 )
        {
            Debug.Log("In");
        }
    }*/
    
}
