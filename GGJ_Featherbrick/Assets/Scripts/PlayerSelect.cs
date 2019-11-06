using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{

    const float COOLDOWN_TIME = 0.3f;

    class Player
    {
        public bool is_active = false;
        public int selected_character_id = 0;
        public bool is_character_selected = false;
        public Image characterImageHolder;
        public Text pressAText;
        public float cool_down_time = 0;
    }

    Player[] players = new Player[4] { new Player(), new Player() , new Player() , new Player()};


    public GameObject[] Player_Pointers;
    public GameObject[] Player_Press_A;

    public List<Sprite> Character_Art;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            players[i].characterImageHolder = Player_Pointers[i].GetComponent<Image>();
            players[i].pressAText = Player_Press_A[i].GetComponent<Text>();
        }
    }

    
    List<int> takenCharacters = new List<int>();


    Color testColours(int selected_character_id)
    {
        switch (selected_character_id)
        {
            case 0:
                return new Color(1, 0, 0, 1);

            case 1:
                return new Color(0, 1, 0, 1);
            case 2:
                return new Color(0, 0, 1, 1);
        }
        return new Color(1, 1, 0, 1);
    }
    // Update is called once per frame



    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if (players[i].is_character_selected)
            {
                if (Input.GetAxis("Player" + (i + 1) + "LT") > 0.3)
                {
                    players[i].characterImageHolder.color = new Color(1, 1, 1, 1);
                    players[i].is_character_selected = false;
                    takenCharacters.Remove(players[i].selected_character_id);
                }
            }
            else if (players[i].is_active)
            {

                if (takenCharacters.Contains(players[i].selected_character_id))
                {
                    players[i].characterImageHolder.color = new Color(0.0f, 0.0f, 0.0f, 1);
                }
                else
                {
                    players[i].characterImageHolder.color = new Color(1.0f, 1.0f, 1.0f, 1);
                }
                if (Input.GetAxis("Player"+ (i + 1) + "LH") > 0.3)
                {
                    if (players[i].cool_down_time > 0)
                    {
                        players[i].cool_down_time -= Time.deltaTime;                     
                    }
                    else 
                    {
                        players[i].cool_down_time = COOLDOWN_TIME;
                        players[i].selected_character_id++;
                       if (players[i].selected_character_id > (Character_Art.Count - 1))
                        {
                            players[i].selected_character_id = 0;
                        }
                        players[i].characterImageHolder.sprite = Character_Art[players[i].selected_character_id];
                        
                    }


                }
                else if (Input.GetAxis("Player" + (i + 1) + "LH") < -0.3)
                {
                    if (players[i].cool_down_time > 0)
                    {
                        players[i].cool_down_time -= Time.deltaTime;
                       
                    }
                    else
                    {
                        players[i].cool_down_time = COOLDOWN_TIME;
                        players[i].selected_character_id--;
                        if (players[i].selected_character_id < 0)
                        {
                            players[i].selected_character_id = (Character_Art.Count - 1);
                        }
                        players[i].characterImageHolder.sprite = Character_Art[players[i].selected_character_id];


                    }
                }
                else
                {
                    players[i].cool_down_time = 0;
                }

                if (Input.GetAxis("Player" + (i + 1) + "LT") > 0.3)
                {

                    players[i].characterImageHolder.color = new Color(1.0f, 1.0f, 1.0f, 0);
                    players[i].pressAText.text = "Press 'A' to Join!";
                    players[i].is_active = false;

                }
                else if (Input.GetButtonDown("Player" + (i + 1) + "A"))
                {
                    if (!takenCharacters.Contains(players[i].selected_character_id))
                    {
                        takenCharacters.Add(players[i].selected_character_id);
                        players[i].is_character_selected = true;
                        players[i].pressAText.text = "Ready!";
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Player" + (i + 1) + "A"))
                {
                    players[i].characterImageHolder.sprite = Character_Art[players[i].selected_character_id];
                    players[i].characterImageHolder.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                    players[i].pressAText.text = " ";
                    players[i].is_active = true;
                }
                else if (Input.GetAxis("Player" + (i + 1) + "LT") > 0.3)
                {
                    //exit game
                }
            }
        }

        Debug.Log("Player selected_character_id = " + players[0].selected_character_id);
    }
}
