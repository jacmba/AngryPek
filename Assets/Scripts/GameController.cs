using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
  // Event bus stuff
  public static event Action OnGameStart;
  public static event Action OnDragStart;
  public static event Action OnLaunchPek;

  // Start is called before the first frame update
  void Start()
  {
    // Todo initialize stuff
  }

  // Update is called once per frame
  void Update()
  {
    // Todo update stuff
  }

  // Event bus invoke methods
  public static void startGame()
  {
    OnGameStart?.Invoke();
  }

  public static void startDrag()
  {
    OnDragStart?.Invoke();
  }

  public static void launchPek()
  {
    OnLaunchPek?.Invoke();
  }
}
