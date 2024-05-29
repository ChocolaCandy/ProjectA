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
    /// ����ī�޶� ���� �޼���
    /// </summary>
    private void MainCameraSetting(CinemachineBrain cinemachineBrain)
    {
        cinemachineBrain.m_IgnoreTimeScale = true;
    }
}
