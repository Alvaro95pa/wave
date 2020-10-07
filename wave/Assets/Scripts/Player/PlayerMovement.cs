using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxisInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(xAxisInput, 0, 0) * _speed * Time.deltaTime);

        float zAxisInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, zAxisInput) * _speed * Time.deltaTime);
    }
}
