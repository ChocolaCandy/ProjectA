using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Camera))]
public class MainCameraController : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    [Tooltip("Transform to focus")]
    private Transform _focusObject = null;
    [SerializeField]
    [Tooltip("Main camera component")]
    private Camera _camera = null;

    [Header("Scroll setting")]
    [SerializeField]
    [Tooltip("Sensitivity of scroll")]
    private float ScrollSensitivity = 100.0f;
    [SerializeField]
    [Tooltip("Min value of camera FOV")]
    private float MinFOV = 20.0f;
    [SerializeField]
    [Tooltip("Max value of camera FOV")]
    private float MaxFOV = 60.0f;

    [Header("Mouse setting")]
    [SerializeField]
    private float MouseSensitivity = 100.0f;

    [Header("Default value")]
    [SerializeField]
    [Tooltip("Init distance between camera and player")]
    private float _cameraDistance = 5.0f;
    [SerializeField]
    [Tooltip("Init value of camera FOV")]
    private float _initFOV = 20.0f;
    [SerializeField]
    [Tooltip("Default value of EndPoint FOV")]
    private float _defaultFOV = 40.0f;

    [Header("Debug value")]
    [SerializeField]
    [Tooltip("Camera vector from player")]
    private Vector3 _endPointPosition = Vector3.zero;
    [SerializeField]
    [Tooltip("Camera Fov")]
    public float _endPointFOV = 0.0f;

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
        //줌인/줌아웃
        ZoomInOut();

        //회전
        RotatePlayer();

        //카메라 트랜스폼 변경
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _endPointFOV, Time.deltaTime * 1.5f);
        transform.position = _focusObject.position + _endPointPosition;

        //Todo : bug fix inverse coordinate
        //transform.LookAt(_focusObject, _focusObject.up);
    }

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
    private void InitCameraPosition()
    {
        Vector3 initPosition = _focusObject.position + -(_focusObject.forward) * (Mathf.Abs(_cameraDistance));
        transform.position = initPosition;
        _camera.fieldOfView = _initFOV;
        _endPointPosition = transform.position - _focusObject.position;
        _endPointFOV = _defaultFOV;
    }

    /// <summary>
    /// 줌인/줌아웃 메서드
    /// </summary>
    private void ZoomInOut()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput == 0.0f)
        {
            return;
        }
        float afterFOV = Mathf.Clamp(_camera.fieldOfView + -(wheelInput * ScrollSensitivity), MinFOV, MaxFOV);
        _endPointFOV = afterFOV;
    }
    #endregion

    float min = -90.0f;
    float max = 90.0f;
    public float value = 0.0f;

    public void RotatePlayer()
    {
        Rotate_AxisX();
        Rotate_AxisY();
    }

    public void Rotate_AxisX()
    {
        float xMouseInput = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;

        Quaternion quaternion = Quaternion.AngleAxis(xMouseInput, _focusObject.transform.up);
        Vector3 vector2 = quaternion * _endPointPosition;
        _endPointPosition = vector2;
    }

    public void Rotate_AxisY()
    {
        float yMouseInput = Input.GetAxis("Mouse Y") * MouseSensitivity * 0.5f * Time.deltaTime;
        float angleValue = Mathf.Clamp(value + -yMouseInput, min, max);
        if (angleValue <= min || angleValue >= max)
            return;
        Quaternion quaternion1 = Quaternion.AngleAxis(-yMouseInput, transform.right);
        Vector3 afterY = quaternion1 * _endPointPosition;
        value += -yMouseInput;
        _endPointPosition = afterY;
    }
}
