using UnityEngine;

public class GameplayWidget : MonoBehaviour
{
    
    [SerializeField] private JoyStick moveStick;

    public JoyStick MoveStick
    {
        get => moveStick;
        private set => moveStick = value;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
