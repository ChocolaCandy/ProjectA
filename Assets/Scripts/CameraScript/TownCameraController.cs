using Cinemachine;
using System;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using static Cinemachine.CinemachineCollider;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[RequireComponent(typeof(CinemachineCollider))]
public class TownCameraController : MonoBehaviour
{
    #region SerializeFields
    //Costomizeable setting values
    [Header("Component")]
    [Tooltip("Transform to focus")]
    [SerializeField] private Transform _focusObject;
    [Tooltip("Cinemachine VirtualCamera")]
    [SerializeField] private CinemachineVirtualCamera _camera;
    [Tooltip("Cinemachine FramingTransposer Component")]
    [SerializeField] private CinemachineFramingTransposer _cameraFollowSetting;
    [Tooltip("Cinemachine POV Component")]
    [SerializeField] private CinemachinePOV _cameraLookAtSetting;
    [Tooltip("Cinemachine Collider Component")]
    [SerializeField] private CinemachineCollider _cameraCollider;

    [Header("Mouse setting")]
    [Range(0.5f, 1.5f)]
    [Tooltip("Sensitivity of mouse")]
    [SerializeField] private float _mouseSensitivity = 1.0f;

    [Header("Scroll setting")]
    [Range(1f, 10f)]
    [Tooltip("Sensitivity of scroll")]
    [SerializeField] private float _scrollSensitivity = 5.0f;

    [Header("Collider setting")]
    [SerializeField] private LayerMask _collideAgainst;
    [SerializeField] private LayerMask _transparentLayers;

    [Header("Camera value")]
    [Tooltip("Value of camera distance")]
    [SerializeField] private float _cameraDistance = 5.0f;
    [Tooltip("Value of camera fieldOfView")]
    [SerializeField] private float _cameraFOV = 60.0f;
    #endregion

    #region Non-SerializeFields
    //Value of VirtualCamera clipPlane
    private (float, float) _clipPlane = (0.1f, 500f);

    //Value of FramingTransposer DeadZone
    private float DeadZoneWidth = 0.0f;
    private float DeadZoneHeight = 0.0f;
    private float SoftDeadZoneWidth = 0.8f;
    private float SoftDeadZoneHeight = 0.8f;

    //Value of CinemachinePOV horizontal axis
    private float _minMouseValueX = -180.0f;
    private float _maxMouseValueX = 180.0f;
    private float _sensitivityMouseX = 0.5f;
    private (float, float) _accelDecelX = (0.8f, 0.25f);

    //Value of CinemachinePOV vertical axis
    private float _minMouseValueY = -70.0f;
    private float _maxMouseValueY = 90.0f;
    private float _sensitivityMouseY = 0.3f;
    private (float, float) _accelDecelY = (0.8f, 0.05f);

    //Value of CinemachineCollider
    private string _ignoreTag = "Player";
    private ResolutionStrategy _strategy = ResolutionStrategy.PullCameraForward;
    private float _smoothingTime = 0.0f;
    private float _damping = 0.0f;
    private float _dampingWhenOccluded = 0.0f;

    //Initialize Value of cameraPosition
    private float _initDistance = 1.0f;
    private (float, float) _initPosition = (0, 0);

    //Value of Min/Max camera distance
    private float _minDistance = 1.0f;
    private float _maxDistance = 10.0f;

    #endregion

    #region Methods
    /// <summary>
    /// Player오브젝트의 FocusPoint오브젝트 Transform 반환 메서드
    /// </summary>
    /// <returns>Transform or Null</returns>
    private Transform GetFocusObject()
    {
        GameObject player = GameObject.FindWithTag(TagName.Player);
        if (!player)
            return null;
        Transform focusPoint = player.transform.Find(UtilityName.FocusPoint);
        if (!focusPoint)
        {
            focusPoint = CreateFocusPoint(player);
        }
        return focusPoint;
    }

    /// <summary>
    /// FocusPoint오브젝트 생성 메서드
    /// </summary>
    /// <returns>Transform or Null</returns>
    private Transform CreateFocusPoint(GameObject player)
    {
        CapsuleCollider collider;
        Transform focusPoint = new GameObject(UtilityName.FocusPoint).transform;
        focusPoint.SetParent(player.transform);
        focusPoint.localRotation = Quaternion.identity;
        if (player.TryGetComponent<CapsuleCollider>(out collider))
            focusPoint.localPosition = Vector3.up * collider.height;
        else
            focusPoint.localPosition = Vector3.zero;
        return focusPoint;
    }

    /// <summary>
    /// 카메라 초기화 로직 메서드
    /// </summary>
    private void InitCameraSetting()
    {
        CameraTargetSetting();
        LensSetting();
        FollowingSetting();
        LookAtSetting();
        ColliderSetting();  
    }

    /// <summary>
    /// Follow/LookAt 타깃 세팅 메서드 
    /// </summary>
    private void CameraTargetSetting()
    {
        _camera.Follow = _focusObject;
        _camera.LookAt = _focusObject;
    }

    /// <summary>
    /// Lens필드 세팅 메서드 
    /// </summary>
    private void LensSetting()
    {
        _camera.m_Lens.FieldOfView = _cameraFOV;
        _camera.m_Lens.NearClipPlane = _clipPlane.Item1;
        _camera.m_Lens.FarClipPlane = _clipPlane.Item2;
    }

    /// <summary>
    /// Body필드(FramingTransposer) 세팅 메서드
    /// </summary>
    private void FollowingSetting()
    {
        _cameraFollowSetting.m_CameraDistance = _initDistance;
        _cameraFollowSetting.m_DeadZoneWidth = DeadZoneWidth;
        _cameraFollowSetting.m_DeadZoneHeight = DeadZoneHeight;
        _cameraFollowSetting.m_SoftZoneWidth = SoftDeadZoneWidth;
        _cameraFollowSetting.m_SoftZoneHeight = SoftDeadZoneHeight;
    }

    /// <summary>
    /// Aim필드(POV) 세팅 메서드
    /// </summary>
    private void LookAtSetting()
    {
        CameraAxisX();
        CameraAxisY();
    }

    /// <summary>
    /// HorizontalAxis(X축) 세팅 메서드
    /// </summary>
    private void CameraAxisX()
    {
        _cameraLookAtSetting.m_HorizontalAxis.Value = _initPosition.Item1;
        _cameraLookAtSetting.m_HorizontalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        _cameraLookAtSetting.m_HorizontalAxis.m_MinValue = _minMouseValueX;
        _cameraLookAtSetting.m_HorizontalAxis.m_MaxValue = _maxMouseValueX;
        _cameraLookAtSetting.m_HorizontalAxis.m_MaxSpeed = _sensitivityMouseX * _mouseSensitivity;
        _cameraLookAtSetting.m_HorizontalAxis.m_AccelTime = _accelDecelX.Item1;
        _cameraLookAtSetting.m_HorizontalAxis.m_DecelTime = _accelDecelX.Item2;
    }

    /// <summary>
    /// VerticalAxi(Y축) 세팅 메서드
    /// </summary>
    private void CameraAxisY()
    {
        _cameraLookAtSetting.m_VerticalAxis.Value = _initPosition.Item2;
        _cameraLookAtSetting.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        _cameraLookAtSetting.m_VerticalAxis.m_MinValue = _minMouseValueY;
        _cameraLookAtSetting.m_VerticalAxis.m_MaxValue = _maxMouseValueY;
        _cameraLookAtSetting.m_VerticalAxis.m_MaxSpeed = _sensitivityMouseY * _mouseSensitivity;
        _cameraLookAtSetting.m_VerticalAxis.m_AccelTime = _accelDecelY.Item1;
        _cameraLookAtSetting.m_VerticalAxis.m_DecelTime = _accelDecelY.Item2;
    }

    /// <summary>
    /// Cinemachine Collider 세팅 메서드
    /// </summary>
    private void ColliderSetting()
    {
        _cameraCollider.m_CollideAgainst = _collideAgainst;
        _cameraCollider.m_IgnoreTag = _ignoreTag;
        _cameraCollider.m_TransparentLayers = _transparentLayers;
        _cameraCollider.m_Strategy = _strategy;
        _cameraCollider.m_SmoothingTime = _smoothingTime;
        _cameraCollider.m_Damping = _damping;
        _cameraCollider.m_DampingWhenOccluded = _dampingWhenOccluded;
    }

    /// <summary>
    /// 줌인/줌아웃 메서드
    /// </summary>
    private void ZoomInOut()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput == 0.0f)
            return;
        //if (CheckCollision())
        //{
        //    _cameraDistance = (_focusObject.transform.position - Camera.main.transform.position).magnitude;
        //}
        _cameraDistance = Mathf.Clamp(_cameraDistance + -(wheelInput * _scrollSensitivity), _minDistance, _maxDistance);
    }

    private bool CheckCollision()
    {
        if (_camera.transform.position != Camera.main.transform.position)
            return true;
        return false;
    }

    /// <summary>
    /// 줌 메서드
    /// </summary>
    private void Zoom()
    {
        //if (CheckCollision())
        //{
        //    _cameraFollowSetting.m_CameraDistance = (_focusObject.transform.position - Camera.main.transform.position).magnitude - 0.1f;
        //    return;
        //}
        if (Mathf.Round(_cameraFollowSetting.m_CameraDistance * 10) / 10 == Mathf.Round(_cameraDistance * 10) / 10)
            return;
        _cameraFollowSetting.m_CameraDistance = Mathf.Lerp(_cameraFollowSetting.m_CameraDistance, _cameraDistance, Time.deltaTime);
    }
    #endregion

    private void Awake()
    {
        _camera = gameObject.GetComponent<CinemachineVirtualCamera>();
        _cameraFollowSetting = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _cameraLookAtSetting = _camera.GetCinemachineComponent<CinemachinePOV>();
        _cameraCollider = GetComponent<CinemachineCollider>();
    }

    private void Start()
    {
        _focusObject = GetFocusObject();
        if (!_focusObject)
        {
            //Exception Handling
            return;
        }
        InitCameraSetting();
    }

    private void LateUpdate()
    {
        ZoomInOut();
        Zoom();
    }
}
