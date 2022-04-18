using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

namespace Client
{
    public class CoinTrigger : MonoBehaviour
    {
        public EcsWorld World;
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                Debug.Log("---Coin---");

                ref var eventComp = ref World.GetPool<GetBonusEvent>().Add(World.NewEntity()); //�������� � ��� GetBonusEvent ����� ��������. ����� �������� �������, ������� ����� ����� ����������

                //��������� ����� ������ ���������� � ECS 
                eventComp.CoinValue = 1;
                
                other.gameObject.SetActive(false);
            }
        }
    }
}
