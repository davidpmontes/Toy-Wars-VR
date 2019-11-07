using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : Collectible
{
    [SerializeField] private GameObject smoke_stack = default;
    [SerializeField] private GameObject smoke_prefab = default;
    [SerializeField] private float toot_time = default;
    [SerializeField] private float cooldown_time = default;
    private bool on_cooldown = false;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    override public void Shot()
    {
        StartCoroutine(Toot());
        base.Shot();
    }

    IEnumerator Toot()
    {
        if (!on_cooldown)
        {
            on_cooldown = true;
            Instantiate(smoke_prefab, smoke_stack.transform);
            audioManager.PlayOneshot("train_horn_02", smoke_stack.transform, 2.0f);
            yield return new WaitForSeconds(toot_time);
            Instantiate(smoke_prefab, smoke_stack.transform);
            audioManager.PlayOneshot("train_horn_02", smoke_stack.transform, 2.0f);
            yield return new WaitForSeconds(cooldown_time);
            on_cooldown = false;
        }
    }

}
