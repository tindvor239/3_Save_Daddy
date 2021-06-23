using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIChest : UIToggle
{
    [SerializeField]
    private Text prize;
    [SerializeField]
    private GameObject prizeBackground;
    [SerializeField]
    protected ParticleSystem coins;
    [SerializeField]
    protected ParticleSystem[] loopingEffects;
    private Animator animator;
    #region Properties
    public long Prize
    {
        get
        {
            try
            {
                string[] splitedString = prize.text.Split(' ');
                string result = splitedString[1];
                return long.Parse(result);
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            prize.text = string.Format("$ {0}", value);
        }
    }
    #endregion
    public void Open()
    {
        animator.SetTrigger("Open");
        StartCoroutine(Animate(1f));
    }
    public void Restart(int prize)
    {
        Prize = prize;
        isCheck = false;
        this.prize.color = new Color(this.prize.color.r, this.prize.color.g, this.prize.color.b, 1);
        foreach (ParticleSystem particle in loopingEffects)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
        prizeBackground.SetActive(false);
    }

    protected IEnumerator Animate(float duration)
    {
        yield return new WaitForSeconds(duration);
        coins.Play();
        foreach(ParticleSystem particle in loopingEffects)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
        if (GameManager.CurrentKey > 0)
        {
            GameManager.CurrentKey--;
            UIChestRoom.TopPrize += Prize;
            GameManager.Money += Prize;
            
            Sequence sequence = DOTween.Sequence();
            Vector2 destination = new Vector2(transform.position.x, transform.position.y + 5);
            Color targetColor = new Color(prize.color.r, prize.color.g, prize.color.b, 0);
            ViewManager.Instance.MoveOutput(sequence, prize.transform, destination, 2f);
            ViewManager.JoinAction(sequence, ViewManager.DoColor(prize, targetColor, 3f));
            
            isCheck = true;
            prizeBackground.SetActive(true);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        prizeBackground.SetActive(false);
        foreach (ParticleSystem particle in loopingEffects)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }
}
