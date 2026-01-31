using UnityEngine;

public class MaskSelector : MonoBehaviour
{
    private int _activeSlotIndexNumber = 0;

    private PlayerInputActions _playerInputActions;

    [Header("Setup")]
    [SerializeField] private RectTransform _maskInventoryContainer;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
    }

    private void Start()
    {
        DeselectAllSlots();
        SelectActiveSlot(_activeSlotIndexNumber);
        //ChangeActiveMask();
    }

    private void OnEnable()
    {
        _playerInputActions.Inventory.Enable();
        _playerInputActions.Inventory.ToggleActiveMask.performed += ToggleActiveMask_performed;
        _playerInputActions.Inventory.SelectActiveMask.performed += SelectActiveMask_performed;
    }

    private void OnDisable()
    {
        _playerInputActions.Inventory.ToggleActiveMask.performed += ToggleActiveMask_performed;
        _playerInputActions.Inventory.SelectActiveMask.performed += SelectActiveMask_performed;
        _playerInputActions.Inventory.Disable();
    }

    private void SelectActiveMask_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int choosenIndex = (int)obj.ReadValue<float>() - 1;

        DeselectAllSlots();
        SelectActiveSlot(choosenIndex);
        //ChangeActiveMask();
    }

    private void ToggleActiveMask_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int direction = (int)obj.ReadValue<float>();

        int choosenIndex = _activeSlotIndexNumber + direction;

        if (choosenIndex < 0)
        {
            choosenIndex = _maskInventoryContainer.childCount - 1;
        }
        else if (choosenIndex > _maskInventoryContainer.childCount - 1)
        {
            choosenIndex = 0;
        }

        _activeSlotIndexNumber = choosenIndex;

        DeselectAllSlots();
        SelectActiveSlot(_activeSlotIndexNumber);
        //ChangeActiveMask();
    }




    private void SelectActiveSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > _maskInventoryContainer.childCount - 1)
        {
            return;
        }

        _activeSlotIndexNumber = slotIndex;

        if (_maskInventoryContainer.GetChild(slotIndex).TryGetComponent(out MaskInventorySlot maskInventorySlot))
        {
            maskInventorySlot.Select();
        }
        else
        {
            Debug.LogError("Cannot select inventory mask slot");
        }
    }

    private void DeselectAllSlots()
    {
        foreach (RectTransform maskInventorySlotTransform in _maskInventoryContainer)
        {
            if (maskInventorySlotTransform.TryGetComponent(out MaskInventorySlot maskInventorySlot))
            {
                maskInventorySlot.Deselect();
            }
            else
            {
                Debug.LogError("Cannot deselect inventory mask slot");
            }    

        }
    }

    //public void ChangeActiveMask()
    //{
    //   //To implement
    //}
}
