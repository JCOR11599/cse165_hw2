using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(70.0f, transform.eulerAngles.y, transform.eulerAngles.z);   
    }
}
