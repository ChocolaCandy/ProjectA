using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public class MainCameraController : MonoBehaviour
{
    private void Awake()
    {
        CinemachineVirtualCameraBase VCam = FindObjectOfType<CinemachineVirtualCameraBase>();
        if (VCam)
            AddSetVcamBrain();
    }

    /// <summary>
    /// �ó׸ӽ� �극�� ������Ʈ �߰�/���� �޼���
    /// </summary>
    private void AddSetVcamBrain()
    {
        CinemachineBrain cinemachineBrain = gameObject.GetOrAddComponent<CinemachineBrain>();
        cinemachineBrain.m_IgnoreTimeScale = true;
    }

    private void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        if (gameObject)
            transform.position = gameObject.transform.position;
    }
}
