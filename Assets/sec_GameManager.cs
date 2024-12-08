using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sec_GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public src_MainCharacter MainCharacter;
    public GameObject menuGameOver;
    public GameObject menuGameplay;
    public GameObject Pausa;
    public GameObject Corazon1;
    public GameObject Corazon2;
    public GameObject Corazon3;
    public CinemachineVirtualCamera VirtualCamera;
    private bool isPaused = false;
    public AudioSource nivelMusica;

    void Start()
    {
        menuGameOver.SetActive(false);
        menuGameplay.SetActive(true);
        Pausa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (MainCharacter.Lives == 3)
        {
            Corazon1.SetActive(true);
            Corazon2.SetActive(true);
            Corazon3.SetActive(true);
        }

        if (MainCharacter.Lives == 2)
        {
            Corazon1.SetActive(true);
            Corazon2.SetActive(true);
            Corazon3.SetActive(false);
        }

        if (MainCharacter.Lives == 1)
        {
            Corazon1.SetActive(true);
            Corazon2.SetActive(false);
            Corazon3.SetActive(false);
        }

        if (MainCharacter.Lives == 0)
        {
            Corazon1.SetActive(false);
            Corazon2.SetActive(false);
            Corazon3.SetActive(false);
        }

        if (MainCharacter.Lives == 0 && !menuGameOver.activeSelf)
        {
            menuGameOver.SetActive(true);
            menuGameplay.SetActive(false);

            MainCharacter.Jump();
            MainCharacter.GetComponent<Collider2D>().enabled = false;

            VirtualCamera.Follow = null;
            VirtualCamera.LookAt = null;
        }
    }


    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Pausa el tiempo
        menuGameOver.SetActive(false);
        menuGameplay.SetActive(false);
        Pausa.SetActive(true);
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Reanuda el tiempo
        Pausa.SetActive(false);
        menuGameplay.SetActive(true);
    }

}
