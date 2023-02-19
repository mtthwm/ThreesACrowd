using UnityEngine;

public class Highlighter : MonoBehaviour
{

    [SerializeField] private Animator m_Animator;

    private void OnEnable()
    {
        ClickableObject.OnHoverEnter += HoverEnter;
        ClickableObject.OnHoverExit += HoverExit;
    }

    private void OnDisable()
    {
        ClickableObject.OnHoverEnter -= HoverEnter;
        ClickableObject.OnHoverExit -= HoverExit;
    }

    private void HoverEnter (GameObject origin, Vector3 position)
    {
        if (origin == this.gameObject)
        {
            Highlight(true);
        }
    }

    private void HoverExit(GameObject origin, Vector3 position)
    {
        if (origin == this.gameObject)
        {
            Highlight(false);
        }
    }

    private void Highlight (bool highlighted)
    {
        m_Animator.SetBool("HasFocus", highlighted);
    }
}
