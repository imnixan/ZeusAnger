using UnityEngine;
using DigitalRuby.LightningBolt;
using DG.Tweening;

public class ZeusStrike : MonoBehaviour
{
    private LightningBoltScript lightningBolt;

    [SerializeField]
    private Transform boltStart;

    [SerializeField]
    private Transform boltEnd;

    [SerializeField]
    private float strikeLength;

    private void Start()
    {
        lightningBolt = GetComponent<LightningBoltScript>();
        lightningBolt.enabled = false;
    }

    private void OnBoltStrike(Vector2 strikePosition)
    {
        Sequence boltStrike = DOTween
            .Sequence()
            .AppendCallback(() =>
            {
                lightningBolt.gameObject.SetActive(true);
            })
            .Append(boltEnd.DOMove(strikePosition, strikeLength))
            .Append(boltEnd.DOMove(boltStart.position, strikeLength))
            .AppendCallback(() =>
            {
                lightningBolt.gameObject.SetActive(false);
            })
            .Play();
    }

    private void OnEnable()
    {
        Enemy.ElementalStrike += OnBoltStrike;
        Jerry.JerryStrike += OnBoltStrike;
    }

    private void OnDisable()
    {
        Enemy.ElementalStrike -= OnBoltStrike;
        Jerry.JerryStrike -= OnBoltStrike;
    }
}
