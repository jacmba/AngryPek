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
  public void startGame()
  {
    OnGameStart?.Invoke();
  }

  public void startDrag()
  {
    OnDragStart?.Invoke();
  }

  public void launchPek()
  {
    OnLaunchPek?.Invoke();
  }

  public void collectPizza()
  {
    OnPizzaCollected?.Invoke();
  }
}
