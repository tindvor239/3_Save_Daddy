using UnityEngine;
using DG.Tweening;

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
            if(Mathf.Round(pinLocks[i].rotation.x) == Mathf.Round(pinBase.transform.rotation.x) &&
                Mathf.Round(pinLocks[i].rotation.y) == Mathf.Round(pinBase.transform.rotation.y) &&
                Mathf.Round(pinLocks[i].rotation.z) == Mathf.Round(pinBase.transform.rotation.z))
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
