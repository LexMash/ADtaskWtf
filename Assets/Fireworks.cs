using System.Collections;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private AdController _adController;
    [SerializeField] private ParticleSystem _particles;

    private Coroutine _fireworks;

    private void Start()
    {
        _particles.Stop();
    }

    private void OnEnable()
    {
        _adController.OnAdShowCompleteEvent += FireworksStart;
    }

    private void OnDisable()
    {
        _adController.OnAdShowCompleteEvent -= FireworksStart;
    }

    private void FireworksStart()
    {
        _fireworks = StartCoroutine(FireworksCoroutine());
    }

    private IEnumerator FireworksCoroutine()
    {
        _particles.Play();

        yield return new WaitForSeconds(1f);

        _particles.Stop();

        StopCoroutine(_fireworks);
    }

}
