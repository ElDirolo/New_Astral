using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInHouse : MonoBehaviour
{

    private CharacterController controller;

    private Transform cam;

    private Animator anim;

    public Transform LookAtTransform;

    private float currentVelocity;

    [SerializeField]private float shoothTime = 0.5f;
    [SerializeField]private Transform groundSensor;
    [SerializeField]private float sensorRadius = 0.2f;
    [SerializeField]private LayerMask gorundLayer;
    private Vector3 playerVelocity;
    [SerializeField]private bool isGrounded;
    [SerializeField]private LayerMask detectorLayer;

    [SerializeField]private float speed;
    [SerializeField]private float gravity = -9.81f;    
    



    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;



    

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movimiento();
        Jump();
    
        if (Global.PlayerScript == true)
        {
            anim.SetBool("Charla", true);
            
        }
        else
        {
            anim.SetBool("Charla", false);
        }
    }
    void Movimiento()
    {

        float z = Input.GetAxisRaw("Vertical");
        anim.SetFloat("MovZ", z);
        float x = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("MovX", x);


        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, xAxis.Value, 0);
        LookAtTransform.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, LookAtTransform.eulerAngles.z);
        
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if(movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref currentVelocity, shoothTime);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);        
        }


    }

    void Jump()
    {
        //Utilizamos un RayCast para el sensordelsuelo
        anim.SetBool("Jump", !isGrounded);
        if(Physics.Raycast(groundSensor.position, Vector3.down, sensorRadius, gorundLayer))
        {
            isGrounded = true;
            Debug.DrawRay(groundSensor.position, Vector3.down * sensorRadius, Color.green);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(groundSensor.position, Vector3.down * sensorRadius, Color.red);

        }


        if(playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        /*if(isGrounded && Input.GetButtonDown("Jump") && Global.PlayerScript == false)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);        
    }


    void OnTriggerEnter(Collider collider)
    {

        if(collider.CompareTag("AI"))
        {
            GameManager.Instance.Impacto();
        }

    }
 
}
