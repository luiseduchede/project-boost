using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float vertThrust = 100f;
    [SerializeField] float rotSpd = 100f;
    [SerializeField] AudioClip rocketBoostAudio;

    [SerializeField] ParticleSystem rocketBoostParticle;
    [SerializeField] ParticleSystem auxBoostParticleLeft;
    [SerializeField] ParticleSystem auxBoostParticleRight;


    Rigidbody rb;
    AudioSource boostAudio;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        boostAudio = GetComponent<AudioSource>();
    }

    // Update is called once per framea
    void Update() {
        
        Thrust();
        Rotate();
    }

    private void Rotate() {
        //rb.freezeRotation = true;
        if (Input.GetKey(KeyCode.A)) {
            ApplyRotation(rotSpd);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotSpd);
        }
        else {
            EndRotation();
        }
        //rb.freezeRotation = false;
    }

    private void ApplyRotation(float rotSpd) {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        transform.Rotate(Vector3.forward * rotSpd * Time.deltaTime);
        //------------------------------------------------VFX STARTS-----------------------------------------------
        if (rotSpd < 0 && !auxBoostParticleLeft.isPlaying)
            auxBoostParticleLeft.Play();
        else if (rotSpd > 0 && !auxBoostParticleRight.isPlaying)
            auxBoostParticleRight.Play();
        //------------------------------------------------VFX ENDS--------------------------------------------------
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }

    private void EndRotation() {
        auxBoostParticleRight.Stop();
        auxBoostParticleLeft.Stop();
    }

    void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            EndThrust();
        }
    }

    private void ApplyThrust() {
        rb.AddRelativeForce(Vector3.up * vertThrust * Time.deltaTime);
        //------------------------------------VFX AND SFX STARTS---------------------------------------------------------
        if (!boostAudio.isPlaying)
            boostAudio.PlayOneShot(rocketBoostAudio);
        if (!rocketBoostParticle.isPlaying)
            rocketBoostParticle.Play();
        //------------------------------------VFX AND SFX ENDS-----------------------------------------------------------
    }

    private void EndThrust() {
        boostAudio.Stop();
        rocketBoostParticle.Stop();
    }
}
