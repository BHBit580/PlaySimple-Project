using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class LetterTile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [SerializeField] private Image rockImage;
    [SerializeField] private Image bugImage;
  
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private float fadeOutDuration = 0.3f;
    
    public Vector2Int Coordinate { get; private set; }
    public char Letter { get; private set; }
    public bool IsSelected { get; private set; }
    public TileType CurrentTileType { get; private set; }
    public bool IsBlocked => CurrentTileType == TileType.Rock;
    
    public void SetTileType(TileType tileType)
    {
        CurrentTileType = tileType;
        UpdateVisuals();
    }
    
    public void SetPosition(int p, int n)
    {
        Coordinate = new Vector2Int(p, n);
    }
    
    public void SetLetter(char letter)
    {
        Letter = letter;
        letterText.text = Letter.ToString();
    }
    
    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        backgroundImage.color = selected ? selectedColor : normalColor;
    }
    
    public void FadeOutAndDestroyItself()
    {
        // Disable interaction immediately
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        
        // Fade out
        canvasGroup.DOFade(0, fadeOutDuration)
            .OnComplete(() => {
                Destroy(gameObject);
            });
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // Only start selection if we're not already selecting
        if (!TileSelectionController.Instance.IsDragging())
        {
            TileSelectionController.Instance.StartSelection(this);
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Only continue selection if we're dragging AND mouse/touch is pressed
        if (TileSelectionController.Instance.IsDragging() && Input.GetMouseButton(0))
        {
            TileSelectionController.Instance.ContinueSelection(this);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        TileSelectionController.Instance.EndSelection();
    }
    
    private void UpdateVisuals()
    {
        rockImage.gameObject.SetActive(false);
        bugImage.gameObject.SetActive(false);
        letterText.gameObject.SetActive(true); 

        switch (CurrentTileType)
        {
            case TileType.Normal:
                break;
            case TileType.Rock:
                rockImage.gameObject.SetActive(true);
                letterText.gameObject.SetActive(false); 
                break;
            case TileType.Bug:
                bugImage.gameObject.SetActive(true);
                break;
        }
    }
}