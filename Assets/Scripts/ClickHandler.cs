using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        PlayerPress();
    }

    void OnMouseUp()
    {
        SetUnPressed();
    }
    
    // For patternts
    public void SetPressed()
    {
        Press();
        Invoke("SetUnPressed", 0.5f);

    }
    
    // Main method
    void Press()
    {
        animator.SetBool("Pressed", true);
        audioSource.Play();
    }

    // For player pressing
    void PlayerPress()
    {
        Press();
        GameManager.lastHit = gameObject;
        GameManager.hitCount++;
    }

    public void SetUnPressed()
    {
        animator.SetBool("Pressed", false);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
