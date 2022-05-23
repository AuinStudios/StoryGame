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

    //[Header("Floats")]
    private float MovementSpeed = 5;
    private float MouseSenstivity = 5;
    private float VerticalMovement = 0;
    private float HorizontalMovement = 0;
    private float VerticalMouse = 0;
    private float HorizontalMouse = 0;
    private float GravityMutlplier = 20;
    //private float MaxSpeed = 5;
    //[Header("Bools")]
    //[SerializeField]
    private bool IsGrounded = false;

    private Vector3 VerticalAndHorizontal;
    private Vector2 Grav;
  
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        //Inputs ------------------------------------------------------
        VerticalMovement = Input.GetAxis("Vertical");
        HorizontalMovement = Input.GetAxis("Horizontal");
        HorizontalMouse -= Input.GetAxis("Mouse Y") * MouseSenstivity;
        VerticalMouse += Input.GetAxis("Mouse X") * MouseSenstivity;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           // MaxSpeed = 10;
            MovementSpeed = 10;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //MaxSpeed = 1;
            MovementSpeed = 5;
        }
        
        // Movement -------------------------------------------------------------------------------------------
        VerticalAndHorizontal = PlayerCamera.right * HorizontalMovement + transform.forward * VerticalMovement;
        // rotate camera -----------------------------------------------------------------------------------------------------------------
        PlayerCamera.rotation = Quaternion.AngleAxis(VerticalMouse, Vector3.up) * Quaternion.AngleAxis(HorizontalMouse, Vector3.right);
        transform.rotation = Quaternion.AngleAxis(VerticalMouse, Vector3.up);
        // Move Player -------------------------------------------------------------------------------------------------------------------------------
        Player.Move(VerticalAndHorizontal * MovementSpeed * Time.deltaTime);
        // Clamp  ---------------------------------------------------------------------------------------
        HorizontalMouse = Mathf.Clamp(HorizontalMouse, -80, 80);
        
        // Check If Player is Grounded ---------------------------------------------------------------------------------------------------------------
        IsGrounded = Physics.CheckSphere(SpherePos.position, 0.3f, Ground, QueryTriggerInteraction.Ignore);

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
