using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Camera startCamera;
    public Canvas startCanvas;
    public Camera afterStartCamera;
    public Canvas afterStartCanvas;

    public Transform cameraTransform;
    private bool isStarted;

    public float speed;
    public float boostedSpeed;
    private float currentSpeed;

    public float rotationSencitivity;
    public float jumpForce;

    private float _xRotation;
    private bool _grounded;

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        currentSpeed = speed;
    }

    void Update()
    {
        if (!isStarted)
        {
            if (Input.anyKeyDown)
            {
                isStarted = true;

                startCamera.enabled = false;
                afterStartCamera.enabled = true;
                afterStartCanvas.gameObject.SetActive(true);
                startCanvas.gameObject.SetActive(false);
            }
        }
        if (Input.GetMouseButton(1))
        {
            _xRotation -= Input.GetAxis("Mouse Y") * rotationSencitivity;
            _xRotation = Mathf.Clamp(_xRotation, -60f, 60f);
            cameraTransform.localEulerAngles = new Vector3(_xRotation, 0f, 0f);

            transform.Rotate(0, Input.GetAxis("Mouse X") * rotationSencitivity, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_grounded)
            {
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }
    }

    private void _processAnimation(bool isWalking, bool isRunning)
    {
        if (isWalking)
        {
            animator.SetBool("Idle", true);
            if (isRunning)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        if (!isStarted || animator.GetBool("isTwerking"))
        {
            return;
        }
        bool isWalking = Input.GetKey(KeyCode.W);
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        _processAnimation(isWalking, isRunning);

        if (isWalking && isRunning)
        {
            currentSpeed = boostedSpeed;
        }
        else if (isWalking && !isRunning)
        {
            currentSpeed = speed;
        }

        Vector3 inputVector = new Vector3(0f, 0f, Mathf.Clamp01(Input.GetAxis("Vertical")));
        Vector3 speedVectorRelativeToPlayer = transform.TransformVector(inputVector * currentSpeed);

        GetComponent<Rigidbody>().velocity = new Vector3(speedVectorRelativeToPlayer.x, GetComponent<Rigidbody>().velocity.y, speedVectorRelativeToPlayer.z);
    }

    public void OnCollisionStay(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        float dot = Vector3.Dot(normal, Vector3.up);
        if (dot > 0.5f)
        {
            _grounded = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        _grounded = false;
    }
}
