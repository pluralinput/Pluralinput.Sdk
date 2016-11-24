using UnityEngine;
using System.Collections;
using Pluralinput.Sdk;

public class CursorController : MonoBehaviour
{

    public Mouse mouse;
    private Vector3 lastMouseMotion;

    // Use this for initialization
    void Start()
    {
        mouse.Move += (o, e) =>
        {
            lastMouseMotion.x = e.LastX;
            lastMouseMotion.y = -e.LastY;

            lastMouseMotion *= 0.01f;

            transform.position += lastMouseMotion;
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        
    }
}
