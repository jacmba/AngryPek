using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameDataTests
{
  private GameData data;

  [SetUp]
  public void Setup()
  {
    data = Resources.Load<GameData>("Data/Game");
  }

  [TearDown]
  public void TearDown()
  {
    data.Clean();
  }

  [Test]
  public void GameDataShouldHaveSameAttemptsWhenFinishingWithOnePiece()
  {
    data.totalStars = 0;
    data.attempts = 3;
    Game game = new Game(0, 3);
    bool bonus = data.FinishStage(game);
    Assert.AreEqual(3, data.attempts);
    Assert.IsFalse(bonus);
  }

  [Test]
  public void GameDataShouldIncreaseAttemptsWhenAccumulatingFiveStars()
  {
    data.accStars = 2;
    data.attempts = 3;
    Game game = new Game(0, 3);
    bool bonus = data.FinishStage(game);
    Assert.AreEqual(4, data.attempts);
    Assert.AreEqual(0, data.accStars);
    Assert.IsTrue(bonus);
  }

  [Test]
  public void GameDataShouldIncreaseAttemptsAndHaveRemainingWhenAccumulatingSevenStars()
  {
    data.accStars = 4;
    data.attempts = 3;
    Game game = new Game(0, 3);
    bool bonus = data.FinishStage(game);
    Assert.AreEqual(4, data.attempts);
    Assert.AreEqual(2, data.accStars);
    Assert.IsTrue(bonus);
  }

  [Test]
  public void GameDataShouldResetLivesToMaxWhenFinishingBelowMax()
  {
    data.attempts = 6;
    Game game = new Game(0, Constants.MAX_ATTEMPTS - 1);
    data.FinishStage(game);
    Assert.AreEqual(Constants.MAX_ATTEMPTS, data.attempts);
  }

  [Test]
  public void GameDataShouldHaveMaxAttemptsAndOneMoreWhenFinishingAndGettingExtra()
  {
    data.attempts = 6;
    data.accStars = 3;
    Game game = new Game(0, Constants.MAX_ATTEMPTS - 1);
    data.FinishStage(game);
    Assert.AreEqual(Constants.MAX_ATTEMPTS + 1, data.attempts);
  }
}
