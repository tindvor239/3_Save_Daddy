using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotatePinTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject hand;
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private float speed;
    private void OnEnable()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        Vector3[] rotations = new Vector3[] { new Vector3(0, 0, 110), new Vector3(0, 0, 117)
            , new Vector3(0, 0, 125), new Vector3(0, 0, 130)
            , new Vector3(0, 0, 135), new Vector3(0, 0, 140)
            , new Vector3(0, 0, 145), new Vector3(0, 0, 150)
            , new Vector3(0, 0, 155) };
        lineRenderer.GetPositions(positions);
        Sequence sequence = DOTween.Sequence();
        for(int i = 0; i < positions.Length; i++)
        {
            float duration = 0;
            if (i == 0)
            {
                duration = Controller.MoveDuration(hand.transform.localPosition, positions[i], speed) -0.1f;
            }
            else
            {
                duration = Controller.MoveDuration(positions[i - 1], positions[i], speed) - 0.15f;
            }
            sequence.Append(hand.transform.DOLocalMove(positions[i], duration)).SetEase(Ease.Linear).
                Join(hand.transform.DORotate(rotations[i], duration));
        }
        sequence.SetLoops(-1);
    }
}
