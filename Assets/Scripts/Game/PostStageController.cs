using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostStageController : MonoBehaviour
{
  [SerializeField] private GameObject rootStar;
  [SerializeField] private GameObject pizzaBox;
  [SerializeField] private GameObject continueText;
  [SerializeField] private GameData data;

  private bool canContinue;
  private bool canTouch;
  private AdsController adsController;
  private EventBus eventBus;

  // Start is called before the first frame update
  void Start()
  {
    canContinue = false;
    canTouch = false;

    if (data.pieces < 1)
    {
      data.pieces = 1;
    }
    adsController = GetComponent<AdsController>();

    eventBus = EventBus.GetInstance();
    eventBus.OnPizzaShow += onPizzaShow;
    eventBus.OnAdComplete += onAdComplete;

    StartCoroutine(ShowStars());
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventBus.OnPizzaShow -= onPizzaShow;
    eventBus.OnAdComplete -= onAdComplete;
  }

  // Update is called once per frame
  void Update()
  {
    if (canContinue && Input.GetMouseButtonDown(0) && canTouch)
    {
      Debug.Log("Continue");
      int nextLevel = data.level <= Constants.MAX_LEVEL ? data.level : 0;
      if (nextLevel == 0)
      {
        data.Clean();
      }
      SceneManager.LoadScene(nextLevel);
    }

    if (!Input.GetMouseButton(0))
    {
      canTouch = true;
    }
  }

  IEnumerator ShowStars()
  {
    yield return new WaitForSeconds(1);
    rootStar.SetActive(true);
  }

  void onPizzaShow()
  {
    StartCoroutine(pizzaShow());
  }

  void onAdComplete()
  {
    Debug.Log("Ad Complete!!");
    canTouch = false;
    ShowContinue();
  }

  IEnumerator pizzaShow()
  {
    yield return new WaitForSeconds(1f);
    pizzaBox.SetActive(true);

    yield return new WaitForSeconds(3f);
    if ((Application.isEditor || SystemInfo.deviceType == DeviceType.Handheld) && UnityEngine.Random.Range(1, 4) == 2)
    {
      adsController.enabled = true;
    }
    else
    {
      ShowContinue();
    }
  }

  void ShowContinue()
  {
    continueText.SetActive(true);
    canContinue = true;
  }
}
