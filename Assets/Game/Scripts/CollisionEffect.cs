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

    [SerializeField] float leastImpulse = 0;
    [SerializeField] float scalePerImpulse = 0.5f;
    [SerializeField] float maxScale = 2f;
    [SerializeField] float volumeScalePerImpulse = 0.5f;
    [SerializeField] float maxVolumeScale = 2f;

    [Header("Debug")]
    [SerializeField] float lastImpulse;

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer & targetLayer) != 0)
        {
            foreach (var contact in collision.contacts)
            {
                var impulse = Vector3.Scale(collision.relativeVelocity, contact.normal).magnitude;
                this.lastImpulse = impulse;
                if (leastImpulse < impulse)
                {
                    impulse -= leastImpulse;
                    var effectInstance = Instantiate(effect);
                    var effectTransform = effectInstance.transform;

                    effectTransform.position = contact.point + offset;

                    var effectScale = effectTransform.localScale;

                    effectTransform.localScale = Vector3.Scale(effectScale, scale) * Mathf.Min(impulse * scalePerImpulse, maxScale);

                    audioSource.PlayOneShot(
                        audioClips[Random.Range(0, audioClips.Length)],
                        Mathf.Min(
                            impulse * volumeScalePerImpulse,
                            maxVolumeScale));
                    break;
                }
            }
        }
    }
}