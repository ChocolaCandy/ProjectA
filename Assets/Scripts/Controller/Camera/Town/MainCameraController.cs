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
    /// 시네머신 브레인 컴포넌트 추가/설정 메서드
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
