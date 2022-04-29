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
  public int newAttempts;

  public void Clean()
  {
    level = 1;
    stars = 0;
    totalStars = 0;
    accStars = 0;
    pieces = 0;
    newAttempts = 0;
    attempts = Constants.MAX_ATTEMPTS;
  }

  public bool FinishStage(Game game)
  {
    stars = game.Finish();
    level++;
    pieces++;
    totalStars += stars;
    accStars += stars;
    newAttempts = 0;

    if (game.attempts < Constants.MAX_ATTEMPTS)
    {
      attempts = Constants.MAX_ATTEMPTS;
    }

    if (accStars >= 5)
    {
      this.attempts++;
      newAttempts++;
      accStars -= 5;
      return true;
    }

    return false;
  }

  public void IncreaseAttempts()
  {
    attempts++;
    newAttempts++;
  }
}
