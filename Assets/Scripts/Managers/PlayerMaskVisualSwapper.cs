using UnityEngine;

public class PlayerMaskVisualSwapper : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] private Sprite _repairMask;
    [SerializeField] private Sprite _destructionMask;
    [SerializeField] private Sprite _consolationMask;
    [SerializeField] private Sprite _frightMask;


    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        PlayerMask.Instance.OnChangedMask += PlayerMask_OnChangedMask;

        PutOnNewMask(_repairMask);
    }

    private void OnDestroy()
    {
        PlayerMask.Instance.OnChangedMask -= PlayerMask_OnChangedMask;
    }

    private void PlayerMask_OnChangedMask(object sender, PlayerMask.OnChangedMaskEventArgs e)
    {
        int swapsLeft = MaskSwapLimiter.Instance.GetCurrentMaskSwaps();

        switch (e.NewMask)
        {
            default:
            case Mask.Repair:
                PutOnNewMask(_repairMask);
                break;

            case Mask.Destruction:
                PutOnNewMask(_destructionMask);

                break;

            case Mask.Consolation:
                PutOnNewMask(_consolationMask);

                break;

            case Mask.Fright:
                PutOnNewMask(_frightMask);

                break;
        }
    }

    private void PutOnNewMask(Sprite newMaskSprite)
    {
        _spriteRenderer.sprite = newMaskSprite;
    }
}
