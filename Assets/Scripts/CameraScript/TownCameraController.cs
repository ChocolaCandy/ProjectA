using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class MainCameraController : MonoBehaviour
{
    #region SerializeFields
    [Header("Component")]
    [Tooltip("Transform to focus")]
    [SerializeField] private Transform _focusObject;
    [Tooltip("Cinemachine VirtualCamera")]
    [SerializeField] private CinemachineVirtualCamera _camera;
    [Tooltip("Cinemachine FramingTransposer Component")]
    [SerializeField] private CinemachineFramingTransposer _cameraFollowSetting;
    [Tooltip("Cinemachine POV Component")]
    [SerializeField] private CinemachinePOV _cameraLookAtSetting;

    [Header("Mouse setting")]
    [Range(0.5f, 1.5f)]
    [Tooltip("Sensitivity of mouse")]
    [SerializeField] private float _mouseSensitivity = 1.0f;

    [Header("Scroll setting")]
    [Range(1f, 10f)]
    [Tooltip("Sensitivity of scroll")]
    [SerializeField] private float _scrollSensitivity = 5.0f;

    [Header("Camera value")]
    [Tooltip("Value of camera distance")]
    [SerializeField] private float _cameraDistance = 5.0f;
    [Tooltip("Value of camera fieldOfView")]
    [SerializeField] private float _cameraFOV = 60.0f;
    #endregion

    #region Non-SerializeFields
    //Value of VirtualCamera clipPlane
    private (float, float) _clipPlane = (0.1f, 500f);

    //Value of CinemachinePOV horizontal axis
    private float _minMouseValueX = -180.0f;
    private float _maxMouseValueX = 180.0f;
    private float _sensitivityMouseX = 0.5f;
    private (float, float) _accelDecelX = (0.8f, 0.25f);

    //Value of CinemachinePOV vertical axis
    private float _minMouseValueY = -90.0f;
    private float _maxMouseValueY = 90.0f;
    private float _sensitivityMouseY = 0.3f;
    private (float, float) _accelDecelY = (0.8f, 0.05f);

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
        AxisState x = _cameraLookAtSetting.m_HorizontalAxis;
        x.Value = _initPosition.Item1;
        x.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        x.m_MinValue = _minMouseValueX;
        x.m_MaxValue = _maxMouseValueX;
        x.m_MaxSpeed = _sensitivityMouseX * _mouseSensitivity;
        x.m_AccelTime = _accelDecelX.Item1;
        x.m_DecelTime = _accelDecelX.Item2;
    }

    /// <summary>
    /// VerticalAxi(Y축) 세팅 메서드
    /// </summary>
    private void CameraAxisY()
    {
        AxisState y = _cameraLookAtSetting.m_VerticalAxis;
        y.Value = _initPosition.Item2;
        y.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        y.m_MinValue = _minMouseValueY;
        y.m_MaxValue = _maxMouseValueY;
        y.m_MaxSpeed = _sensitivityMouseY * _mouseSensitivity;
        y.m_AccelTime = _accelDecelY.Item1;
        y.m_DecelTime = _accelDecelY.Item2;
    }

    /// <summary>
    /// 줌인/줌아웃 메서드
    /// </summary>
    private void ZoomInOut()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput == 0.0f)
            return;
        _cameraDistance = Mathf.Clamp(_cameraDistance + -(wheelInput * _scrollSensitivity), _minDistance, _maxDistance);
    }

    /// <summary>
    /// 줌 메서드
    /// </summary>
    private void Zoom()
    {
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
