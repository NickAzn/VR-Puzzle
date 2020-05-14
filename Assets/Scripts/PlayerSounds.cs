using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioSource ballAudio;
    public float audioRollSpeedThreshold;
    public float audioRollCap;
    public float audioImpactSpeedThreshold;
    public float audioImpactCap;

    public AudioClip startRoll;
    public AudioClip[] midRoll;
    public AudioClip endRoll;
    public AudioClip impact;

    private Rigidbody rb;

    private Vector3 prevVelocity = Vector3.zero;
    private List<GameObject> touching = new List<GameObject>();

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 10) {
            touching.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (touching.Contains(collision.gameObject)) {
            touching.Remove(collision.gameObject);
        }
    }

    private void FixedUpdate() {
        if (touching.Count == 0) {
            ballAudio.Stop();
            return;
        }
        if (rb.velocity.magnitude >= audioRollSpeedThreshold && !ballAudio.isPlaying) {
            if (prevVelocity.magnitude - rb.velocity.magnitude >= audioImpactSpeedThreshold) {
                ballAudio.pitch = Random.Range(0.75f, 1.05f);
                ballAudio.volume = Mathf.Lerp(0.5f, 1f, (prevVelocity.magnitude - rb.velocity.magnitude - audioImpactSpeedThreshold) / (audioImpactCap - audioImpactSpeedThreshold));
                ballAudio.clip = impact;
                ballAudio.Play();
            } else if (prevVelocity.magnitude < audioRollSpeedThreshold ) {
                ballAudio.pitch = Random.Range(0.95f, 1.05f);
                ballAudio.volume = Mathf.Lerp(0.5f, 1f, (rb.velocity.magnitude - audioRollSpeedThreshold) / (audioRollCap - audioRollSpeedThreshold));
                ballAudio.clip = startRoll;
                ballAudio.Play();
            } else if (!ballAudio.isPlaying) {
                ballAudio.pitch = Random.Range(0.85f, 1.15f);
                ballAudio.clip = midRoll[Random.Range(0, midRoll.Length)];
                ballAudio.volume = Mathf.Lerp(0.5f, 1f, (rb.velocity.magnitude - audioRollSpeedThreshold) / (audioRollCap - audioRollSpeedThreshold));
                ballAudio.Play();
            }
        } else if (prevVelocity.magnitude >= audioRollSpeedThreshold) {
            ballAudio.pitch = Random.Range(0.95f, 1.05f);
            ballAudio.clip = endRoll;
            ballAudio.volume = Mathf.Lerp(0.5f, 1f, (rb.velocity.magnitude - audioRollSpeedThreshold) / (audioRollCap - audioRollSpeedThreshold));
            ballAudio.Play();
        }
        prevVelocity = rb.velocity;
    }
}
