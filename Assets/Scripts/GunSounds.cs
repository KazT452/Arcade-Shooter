using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    [SerializeField] Player player;

    public AudioSource audioSource;
    public AudioClip shoot;
    public AudioClip reload;

    private void Awake()
    {
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(shoot);
       
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            
        }

        
    }

    
}
