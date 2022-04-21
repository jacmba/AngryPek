using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
  public int enemies { get; private set; }
  public int attempts { get; private set; }
  public bool hasPizza { get; private set; }

  public Game(int enemies, int attempts)
  {
    this.enemies = enemies;
    this.attempts = attempts;
    hasPizza = false;
  }

  public void KillEnemy()
  {
    enemies--;
  }

  public bool KillPek()
  {
    attempts--;
    if (attempts <= 0)
    {
      attempts = 0;
      return true;
    }

    return false;
  }

  public int Finish()
  {
    int stars = attempts - enemies;
    return Mathf.Clamp(stars, 0, 3);
  }

  public void CollectPizza()
  {
    hasPizza = true;
  }
}
