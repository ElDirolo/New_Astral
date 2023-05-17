using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermove : MonoBehaviour
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
    [SerializeField]private float jumpHeight = 1f;
    [SerializeField]private float gravity = -9.81f;    
    
    public Transform bulletSpawn;
    public Transform NormalSpawn;   
    public Transform AimSpawn;
    private int powerType;


    [SerializeField] private bool shootColdDown = false;
    [SerializeField] private float shootDuration = 1f;
    [SerializeField] private float shootTimer = 0f;

    public GameObject[] cameras;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;


    public AudioSource PlayerAudio;
    public AudioSource PlayerAudiowalk;
    public AudioSource PlayerAudioJump;
    public AudioSource PlayerAudiChangesPower;
    public AudioClip ChangesPower;
    public AudioClip FireSFX;
    public AudioClip WhaterSFX;
    public AudioClip hitPlayer;
    public AudioClip walkPlayer;
    public AudioClip JumpPlayer;


    

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        PlayerAudio = GetComponent<AudioSource>();
        PlayerAudiowalk.clip = walkPlayer;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movimiento();
        Jump();
        Disparo();
    
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
        if(Input.GetButton("Fire2") && Global.PlayerScript == false)
        {
            bulletSpawn = AimSpawn;
            anim.SetBool("Apuntando", true);
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
            speed = 2f;
        }
        else
        {
            speed = 5f;
            anim.SetBool("Apuntando", false);
            bulletSpawn = NormalSpawn;
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
        }

        if(movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref currentVelocity, shoothTime);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            PlayerAudiowalk.Play();
            if(moveDirection == Vector3.zero)
            {
                PlayerAudiowalk.Play();
            }
            else
            {
                PlayerAudiowalk.Stop();
            }   
               
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

        if(isGrounded && Input.GetButtonDown("Jump") && Global.PlayerScript == false)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayerAudioJump.PlayOneShot(JumpPlayer);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);        
    }

    void Disparo()
    {

        if (shootColdDown == true)
        {
            if (shootTimer <= shootDuration)
            {
                shootTimer += Time.deltaTime;
            }
            else
            {
                shootColdDown = false;
                shootTimer = 0f;
            }
        }

        if (Input.GetButtonDown("Fire1") && isGrounded && shootColdDown == false && Global.PlayerScript == false)
        {
            
            GameObject powerBall = PoolManager.Instance.GetPooledPower(powerType, bulletSpawn.position, bulletSpawn.rotation);
            powerBall.SetActive(true);

            shootColdDown = true;
            anim.SetBool("Ataque", true);

            if(powerType == 1)
            {
                PlayerAudio.PlayOneShot(FireSFX);
            }
            if(powerType == 0)
            {
                PlayerAudio.PlayOneShot(WhaterSFX);
            }
        }
        else
        {
            anim.SetBool("Ataque", false);
        }


        if(Input.GetKeyDown(KeyCode.Alpha1) && Global.PlayerScript == false)
        {
            Debug.Log("Fuego");
            powerType = 1;
            PlayerAudiChangesPower.PlayOneShot(ChangesPower);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4) && Global.PlayerScript == false)
        {
            Debug.Log("Agua");
            powerType = 0;
            PlayerAudiChangesPower.PlayOneShot(ChangesPower);
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        if(collider.CompareTag("AI"))
        {
            GameManager.Instance.Impacto();
            PlayerAudio.PlayOneShot(hitPlayer);
        }

        if(collider.gameObject.CompareTag("KillZone"))
        {
            PlayerAudio.PlayOneShot(hitPlayer);
        }

    }
    void Muerte()
    {
        if(Global.vidas > 0)
        {
            Global.nivel = PlayerPrefs.GetInt("LevelMax");
            SceneManager.LoadScene(Global.nivel);
            Global.vidas = 3;

        }
    }    
}
