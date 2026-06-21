using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxDestruction : DestructionObject
{
    public ParticleSystem particles;

    public override void OnHit()
    {
        if(particles)
        {
            Transform trans = particles.transform;
            trans.parent = null;
            trans.localScale = Vector3.one;
            trans.position = transform.position;
            particles.Play();
        }
        base.OnHit();
    }
}
