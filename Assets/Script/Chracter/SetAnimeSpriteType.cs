using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimeSpriteType : MonoBehaviour
{
    public AnimatorOverrideController[] overrideControllers;
    public AnimatorOverrider overider;

    public void Set(int value)
    {
        overider.SetAnimations(overrideControllers[value]);
    }
}
