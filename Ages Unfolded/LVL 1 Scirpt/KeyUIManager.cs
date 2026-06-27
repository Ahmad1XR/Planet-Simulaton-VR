using UnityEngine;
using UnityEngine.UI;

public class KeyUIManager : MonoBehaviour
{

    public static KeyUIManager instance;

    [SerializeField] private Image keyIcon;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        keyIcon.enabled = false;
    }

    public void ShowKeyIcon()
    {
        keyIcon.enabled = true;
    }

    public void HideKeyIcon()
    {
        keyIcon.enabled = false;
    }

    public bool IsKeyIconVisible()
    {
        return keyIcon != null && keyIcon.enabled;
    }
}
