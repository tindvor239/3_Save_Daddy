using UnityEngine;
using System;

public class RotatePin : Pin
{
    [SerializeField]
    private Transform[] pinLocks = new Transform[2];
    protected override void Unpin()
    {
        if (isAlreadyUnpin == false)
        {
            base.Unpin();
            Quaternion rotate = FlipFlop();
            GameController.Instance.Rotate(pinTransform.transform, in rotate, unpinDuration);
        }
    }
    private Quaternion FlipFlop()
    {
        for(int i = 0; i < pinLocks.Length; i++)
        {
            if(Math.Round(pinLocks[i].rotation.x, 2) == Math.Round(pinTransform.transform.rotation.x, 2) &&
                Math.Round(pinLocks[i].rotation.y, 2) == Math.Round(pinTransform.transform.rotation.y, 2) &&
                Math.Round(pinLocks[i].rotation.z, 2) == Math.Round(pinTransform.transform.rotation.z, 2))
            {
                if(i == pinLocks.Length - 1)
                {
                    return pinLocks[i - 1].rotation;
                }
                else
                {
                    return pinLocks[i + 1].rotation;
                }
            }
        }
        return new Quaternion();
    }
}
