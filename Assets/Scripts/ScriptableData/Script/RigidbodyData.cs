using UnityEngine;

[CreateAssetMenu(fileName = "RigidbodyData", menuName = "ScriptableObject/RigidbodyData")]
public class RigidbodyData : ScriptableObject
{
    #region Fields
    //RigidbodyData Fields
    [SerializeField] private float _mass = 1.0f; //����
    [SerializeField] private float _drag = 0.0f; //�׷�
    [SerializeField] private float _angularDrag = 0.05f; //ȸ���׷�
    [SerializeField] private bool _automaticCenterOfMass = true; //�����߽� ��� ���俩��
    [SerializeField] private bool _automaticTensor = true; //ȸ�� �����ټ� ��� ���俩��
    [SerializeField] private bool _useGravity = true; //�߷� ��� ����
    [SerializeField] private bool _isKinematic = false; //������� ����
    [SerializeField] private RigidbodyInterpolation _interpolate = RigidbodyInterpolation.None; //������� ������ ���� ����
    [SerializeField] private CollisionDetectionMode _collisionDetection = CollisionDetectionMode.Discrete; //�浹���� ��� ����
    [SerializeField] private RigidbodyConstraints _rigidbodyConstraints = RigidbodyConstraints.None; //������ ���� ����
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
