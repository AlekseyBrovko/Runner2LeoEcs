using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

namespace Client
{
    public class FinishTrigger : MonoBehaviour
    {
        public EcsWorld World;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                Debug.Log("---Finish---");

                ref var finishComp = ref World.GetPool<FinishComponent>().Add(World.NewEntity()); //�������� � ��� ����� ��������. ����� �������� �������, ������� ����� ����� ����������

                //��������� ����� ������ ���������� � ECS 
                //eventComp.CoinValue = 1;

                //other.gameObject.SetActive(false);
            }
        }
    }
}
