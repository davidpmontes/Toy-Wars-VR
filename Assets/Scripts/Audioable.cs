using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioable : Collectible
{

    public string clip_name;
    private bool cooldown = false;

    override public void Init()
    {
        AudioManager.Instance.PlayOneshot(clip_name, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !cooldown)
        {
            cooldown = true;
            AudioManager.Instance.PlayOneshot(clip_name, transform.position);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}
