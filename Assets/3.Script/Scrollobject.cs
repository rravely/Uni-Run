using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollobject : MonoBehaviour
{
    public float Speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameover)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }
}
