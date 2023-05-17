using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antorchas : MonoBehaviour
{

    public GameObject Llamas;
    public GameObject Puerta;
    public  bool activado = false;
    [SerializeField] private GameObject camaraAntorcha;

    private Rigidbody DoorRigg;
    
    public Animator anim;
    
    private AudioSource audioAntorcha;
    public AudioSource AudioPuerta;
    public AudioClip EncendidoSFX;
    public AudioClip ActivoSFX;
    public AudioClip PuertaArriba;

    void Start()
    {
        DoorRigg = Puerta.GetComponent<Rigidbody>();
        anim = Puerta.GetComponentInChildren<Animator>();
        audioAntorcha = GetComponent<AudioSource>();
        audioAntorcha.clip = ActivoSFX;
    }


    void OnTriggerEnter(Collider collider)
   {        
    
        if(collider.gameObject.tag == "Caliente" && activado == false && Global.bigTorch == 3)
        {
            Global.bigTorch++;
            activado = true;
            Llamas.SetActive(true);
            camaraAntorcha.SetActive(true);
            anim.SetBool("Desbloqueado", true);
            Destroy(camaraAntorcha, 4f);
            Destroy(Puerta, 4f);
            Debug.Log("feura");
            
            audioAntorcha.PlayOneShot(EncendidoSFX);
            AudioPuerta.PlayOneShot(PuertaArriba);
            audioAntorcha.PlayDelayed(4f);




        }
        if(collider.gameObject.tag == "Caliente" && activado == false)
        {
            activado = true;
            Llamas.SetActive(true);
            Global.bigTorch++;

            audioAntorcha.PlayOneShot(EncendidoSFX);
            audioAntorcha.PlayDelayed(4f);



        }

        if (this.gameObject.tag == "Antorcha" && collider.gameObject.tag == "Caliente")
        {
            
            Llamas.SetActive(true);
            DoorRigg.AddForce(0, 50, 0);
            Puerta.GetComponent<Collider>().enabled = false;
            Destroy(Puerta, 4f);
            camaraAntorcha.SetActive(true);
            Destroy(camaraAntorcha, 4f);


            audioAntorcha.PlayOneShot(EncendidoSFX);
            AudioPuerta.PlayOneShot(PuertaArriba);
            audioAntorcha.PlayDelayed(4f);
            
        }

        if (this.gameObject.tag == "BigAntorcha" && collider.gameObject.tag == "Caliente")
        {
            Llamas.SetActive(true);
            Destroy(Puerta);
            Debug.Log("Cueva");
            


        }
        if (this.gameObject.tag == "EndDoor" && collider.gameObject.tag == "Caliente")
        {
            Llamas.SetActive(true);
            DoorRigg.AddForce(0, -50, 0);
            Puerta.GetComponent<Collider>().enabled = false;
            Destroy(Puerta, 4f);


        }


    }
}
