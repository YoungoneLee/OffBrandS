using UnityEngine;
using UnityEngine.UI;


public class ButtonClick : MonoBehaviour
{
    [SerializeField] GameObject startCanvas;
    public void whenButtonClicked()
    {
        startCanvas.SetActive(false);
    }
}
