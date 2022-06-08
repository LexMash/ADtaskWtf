using System.Collections;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private float _playTime;

    private Coroutine _fireworksRoutine;

    private void Start()
    {
        _particles.Stop();
    }

    public void Play()
    {
        if (_fireworksRoutine != null)
        {
            StopCoroutine(_fireworksRoutine);
        }

        _fireworksRoutine = StartCoroutine(FireworksCoroutine());
    }

    private IEnumerator FireworksCoroutine()
    {
        _particles.Play();

        yield return new WaitForSeconds(_playTime);

        _particles.Stop();
    }
}
