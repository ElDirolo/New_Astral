using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cavelight : MonoBehaviour
{
    public GameObject Llamas;
    public GameObject Barreras;

    private Rigidbody DoorRigg;
    
    
    public AudioSource audioAntorcha;
    
    public AudioClip EncendidoSFX;
    public AudioClip ActivoSFX;

    public AudioSource AudioPuerta;
    public AudioClip PuertaArriba;

    void Start()
    {
        DoorRigg = Barreras.GetComponent<Rigidbody>();
        //audioAntorcha = GetComponent<AudioSource>();
        audioAntorcha.clip = ActivoSFX;
    }


    void OnTriggerEnter(Collider collider)
   {        
        if (this.gameObject.tag == "AntorchaCave" && collider.gameObject.tag == "Caliente")
        {
            Llamas.SetActive(true);
            Destroy(Barreras);
            Debug.Log("Cueva");
            audioAntorcha.PlayOneShot(EncendidoSFX);
            audioAntorcha.PlayDelayed(4f);            


        }
        if (this.gameObject.tag == "EndDoor" && collider.gameObject.tag == "Caliente")
        {
            Llamas.SetActive(true);
            DoorRigg.AddForce(0, -60, 0);
            Barreras.GetComponent<Collider>().enabled = false;
            Destroy(Barreras, 4f);
            audioAntorcha.PlayOneShot(EncendidoSFX);
            AudioPuerta.PlayOneShot(PuertaArriba);
            audioAntorcha.PlayDelayed(4f);


        }


    }
}



    

