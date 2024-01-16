using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public string scene;
    public int maxhealth = 20;
    public Slider healthbar;
    public TextMeshProUGUI health;
    public static int hp;
    void Start()
    {

        hp = 50;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            hp -= 10;
        }
        healthbar.maxValue = maxhealth;
        healthbar.value = hp;
        health.text = hp.ToString() + " / " + maxhealth.ToString();
        if (hp == 0)
        {
            SceneManager.LoadScene(sceneName: SceneManager.GetActiveScene().name);
        }
    }
}

