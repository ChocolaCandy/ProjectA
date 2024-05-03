using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject a;

    private void Update()
    {
        transform.RotateAround(a.transform.position, Vector3.up, 20 * Time.deltaTime);
    }
}
