using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField]
    private float _jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool down = Input.GetButtonDown("Jump");
        bool held = Input.GetButton("Jump");
        bool up = Input.GetButtonUp("Jump");

        if (down)
        {
            transform.Translate(Vector3.up * _jumpHeight);
        }
        else if (held)
        {

        }
        else if (up)
        {

        }
    }
}
