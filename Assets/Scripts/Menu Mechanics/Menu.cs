using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    private Animator _animator;
    private CanvasGroup _canvasGroup;
    private bool _showingMenu = false;

    public bool IsOpen
    {
        get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }
    }

    public void Awake ()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();

        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0,0);
    }

    public void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
            toggleSpriteVisibility(false);
        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
            toggleSpriteVisibility(true);
        }
    }

    private void toggleSpriteVisibility(bool visibility)
    {
        foreach (Renderer childRenderer in gameObject.GetComponentsInChildren<Renderer>())
            childRenderer.enabled = visibility;
        _showingMenu = visibility;
    }
}
