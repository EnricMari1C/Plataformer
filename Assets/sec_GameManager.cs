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
    public GameObject Corazon1;
    public GameObject Corazon2;
    public GameObject Corazon3;
    public CinemachineVirtualCamera VirtualCamera;

    void Start()
    {
        menuGameOver.SetActive(false);
        menuGameplay.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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
}
