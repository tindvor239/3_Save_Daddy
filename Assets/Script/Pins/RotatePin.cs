using UnityEngine;
using DG.Tweening;

public class RotatePin : Pin
{
    [SerializeField]
    private  GameObject pinBase;
    [SerializeField]
    private Transform[] pinLocks = new Transform[2];
    public override void Unpin()
    {
        if (isAlreadyUnpin == false)
        {
            base.Unpin();
            Quaternion rotate = FlipFlop();
            GameController.Rotate(pinBase.transform, in rotate, unpinDuration);
        }
    }
    private Quaternion FlipFlop()
    {
        for(int i = 0; i < pinLocks.Length; i++)
        {
            if(pinLocks[i].rotation == pinBase.transform.rotation)
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
