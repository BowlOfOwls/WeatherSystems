using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameInput gameInput;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] private float flySpeed = 5.0f;

    private void Awake()
    {
        gameInput.OnScrollPerformedEvent += GameInput_OnScrollPerformedEvent;
    }

    private void GameInput_OnScrollPerformedEvent(object sender, GameInput.OnScrollPerformedEventArg e)
    {
        if (e.scroll > 0)
        {
            transform.position += new Vector3(0, flySpeed, 0) * Time.deltaTime;
        } else if (e.scroll < 0)
        {
            transform.position += new Vector3(0, -flySpeed, 0) * Time.deltaTime;

        }
    }

    void Update()
    {
        handleMovement();
        handleLookAround();
    }


    public void handleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //Vector3 moveDir = new Vector3(inputVector.x, 0, ;
        //transform.position += moveDir * speed * Time.deltaTime;

        transform.position += new Vector3(transform.forward.x, 0, transform.forward.z).normalized * inputVector.y * speed * Time.deltaTime;
        transform.position += new Vector3(transform.right.x, 0, transform.right.z).normalized * inputVector.x * speed * Time.deltaTime;
    }

    public void handleLookAround()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-mouseY * sensitivity, mouseX * sensitivity, 0);
    }

}

