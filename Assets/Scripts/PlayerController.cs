using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotation = 50;
    private void Start()
    { 
        Managers.InputManager.KeyBoardInput += Key;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
    }
    
    void Key()
    {
        //TODO 캐릭터 움직임 개선(테스트용)
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += Vector3.back * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += Vector3.forward * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * 3 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.rotation *= Quaternion.Euler(Vector3.down * Time.deltaTime * rotation);
        }
        if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.rotation *= Quaternion.Euler(Vector3.up * Time.deltaTime * rotation);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
}
