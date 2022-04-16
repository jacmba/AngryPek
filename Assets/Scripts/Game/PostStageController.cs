using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PostStageController : MonoBehaviour
{
  [SerializeField] private GameObject rootStar;
  [SerializeField] private GameObject pizzaBox;

  public static event Action OnPizzaShow;

  // Start is called before the first frame update
  void Start()
  {
    OnPizzaShow += onPizzaShow;
    StartCoroutine(ShowStars());
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    OnPizzaShow -= onPizzaShow;
  }

  // Update is called once per frame
  void Update()
  {
  }

  IEnumerator ShowStars()
  {
    yield return new WaitForSeconds(1);
    rootStar.SetActive(true);
  }

  void onPizzaShow()
  {
    pizzaBox.SetActive(true);
  }

  public static void ShowPizza()
  {
    OnPizzaShow?.Invoke();
  }
}
