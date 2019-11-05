using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{

    class Player
    {
        public bool is_active = false;
        public int selected_character_id = 0;
        public bool is_character_selected = false;
        public Image Player_pointer;
    }

    Player[] players = new Player[4];

    public Image[] Player_Pointers;
    // Start is called before the first frame update
    void Start()
    {
    }

    
    List<int> availableCharacters;


    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            if (players[i].is_active)
            {
                if (Input.GetButtonDown("Player" + i + "A"))
                {
                    players[i].Player_pointer.enabled = false;
                    players[i].is_active = true;
                }
            }
            else
            {
                if (Input.GetAxis("Player" + i + "LT") > 0.3)
                {

                    players[i].is_active = false;

                    players[i].Player_pointer.enabled = false;
                }
            }
        }


    }
}
