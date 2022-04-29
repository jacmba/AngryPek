using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterAnimator : MonoBehaviour
{
  [SerializeField] private float speed = 0.5f;
  [SerializeField] private bool stars = true;
  private Text counterText;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    counterText = GetComponent<Text>();
    eventBus = EventBus.GetInstance();
  }

  IEnumerator AnimationThread(int from, int to)
  {
    counterText = GetComponent<Text>();
    for (int i = from; i <= to; i++)
    {
      counterText.text = i.ToString();
      yield return new WaitForSeconds(speed);
    }
    if (stars)
    {
      eventBus.AnimateStarsCounter();
    }
    else
    {
      eventBus.ShowCounter();
    }
  }

  public void Animate(int from, int to)
  {
    StartCoroutine(AnimationThread(from, to));
  }
}
