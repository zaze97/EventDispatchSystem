using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        EventCenter.AddListener<string>(EventType.ShowText, Show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Show(string str)
    {
        gameObject.GetComponent<Text>().text = str;
        gameObject.SetActive(true);
    }
}
