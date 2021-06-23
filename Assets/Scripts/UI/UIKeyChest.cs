using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIKeyChest : UIToggle
{
    [SerializeField]
    private ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void Open()
    {
        Shaking();
        StartCoroutine(OnOpen());
    }

    private IEnumerator OnOpen()
    {
        yield return new WaitForSeconds(0.81f);
        Sequence sequence = DOTween.Sequence();
        ViewManager.Scale(transform, new Vector3(1.2f, 1.2f, 1f), 0.5f, sequence);
        StartCoroutine(GetKey());
        ViewManager.Scale(transform, new Vector3(1f, 1f, 1f), 0.5f, sequence);
    }
    private IEnumerator GetKey()
    {
        yield return new WaitForSeconds(0.5f);
        // -1 key to stack.
        isCheck = true;
        effect.Play();
        gameObject.GetComponent<UIAnimation>().Stop();
    }
    private void Shaking()
    {
        Sequence sequence = DOTween.Sequence();
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, -20f), 0.2f);
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, 20f), 0.2f);
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, -15f), 0.1f);
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, 15f), 0.1f);
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, -10f), 0.05f);
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, 10f), 0.05f);
        ViewManager.Instance.Rotating(sequence, transform, new Vector3(0, 0, 0f), 0.01f);
    }
}
