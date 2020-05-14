using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour {

    private Player player;

    public Text Bought1;
    public Text Bought2;
    public Text Bought3;
    public Text Bought4;
    public Text Bought5;
    public Text Bought6;
    public Text Bought7;
    public Text Bought8;
    public Text Bought9;

    //för ljud
    private int counter = 0;
    public Sprite SoundOn;
    public Sprite SoundOff;
    public Button myButton;
    public AudioSource audio;

    private void Awake()
    {
        //visa ad
       // UnityAdsScript.instance.ShowBannerAd();

        player = GameObject.FindObjectOfType<Player>();
        PlayerPrefs.SetInt("InGame", 0);

        Bought2.text = "Buy 150";
        Bought3.text = "Buy 250";
        Bought4.text = "Buy 400";
        Bought5.text = "Buy 900";
        Bought6.text = "Buy 1100";
        Bought7.text = "Buy 1900";
        Bought8.text = "Buy 2400";
        Bought9.text = "Buy 3000";


        if (PlayerPrefs.GetString("Gun2") == "Gun2")
        {
            Bought2.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun3") == "Gun3")
        {
            Bought3.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun4") == "Gun4")
        {
            Bought4.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun5") == "Gun5")
        {
            Bought5.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun6") == "Gun6")
        {
            Bought6.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun7") == "Gun7")
        {
            Bought7.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun8") == "Gun8")
        {
            Bought8.text = "Equip";
        }
        if (PlayerPrefs.GetString("Gun9") == "Gun9")
        {
            Bought9.text = "Equip";
        }

        // valt vapen har annan färg på knapp
        if(PlayerPrefs.GetInt("ChosenGun") == 0)
        {
            Bought1.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 1)
        {
            Bought2.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 2)
        {
            Bought3.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 3)
        {
            Bought4.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 4)
        {
            Bought5.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 5)
        {
            Bought6.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 6)
        {
            Bought7.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 7)
        {
            Bought8.color = new Color(0f, 1f, 1f);
        }
        if (PlayerPrefs.GetInt("ChosenGun") == 8)
        {
            Bought9.color = new Color(0f, 1f, 1f);
        }

      
        
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            myButton.image.overrideSprite = SoundOn;
            
        }
        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            myButton.image.overrideSprite = SoundOff;
            
        }
    }

    public void LoadLevel()
    {
        Application.LoadLevel(1);
        //vi countar att vi bytt scene och förstör ej game object.
        Counter count = GameObject.Find("SceneCounter").GetComponent<Counter>();
        int curr_count = count.NumberOfSceneChanges();
        GameObject countObj = GameObject.Find("SceneCounter");
        DontDestroyOnLoad(countObj); //förstör ej vid load
        GameObject adObj = GameObject.Find("AdManager"); //destroy on load
        Destroy(adObj);
        

    }
	
    public void Gun1()
    {
        PlayerPrefs.SetInt("ChosenGun", 0);
        PlayerPrefs.SetFloat("FireRate", 0.75f);

        Bought1.color = new Color(0f, 1f, 1f);
        Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);


    }
    
    public void Gun2()
    {
        int Gun2_cost = 150;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        // om man väljer vapnet och man redan har det
        if(PlayerPrefs.GetString("Gun2") == "Gun2")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 1);
            PlayerPrefs.SetFloat("FireRate", 0.75f);

            Bought2.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }

        // om man inte har vapnet men köper det!
        if (TotCoin >= Gun2_cost && PlayerPrefs.GetString("Gun2") != "Gun2")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 1);
            PlayerPrefs.SetFloat("FireRate", 0.75f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun2_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun2", "Gun2");
            Bought2.text = "Equip";
            Bought2.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }

     
    }

    public void Gun3()
    {
        int Gun3_cost = 250;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun3") == "Gun3")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 2);
            PlayerPrefs.SetFloat("FireRate", 0.625f);

            Bought3.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun3_cost && PlayerPrefs.GetString("Gun3") != "Gun3")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 2);
            PlayerPrefs.SetFloat("FireRate", 0.625f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun3_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun3", "Gun3");
            Bought3.text = "Equip";
            Bought3.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }

    }

    public void Gun4()
    {
        int Gun4_cost = 400;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun4") == "Gun4")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 3);
            PlayerPrefs.SetFloat("FireRate", 0.625f);

            Bought4.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun4_cost && PlayerPrefs.GetString("Gun4") != "Gun4")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 3);
            PlayerPrefs.SetFloat("FireRate", 0.625f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun4_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun4", "Gun4");
            Bought4.text = "Equip";
            Bought4.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
    }

    public void Gun5()
    {
        int Gun5_cost = 900;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun5") == "Gun5")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 4);
            PlayerPrefs.SetFloat("FireRate", 0.5f);

            Bought5.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun5_cost && PlayerPrefs.GetString("Gun5") != "Gun5")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 4);
            PlayerPrefs.SetFloat("FireRate", 0.5f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun5_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun5", "Gun5");
            Bought5.text = "Equip";
            Bought5.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }

    }

    public void Gun6()
    {
        int Gun6_cost = 1100;

        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun6") == "Gun6")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 5);
            PlayerPrefs.SetFloat("FireRate", 0.5f);

            Bought6.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun6_cost && PlayerPrefs.GetString("Gun6") != "Gun6")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 5);
            PlayerPrefs.SetFloat("FireRate", 0.5f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun6_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun6", "Gun6");
            Bought6.text = "Equip";
            Bought6.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
    }

    public void Gun7()
    {
        int Gun7_cost = 1900;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun7") == "Gun7")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 6);
            PlayerPrefs.SetFloat("FireRate", 0.375f);

            Bought7.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun7_cost && PlayerPrefs.GetString("Gun7") != "Gun7")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 6);
            PlayerPrefs.SetFloat("FireRate", 0.375f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun7_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun7", "Gun7");
            Bought7.text = "Equip";
            Bought7.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
    }

    public void Gun8()
    {
        int Gun8_cost = 2400;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun8") == "Gun8")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 7);
            PlayerPrefs.SetFloat("FireRate", 0.375f);
            Bought8.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun8_cost && PlayerPrefs.GetString("Gun8") != "Gun8")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 7);
            PlayerPrefs.SetFloat("FireRate", 0.375f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun8_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun8", "Gun8");
            Bought8.text = "Equip";
            Bought8.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought9.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
    }

    public void Gun9()
    {
        int Gun9_cost = 3000;
        int TotCoin = PlayerPrefs.GetInt("TotalCoin");

        if (PlayerPrefs.GetString("Gun9") == "Gun9")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 8);
            PlayerPrefs.SetFloat("FireRate", 0.23f);

            Bought9.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        if (TotCoin >= Gun9_cost && PlayerPrefs.GetString("Gun9") != "Gun9")
        {
            //sätter vapnet på vår spelare
            PlayerPrefs.SetInt("ChosenGun", 8);
            PlayerPrefs.SetFloat("FireRate", 0.23f);

            //drar bort pengar 
            PlayerPrefs.SetInt("TotalCoin", TotCoin - Gun9_cost);

            //sätter att vi har vapnet
            PlayerPrefs.SetString("Gun9", "Gun9");
            Bought9.text = "Equip";
            Bought9.color = new Color(0f, 1f, 1f);

            Bought9.color = new Color(0f, 1f, 1f);
            Bought1.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought2.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought3.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought4.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought5.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought6.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought7.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
            Bought8.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
    }

    //ljudknapp
    public void SoundOnOff()
    {
        counter++;
        if (counter % 2 == 0)
        {
            myButton.image.overrideSprite = SoundOff;
            audio.Pause();
            PlayerPrefs.SetInt("SoundOn", 1);
        }
        else
        {
            myButton.image.overrideSprite = SoundOn;
            audio.UnPause();
            PlayerPrefs.SetInt("SoundOn", 0);
        }
    }


}
