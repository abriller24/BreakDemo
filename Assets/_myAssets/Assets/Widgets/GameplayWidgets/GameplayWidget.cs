using UnityEngine;

public class GameplayWidget : MonoBehaviour
{
    [SerializeField] private Joystick moveStick;

    public Joystick MoveStick
    {
        get => moveStick;
        private set => moveStick = value;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
