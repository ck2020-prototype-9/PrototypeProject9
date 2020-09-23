using UnityEngine;
using System.Collections;

public class CollisionEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem effect;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] LayerMask targetLayer = -1;
    [SerializeField] Vector3 offset = new Vector3(0, 0.01f, 0);
    [SerializeField] Vector3 scale = new Vector3(1, 1, 1);

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.layer & targetLayer) != 0)
        {
            foreach (var contact in collision.contacts)
            {
                var effectInstance = Instantiate(effect);
                var effectTransform = effectInstance.transform;
                effectTransform.position = contact.point + offset;
                var effectScale = effectTransform.localScale;
                effectTransform.localScale = new Vector3(effectScale.x * scale.x, effectScale.y * scale.y, effectScale.z * scale.z);
                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
                break;
            }
        }
    }
}