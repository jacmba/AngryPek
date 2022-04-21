using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventBus
{
  private static EventBus instance = null;

  public event Action OnGameStart;
  public event Action OnDragStart;
  public event Action OnLaunchPek;
  public event Action OnPizzaCollected;
  public event Action OnPizzaShow;
  public event Action OnAdComplete;
  public event Action OnGameRendered;
  public event Action OnFadeOutStarted;
  public event Action OnFadeOutDone;
  public event Action OnExitToMenu;

  private EventBus() { }

  public static EventBus GetInstance()
  {
    if (instance == null)
    {
      instance = new EventBus();
    }

    return instance;
  }

  // Event bus invoke methods
  public void StartGame()
  {
    OnGameStart?.Invoke();
  }

  public void StartDrag()
  {
    OnDragStart?.Invoke();
  }

  public void LaunchPek()
  {
    OnLaunchPek?.Invoke();
  }

  public void CollectPizza()
  {
    OnPizzaCollected?.Invoke();
  }

  public void ShowPizza()
  {
    OnPizzaShow?.Invoke();
  }

  public void CompleteAd()
  {
    OnAdComplete?.Invoke();
  }

  public void RenderGame()
  {
    OnGameRendered?.Invoke();
  }

  public void StartFadeOut()
  {
    OnFadeOutStarted?.Invoke();
  }

  public void DoneFadeOut()
  {
    OnFadeOutDone?.Invoke();
  }

  public void ExitToMenu()
  {
    OnExitToMenu?.Invoke();
  }
}
