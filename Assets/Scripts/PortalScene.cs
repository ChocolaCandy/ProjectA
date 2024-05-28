using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene : MonoBehaviour
{
    public string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player3D"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
