using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class MainCameraController : MonoBehaviour
{
    [Header("Object")]
    [Tooltip("Transform to focus")]
    [SerializeField]  private Transform _focusObject;
    [Tooltip("Cinemachine VirtualCamera")]
    [SerializeField]  private CinemachineVirtualCamera _camera;
    [Tooltip("Cinemachine FramingTransposer Component")]
    [SerializeField]  private CinemachineFramingTransposer _cameraFollowSetting;
    [Tooltip("Cinemachine POV Component")]
    [SerializeField]  private CinemachinePOV _cameraLookAtSetting;

    [Header("Scroll setting")]
    [Range(0f, 10f)]
    [Tooltip("Sensitivity of scroll")]
    [SerializeField] private float _scrollSensitivity = 5.0f;

    [Header("Mouse setting")]
    [Range(0.5f, 1.5f)]
    [Tooltip("Sensitivity of mouse")]
    [SerializeField] private float _mouseSensitivity = 1.0f;

    [Header("Default value")]
    [Tooltip("Value of camera distance")]
    [SerializeField]  private float _cameraDistance = 5.0f;
    [Tooltip("Value of camera fieldOfView")]
    [SerializeField] private float _cameraFOV = 60.0f;

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
    public (float, float) _initPosition = (180, 0);

    //Value of Min/Max camera distance
    private float _minDistance = 1.0f;
    private float _maxDistance = 10.0f;
    
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
    private void InitCameraSetting()
    {
        //CameraSetting
        _camera.Follow = _focusObject;
        _camera.LookAt = _focusObject;
        _camera.m_Lens.FieldOfView = _cameraFOV;
        _camera.m_Lens.NearClipPlane = _clipPlane.Item1;
        _camera.m_Lens.FarClipPlane = _clipPlane.Item2;

        //Camera Follow(Body) Setting
        _cameraFollowSetting.m_CameraDistance = _initDistance;

        //Camera LookAt(Aim) Setting
        //Mouse_X
        _cameraLookAtSetting.m_HorizontalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        _cameraLookAtSetting.m_HorizontalAxis.m_MinValue = _minMouseValueX;
        _cameraLookAtSetting.m_HorizontalAxis.m_MaxValue = _maxMouseValueX;
        _cameraLookAtSetting.m_HorizontalAxis.m_MaxSpeed = _sensitivityMouseX * _mouseSensitivity;
        _cameraLookAtSetting.m_HorizontalAxis.m_AccelTime = _accelDecelX.Item1;
        _cameraLookAtSetting.m_HorizontalAxis.m_DecelTime = _accelDecelX.Item2;

        //Mouse_Y
        _cameraLookAtSetting.m_VerticalAxis.m_SpeedMode = AxisState.SpeedMode.InputValueGain;
        _cameraLookAtSetting.m_VerticalAxis.m_MinValue = _minMouseValueY;
        _cameraLookAtSetting.m_VerticalAxis.m_MaxValue = _maxMouseValueY;
        _cameraLookAtSetting.m_VerticalAxis.m_MaxSpeed = _sensitivityMouseY * _mouseSensitivity;
        _cameraLookAtSetting.m_VerticalAxis.m_AccelTime = _accelDecelY.Item1;
        _cameraLookAtSetting.m_VerticalAxis.m_DecelTime = _accelDecelY.Item2;

        //Init Position Setting
        _cameraLookAtSetting.m_HorizontalAxis.Value = _initPosition.Item1;
        _cameraLookAtSetting.m_VerticalAxis.Value = _initPosition.Item2;
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
    #endregion

    private void Awake()
    {
        _camera = gameObject.GetOrAddComponent<CinemachineVirtualCamera>();
        _cameraFollowSetting = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        _cameraLookAtSetting = _camera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Start()
    {
        _focusObject = GetFocusObject();
        if (!_focusObject)
        {
            //TODO
            return;
        }
        InitCameraSetting();
    }
    private void LateUpdate()
    {
        //ZoomIn/ZoomOut
        ZoomInOut();
        if (_cameraFollowSetting.m_CameraDistance == _cameraDistance)
        {
            Debug.Log("skip");
            return;
        }
        _cameraFollowSetting.m_CameraDistance = Mathf.Lerp(_cameraFollowSetting.m_CameraDistance, _cameraDistance, Time.deltaTime);
    }

    
}
