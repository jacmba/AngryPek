using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
  [SerializeField] private CounterAnimator starsCounter;
  [SerializeField] private GameObject attemptsCounter;
  [SerializeField] private GameObject attemptsUI;
  [SerializeField] private GameData data;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    Text starsText = starsCounter.GetComponent<Text>();
    starsText.text = (data.totalStars - data.stars).ToString();

    eventBus = EventBus.GetInstance();
    eventBus.OnStarsCounterAnimated += OnStarsCounterAnimated;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventBus.OnStarsCounterAnimated -= OnStarsCounterAnimated;
  }

  public void OnPostAnimated()
  {
    starsCounter.Animate(data.totalStars - data.stars, data.totalStars);
  }

  public void OnStarsCounterAnimated()
  {
    if (data.newAttempts > 0)
    {
      attemptsUI.SetActive(true);
      Text attemptsText = attemptsCounter.GetComponent<Text>();
      CounterAnimator attemptsAnim = attemptsCounter.GetComponent<CounterAnimator>();
      attemptsText.text = (data.attempts - data.newAttempts).ToString();
      attemptsAnim.Animate(data.attempts - data.newAttempts, data.attempts);
    }
    else
    {
      eventBus.ShowCounter();
    }
  }
}
