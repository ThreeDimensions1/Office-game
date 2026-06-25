using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxDestruction : DestructionObject
{
    public ParticleSystem particles;
    public AudioSource sfx;

    public override void OnHit(bool triggeredByPlayer = false)
    {
        if (isDestroyed || !canBeDestroyed && !triggeredByPlayer) return;
        if(particles)
        {
            Transform trans = particles.transform;
            trans.parent = null;
            trans.localScale = Vector3.one;
            trans.position = transform.position;
            particles.Play();
        }
        if(sfx)
        {
            sfx.transform.parent = null;
            sfx.Play();
        }
        base.OnHit();
    }
}
