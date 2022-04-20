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
    bool bonus = data.FinishStage(3);
    Assert.AreEqual(3, data.attempts);
    Assert.IsFalse(bonus);
  }

  [Test]
  public void GameDataShouldIncreaseAttemptsWhenAccumulatingFiveStars()
  {
    data.accStars = 2;
    data.attempts = 3;
    bool bonus = data.FinishStage(3);
    Assert.AreEqual(4, data.attempts);
    Assert.AreEqual(0, data.accStars);
    Assert.IsTrue(bonus);
  }

  [Test]
  public void GameDataShouldIncreaseAttemptsAndHaveRemainingWhenAccumulatingSevenStars()
  {
    data.accStars = 4;
    data.attempts = 3;
    bool bonus = data.FinishStage(3);
    Assert.AreEqual(4, data.attempts);
    Assert.AreEqual(2, data.accStars);
    Assert.IsTrue(bonus);
  }
}
