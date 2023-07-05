using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int activeCharIndex = 0;
    private List<PlayableCharacter> playableCharacters;

    // Enables the camera at the specified position and disable the rest
    void SwitchActiveCharacter (int index) {
        Debug.Log("Switch to character " + playableCharacters[index].gameObject.name);
        for (int i = 0; i < playableCharacters.Count; i++) {
            bool shouldBeActive = i == index;
            playableCharacters[i].isActiveCharacter = shouldBeActive;
            playableCharacters[i].assignedCamera.enabled = shouldBeActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        // Store list of characters found with tag "Player"
        playableCharacters = new List<PlayableCharacter>();
        foreach (GameObject playerObj in playerObjects) {
            PlayableCharacter character = playerObj.GetComponent<PlayableCharacter>();

            if (character) {
                playableCharacters.Add(character);
            } else {
                Debug.LogWarning("Object " + character.name + " is not a valid playable character!");
            }
        }

        // Enable first character by default
        SwitchActiveCharacter(activeCharIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // Switch currently active character when the user presses tab key
        if (Input.GetKeyDown(KeyCode.Tab)) {
            activeCharIndex += 1;
            if (activeCharIndex == playableCharacters.Count) {
                activeCharIndex = 0;
            }

            SwitchActiveCharacter(activeCharIndex);
        }
    }
}
