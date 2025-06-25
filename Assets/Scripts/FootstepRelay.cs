using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepRelay : MonoBehaviour
{
    private PlayerMovement player;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    public void PlayFootstepSound()
    {
        if (player != null)
        {
            player.PlayFootstepSound();
        }
    }
}
