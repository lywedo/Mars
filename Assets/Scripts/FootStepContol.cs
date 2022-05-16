using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepContol : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;

    private bool jumping = false;

    float lastTime = 0;

    float jumpTime = 0;

    // Update is called once per frame
    void Update()
    {
        if ( jumping && Time.fixedTime - jumpTime > 1.5f )
        {
            jumping = false;
            audio.volume = Random.Range(0.8f,1f);
            audio.pitch = Random.Range(0.8f,1.1f);
            audio.Play();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            jumping = true;
            jumpTime = Time.fixedTime;
            audio.volume = Random.Range(0.8f,1f);
            audio.pitch = Random.Range(0.8f,1.1f);
            audio.Play();
        }
        if( Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Time.fixedTime-lastTime > 0.5f/*audio.isPlaying == false*/){
            audio.volume = Random.Range(0.8f,1f);
            audio.pitch = Random.Range(0.8f,1.1f);
            audio.Play();
            lastTime = Time.fixedTime;            
        }
    }


}
