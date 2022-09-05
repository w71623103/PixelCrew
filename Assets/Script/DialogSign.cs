using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSign : MonoBehaviour
{
    [SerializeField] private Image dialogBox;
    [SerializeField] private Text dialogContent;
    [SerializeField] private string text;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogContent.text = text;
        gameObject.transform.tag = "NPC";
        dialogBox.gameObject.SetActive(false);
    }

    public void DisplaySign()
    {
        if (dialogBox != null)
        {
            dialogBox.gameObject.SetActive(true);
        }
    }

    public void HideSign()
    {
        if (dialogBox != null)
        {
            dialogBox.gameObject.SetActive(false);
        }
    }
}
