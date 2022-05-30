using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [Header("Layermask")]
    [SerializeField]
    private LayerMask Ground;

    [Header("PlayerController")]
    [SerializeField]
    private CharacterController Player;

    [Header("TransformPos")]
    [SerializeField]
    private Transform PlayerCamera;
    [SerializeField]
    private Transform SpherePos;
    [SerializeField]
    private Transform Campos;
    [SerializeField]
    private Transform Ori;
    //[Header("Floats")]
    private float MovementSpeed = 5f;
    private float MouseSenstivity = 3;
    private float VerticalMovement = 0;
    private float HorizontalMovement = 0;
    private float VerticalMouse = 180;
    private float HorizontalMouse = 0;
    private float GravityMutlplier = 20;
    // private float MaxSpeed = 20;
    [Header("Bools")]
    [SerializeField]
    private bool IsGrounded = false;

    private Vector3 VerticalAndHorizontal;
    private Vector2 Grav;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void LateUpdate()
    {
        PlayerCamera.position = Campos.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Inputs ------------------------------------------------------
        VerticalMovement = Input.GetAxis("Vertical");
        HorizontalMovement = Input.GetAxis("Horizontal");
        float Mouse_Y = Input.GetAxis("Mouse Y") * MouseSenstivity;
        float Mouse_X = Input.GetAxis("Mouse X") * MouseSenstivity;
        // Debug.Log(VerticalAndHorizontal);
        HorizontalMouse -= Mouse_Y;
        VerticalMouse += Mouse_X;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //MaxSpeed = 10;
            MovementSpeed = 10f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            // MaxSpeed = 5;
            MovementSpeed = 5f;
        }

        // Movement -------------------------------------------------------------------------------------------

        VerticalAndHorizontal = PlayerCamera.right * HorizontalMovement + Ori.forward * VerticalMovement;
        //VerticalAndHorizontal *= 0.95f;


        // rotate camera -----------------------------------------------------------------------------------------------------------------
        PlayerCamera.rotation = Quaternion.Euler( HorizontalMouse, VerticalMouse, 0);
        Ori.rotation = Quaternion.Euler(0, VerticalMouse, 0);
        // Clamp  ---------------------------------------------------------------------------------------
        HorizontalMouse = Mathf.Clamp(HorizontalMouse, -80, 80);
        //VerticalAndHorizontal =  Vector3.ClampMagnitude(VerticalAndHorizontal, MaxSpeed);
        // Move Player -------------------------------------------------------------------------------------------------------------------------------
        Player.Move(VerticalAndHorizontal * MovementSpeed * Time.deltaTime);

        // Check If Player is Grounded ---------------------------------------------------------------------------------------------------------------
        IsGrounded = Physics.CheckSphere(SpherePos.position, 0.4f, Ground, QueryTriggerInteraction.Ignore);

        if (!IsGrounded)
        {
            Grav.y -= Time.deltaTime * GravityMutlplier;
            Player.Move(Grav * Time.deltaTime);
        }
        else if (Grav.y != 0)
        {
            Grav.y = 0;
        }
    }
}
