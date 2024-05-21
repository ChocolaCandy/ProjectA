using UnityEngine;

[CreateAssetMenu(fileName = "RigidbodyData", menuName = "ScriptableObject/RigidbodyData")]
public class RigidbodyData : ScriptableObject
{
    #region Fields
    //RigidbodyData Fields
    [SerializeField] private float _mass = 1.0f; //질량
    [SerializeField] private float _drag = 0.0f; //항력
    [SerializeField] private float _angularDrag = 0.05f; //회전항력
    [SerializeField] private bool _automaticCenterOfMass = true; //질량중심 계산 오토여부
    [SerializeField] private bool _automaticTensor = true; //회전 관성텐서 계산 오토여부
    [SerializeField] private bool _useGravity = true; //중력 사용 여부
    [SerializeField] private bool _isKinematic = false; //물리계산 여부
    [SerializeField] private RigidbodyInterpolation _interpolate = RigidbodyInterpolation.None; //물리계산 움직임 보간 설정
    [SerializeField] private CollisionDetectionMode _collisionDetection = CollisionDetectionMode.Discrete; //충돌감지 방법 설정
    [SerializeField] private RigidbodyConstraints _rigidbodyConstraints = RigidbodyConstraints.None; //움직임 제한 설정
    #endregion

    /// <summary>
    /// Set fields value to parameter rigidbody 
    /// </summary>
    /// <param name="rigidbody"></param>
    public void Init(Rigidbody rigidbody)
    {
        rigidbody.mass = _mass;
        rigidbody.drag = _drag;
        rigidbody.angularDrag = _angularDrag;
        rigidbody.automaticCenterOfMass = _automaticCenterOfMass;
        rigidbody.automaticInertiaTensor = _automaticTensor;
        rigidbody.useGravity = _useGravity;
        rigidbody.isKinematic = _isKinematic;
        rigidbody.interpolation = _interpolate;
        rigidbody.collisionDetectionMode = _collisionDetection;
        rigidbody.constraints = _rigidbodyConstraints;
    }
}
