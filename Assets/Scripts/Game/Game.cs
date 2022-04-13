using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
  public int enemies { get; private set; }

  public Game(int enemies)
  {
    this.enemies = enemies;
  }

  public void KillEnemy()
  {
    this.enemies--;
  }

  public int Finish(int lives)
  {
    int stars = lives - enemies;
    return Mathf.Clamp(stars, 0, 3);
  }
}