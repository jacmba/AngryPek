using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
  private const string ANDROID_ID = "4706337";
  private const string PLACEMENT_ID = "Interstitial_Android";
  private EventBus eventBus;
  [SerializeField] private GameData data;

  // Start is called before the first frame update
  void Start()
  {
    eventBus = EventBus.GetInstance();
    if (!Advertisement.isInitialized)
    {
      InitializeAds();
    }
    else
    {
      LoadAd();
    }
  }

  public void InitializeAds()
  {
    Advertisement.Initialize(ANDROID_ID, false, this);
  }

  public void LoadAd()
  {
    Advertisement.Load(PLACEMENT_ID, this);
  }

  public void ShowAd()
  {
    Advertisement.Show(PLACEMENT_ID, this);
  }

  public void OnInitializationComplete()
  {
    Debug.Log("Ads successfully initialized");
    LoadAd();
  }

  public void OnInitializationFailed(UnityAdsInitializationError err, string message)
  {
    Debug.Log($"Error initializing Ads: {err.ToString()} - {message}");
    eventBus.CompleteAd();
  }

  public void OnUnityAdsAdLoaded(string id)
  {
    Debug.Log($"Add successfully loaded with id {id}");
    ShowAd();
  }

  public void OnUnityAdsFailedToLoad(string id, UnityAdsLoadError err, string message)
  {
    Debug.Log($"Failed to load ad: {id} - {err.ToString()} - {message}");
    eventBus.CompleteAd();
  }

  public void OnUnityAdsShowFailure(string id, UnityAdsShowError err, string message)
  {
    Debug.Log($"Error showing ad: {id} - {err.ToString()} - {message}");
    eventBus.CompleteAd();
  }

  public void OnUnityAdsShowStart(string id)
  {
    Debug.Log($"Started showing ad {id}");
  }

  public void OnUnityAdsShowClick(string id)
  {
    Debug.Log($"Ad click {id}");
  }

  public void OnUnityAdsShowComplete(string id, UnityAdsShowCompletionState state)
  {
    Debug.Log($"Ad show complete: {id} - {state.ToString()}");
    if (state == UnityAdsShowCompletionState.COMPLETED)
    {
      data.IncreaseAttempts();
    }
    eventBus.CompleteAd();
  }
}
