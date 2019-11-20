public class Football : Collectible
{
    override public void Init()
    {
        base.Init();
        audioManager.PlayOneshot("impact_deep_thud_bounce_01", transform.position);
    }
}
