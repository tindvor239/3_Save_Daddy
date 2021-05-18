using UnityEngine;
using System;

public class RotatePin : Pin
{
    [SerializeField]
    private  GameObject pinBase;
    [SerializeField]
    private Transform[] pinLocks = new Transform[2];
    protected override void Unpin()
    {
        if (isAlreadyUnpin == false)
        {
            base.Unpin();
            Quaternion rotate = FlipFlop();
            GameController.Instance.Rotate(pinBase.transform, in rotate, unpinDuration);
        }
    }
    private Quaternion FlipFlop()
    {
        for(int i = 0; i < pinLocks.Length; i++)
        {
            if(Math.Round(pinLocks[i].rotation.x, 2) == Math.Round(pinBase.transform.rotation.x, 2) &&
                Math.Round(pinLocks[i].rotation.y, 2) == Math.Round(pinBase.transform.rotation.y, 2) &&
                Math.Round(pinLocks[i].rotation.z, 2) == Math.Round(pinBase.transform.rotation.z, 2))
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
