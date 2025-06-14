using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    public AudioClip[] foodstepClips;
    public AudioSource audioSource;
    public float StepInterval = 0.5f;
    private CharacterController controller;
    private float stepTimer = 0f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            stepTimer += Time.fixedDeltaTime;
            if (stepTimer >= StepInterval)
            {
                PlayFootStep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
    void PlayFootStep()
    {
        if (foodstepClips.Length == 0) return;
        int index = Random.Range(0, foodstepClips.Length);
        audioSource.PlayOneShot(foodstepClips[index]);
    }
}
