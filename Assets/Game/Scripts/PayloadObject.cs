

using UnityEngine;

class PayloadObject : ResettableObject
{
    bool isOutBasket = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void StageReset()
    {
        // 현재 위치가 바구니 안이면 삭제
        if (!isOutBasket)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            isOutBasket = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            isOutBasket = true;
        }
    }


}