

using UnityEngine;

class PayloadObject : ResettableObject
{
    bool isOutBasket = true;
    bool init = false;

    public bool IsOutBasket => this.isOutBasket;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void StageReset()
    {
        // 현재 위치가 바구니 안이면 삭제
        if (!isOutBasket || !init)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            isOutBasket = false;
            init = true;
            //GameStageStatisticsManager.Instance.Data.FallenObjectCount -= 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            isOutBasket = true;
            //GameStageStatisticsManager.Instance.Data.FallenObjectCount += 1;
        }
    }


}