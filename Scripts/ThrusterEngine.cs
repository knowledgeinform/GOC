using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterEngine : MonoBehaviour
{
    FlyController fs;
    //this can be optimized using a dictionary, but for the purposes of the demo, we will use this.

    // Start is called before the first frame update
    // Update is called once per frame
    ParticleSystem ss1;
    ParticleSystem ss2;
    ParticleSystem ss3;
    ParticleSystem ss4;
    void Start() 
    {
        
        ss1 = GameObject.Find("thruster1").GetComponent<ParticleSystem>();
        ss2 = GameObject.Find("thruster2").GetComponent<ParticleSystem>();
        ss3 = GameObject.Find("thruster3").GetComponent<ParticleSystem>();
        ss4 = GameObject.Find("thruster4").GetComponent<ParticleSystem>();
        
        var main = ss1.main;
        var main2 = ss2.main;
        var main3 = ss3.main;
        var main4 = ss4.main;

        main.simulationSpeed = 7.0f;
        main2.simulationSpeed = 7.0f;
        main3.simulationSpeed = 7.0f;
        main4.simulationSpeed = 7.0f;

    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            GameObject.Find("thruster1").GetComponent<ParticleSystem>().Play();
            GameObject.Find("thruster2").GetComponent<ParticleSystem>().Play();
            GameObject.Find("thruster3").GetComponent<ParticleSystem>().Play();
            GameObject.Find("thruster4").GetComponent<ParticleSystem>().Play();
        }
        else {
            GameObject.Find("thruster1").GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            GameObject.Find("thruster2").GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            GameObject.Find("thruster3").GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
            GameObject.Find("thruster4").GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}