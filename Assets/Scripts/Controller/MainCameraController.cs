using Cinemachine;
using UnityEngine;


public class MainCameraController : MonoBehaviour
{
    private void Awake()
    {
        CinemachineBrain cinemachineBrainCamera = gameObject.GetOrAddComponent<CinemachineBrain>();
        MainCameraSetting(cinemachineBrainCamera);
    }


    /// <summary>
    /// 메인카메라 세팅 메서드
    /// </summary>
    private void MainCameraSetting(CinemachineBrain cinemachineBrain)
    {
        cinemachineBrain.m_IgnoreTimeScale = true;
    }
}
