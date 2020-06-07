using System.Collections;
using UnityEngine;

public class Train : Collectible
{
    [SerializeField] private GameObject smoke_stack = default;
    [SerializeField] private GameObject smoke_prefab = default;
    [SerializeField] private float toot_time = default;
    [SerializeField] private float cooldown_time = default;
    private bool on_cooldown = false;

    override public void Init()
    {
        StartCoroutine(Toot());
        base.Init();
    }

    IEnumerator Toot()
    {
        if (!on_cooldown)
        {
            on_cooldown = true;
            Instantiate(smoke_prefab, smoke_stack.transform);
            AudioManager.Instance.PlayOneshot("train_horn_02", smoke_stack.transform, 2.0f);
            yield return new WaitForSeconds(toot_time);
            Instantiate(smoke_prefab, smoke_stack.transform);
            AudioManager.Instance.PlayOneshot("train_horn_02", smoke_stack.transform, 2.0f);
            yield return new WaitForSeconds(cooldown_time);
            on_cooldown = false;
        }
    }

}
