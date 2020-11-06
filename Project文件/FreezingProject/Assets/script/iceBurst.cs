using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceBurst : MonoBehaviour
{
    GameObject iceGound;
    GameObject stones;
    GameObject iceSmog;
    GameObject iceSmog1;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        //get the corresponding objects
        material = gameObject.GetComponent<MeshRenderer>().materials[0];
        iceGound = GameObject.Find("iceGround");
        stones = GameObject.Find("stones");
        iceSmog = GameObject.Find("iceSmog");
        iceSmog1 = GameObject.Find("iceSmog1");
        
        //reset everything and stop the Particle Animation
        stopParticle();
        material.SetFloat("_TransformProgress", -0.5f);
        material.SetFloat("_fractionProgress", 0f);
        material.SetFloat("_fractionIntensity", 1f);
        material.SetFloat("_fractionExtention", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))//start the designed animtion by using Coroutine
        {
            StartCoroutine(switchTexture());
            //StartCoroutine(switchTexture());
        }
    }
    IEnumerator switchTexture()
    {
        bool played = false;//to make sure the animation sequences being played only once.
        GetComponent<MeshRenderer>().enabled = true;//turn on the meshRenderer which would be turn off later
        //ok, now we start playing, yet still we need to make sure everything are set as expected
        stopParticle();
        material.SetFloat("_TransformProgress", -0.5f);
        material.SetFloat("_fractionProgress", 0f);
        material.SetFloat("_fractionIntensity", 1f);
        material.SetFloat("_fractionExtention", 0.1f);
        //this animation will go through almost the entire process to add some atomosphere
        iceSmog1.GetComponent<ParticleSystem>().Play();
        // the first animation change the _TransformProgress property to freeze the model
        while (material.GetFloat("_TransformProgress") < 1.5)
        {
            yield return new WaitForSeconds(0.01f);
            material.SetFloat("_TransformProgress", material.GetFloat("_TransformProgress")+0.01f);
        }
        // the second one add fraction to the iced model so it seems that it's going to break
        while (material.GetFloat("_fractionProgress") < 1.1f)
        {
            yield return new WaitForSeconds(0.01f);
            material.SetFloat("_fractionProgress", material.GetFloat("_fractionProgress") + 0.01f);
            if (!played&& material.GetFloat("_fractionProgress")>1f)//at a certain moment we play the Particle animation
            {
                played = true;
                playParticle();
            }
            //increasing the fraction intensity makes the animation more vivid
            material.SetFloat("_fractionIntensity", material.GetFloat("_fractionIntensity") + 0.08f* material.GetFloat("_fractionProgress"));
        }
        //turn off the mesh renderer so that the model seems disappeared during the explosion
        GetComponent<MeshRenderer>().enabled = false;
        played = false;
    }
    void stopParticle()//stop the Particle Aniamtion
    {
        iceGound.GetComponent<ParticleSystem>().Stop();
        stones.GetComponent<ParticleSystem>().Stop();
        iceSmog.GetComponent<ParticleSystem>().Stop();
    }
    void playParticle()//play the Particle Aniamtion
    {
        iceGound.GetComponent<ParticleSystem>().Play();
        stones.GetComponent<ParticleSystem>().Play();
        iceSmog.GetComponent<ParticleSystem>().Play();
    }
}
