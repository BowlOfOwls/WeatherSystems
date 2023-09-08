using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float sensitivity = 2.0f;
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z).normalized * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.position += new Vector3(transform.right.x, 0, transform.right.z).normalized  * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    }
}

