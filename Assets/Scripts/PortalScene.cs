using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.Backspace))
            SceneManager.LoadScene("Test_Title");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Test_Title");
        }
    }
}
