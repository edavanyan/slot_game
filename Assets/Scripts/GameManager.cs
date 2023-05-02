using Data;
using Gestures;
using Scene;
using Slot;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GridController gridController;
    public SlotController slotController;
    public UserDataController userDataController;
    public GestureDetector gestureDetector; 

    private void Start()
    {
        instance = this;
    }
}
