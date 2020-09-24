using UnityEngine;
using System.Collections;

public class CollisionEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] LayerMask targetLayer = -1;
    [SerializeField] Vector3 offset = new Vector3(0, 0.03f, 0);
    [SerializeField] Vector3 scale = new Vector3(1, 1, 1);

    [SerializeField] float scalePerVelocity = 0.5f;
    [SerializeField] float maxScale = 2f;
    [SerializeField] float volumeScalePerVelocity = 0.5f;
    [SerializeField] float maxVolumeScale = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer & targetLayer) != 0)
        {
            foreach (var contact in collision.contacts)
            {
                var effectInstance = Instantiate(effect);
                var effectTransform = effectInstance.transform;

                effectTransform.position = contact.point + offset;

                var impulse = Vector3.Scale(collision.relativeVelocity, contact.normal).magnitude;
                var effectScale = effectTransform.localScale;

                effectTransform.localScale = Vector3.Scale(effectScale, scale) * Mathf.Min(impulse * scalePerVelocity, maxScale);

                audioSource.PlayOneShot(
                    audioClips[Random.Range(0, audioClips.Length)],
                    Mathf.Min(
                        impulse * volumeScalePerVelocity,
                        maxVolumeScale));
                break;
            }
        }
    }
}