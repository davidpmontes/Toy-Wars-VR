using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    void GetSoundEffects(out AudioClip[] fx);
    void UpdateState();
}
