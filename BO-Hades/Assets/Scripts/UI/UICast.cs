using TMPro;
using UnityEngine;

public class UICast : MonoBehaviour
{

    public TextMeshProUGUI castText;
    public int castAmmo = 1;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            castAmmo = 0;
        }
        castText.text = castAmmo.ToString() + " / 1";
    }

}
