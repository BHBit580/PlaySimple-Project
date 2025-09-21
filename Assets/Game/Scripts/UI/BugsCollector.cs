using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BugsCollector : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO bugsCollectedEvent;
    [SerializeField] private TextMeshProUGUI bugsText;
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image bugImage;
    
    private int _collectedBugs;
    private int _totalBugsInLevel;
    private void Start()
    {
        bugsCollectedEvent.RegisterListener(OnBugCollected);
        UpdateBugDisplay();
        TryCheckIfLevelHasBugs();
    }
    
    private void TryCheckIfLevelHasBugs()
    {
        if (levelController.CurrentLevelData != null)
        {
            _totalBugsInLevel = CountBugsInLevel(levelController.CurrentLevelData);
            
            bool hasBugs = _totalBugsInLevel > 0;
            panel.SetActive(hasBugs);
        }
    }

    private int CountBugsInLevel(LevelData levelData)
    {
        if (levelData == null || levelData.gridData == null) return 0;
        
        int bugCount = 0;
        foreach (var tileData in levelData.gridData)
        {
            if (tileData.tileType == (int)TileType.Bug)
            {
                bugCount++;
            }
        }
        return bugCount;
    }

    private void OnBugCollected()
    {
        _collectedBugs++; 
        UpdateBugDisplay();
        AnimateBugCollection();
    }

    private void UpdateBugDisplay()
    {
        if (_totalBugsInLevel > 0)
        {
            bugsText.text = $"{_collectedBugs}/{_totalBugsInLevel}";
        }
        else
        {
            bugsText.text = $"{_collectedBugs} collected";
        }
    }

    private void AnimateBugCollection()
    {
        bugImage.transform.DOScale(1.2f, 0.15f).SetEase(Ease.OutBack).OnComplete(() => {
            bugImage.transform.DOScale(1f, 0.15f).SetEase(Ease.InBack);
        });
    }
    
    private void OnDestroy()
    {
        bugsCollectedEvent.UnregisterListener(OnBugCollected);
    }
}