using UnityEngine;

public class PlantDestruction : DestructionObject
{
    public float scale = 0.4755529f;
    public Transform plant;
    public override void OnHit(bool triggeredByPlayer = false)
    {
        if (isDestroyed || !canBeDestroyed && !triggeredByPlayer) return;
        plant.gameObject.SetActive(true);
        plant.parent = null;
        plant.localScale = Vector3.one * scale;
        plant.position = transform.position;
        base.OnHit();
    }
}
