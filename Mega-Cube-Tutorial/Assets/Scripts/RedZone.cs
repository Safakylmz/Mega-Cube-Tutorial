using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedZone : MonoBehaviour
{
    public GameObject gameOverPanel;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        Cube cube = other.GetComponent<Cube>();
        if (cube != null)
        {
            if(!cube.isMainCube && cube.cubeRigidbody.velocity.magnitude < 0.1f)
            {
                Debug.Log("Game Over");
                gameOverPanel.SetActive(true);
                GameObject.Find("Touch Slider").GetComponent<TouchSlider>().enabled = false; // touch slider scripti deaktif oluyor.
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
