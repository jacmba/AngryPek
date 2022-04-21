using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game data", order = 0)]
public class GameData : ScriptableObject
{
  public int level;
  public int stars;
  public int totalStars;
  public int accStars;
  public int pieces;
  public int attempts;

  public void Clean()
  {
    level = 1;
    stars = 0;
    totalStars = 0;
    accStars = 0;
    pieces = 0;
    attempts = Constants.MAX_ATTEMPTS;
  }

  public bool FinishStage(int stars)
  {
    level++;
    pieces++;
    this.stars = stars;
    totalStars += stars;
    accStars += stars;

    if (accStars >= 5)
    {
      this.attempts++;
      accStars -= 5;
      return true;
    }

    return false;
  }
}
