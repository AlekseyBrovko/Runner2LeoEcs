using Leopotam.EcsLite;
using UnityEngine;

namespace Client 
{
    sealed class InitPlayer : IEcsInitSystem 
    {
        public void Init (EcsSystems systems) 
        {
            var go = GameObject.FindGameObjectWithTag("Player"); //����� ������ �� �����

            var world = systems.GetWorld(); // ������ �� ���
            var entity = world.NewEntity(); // �������� ����� ��������

            world.GetPool<Player>().Add(entity);

            ref var moveComp = ref world.GetPool<PlayerMoveComponent>().Add(entity); // ���������� �������� � ��� Move
            moveComp.ForwardSpeed = 5;

            ref var viewComp = ref world.GetPool<ViewComponent>().Add(entity);
            viewComp.Transform = go.transform; // ��������� ������ �� transform �������

            viewComp.Rigidbody = go.GetComponent<Rigidbody>();

            ref var inputComp = ref world.GetPool<PlayerInputComponent>().Add(entity);
            moveComp.SideSpeed = 5f;
            
            var coins = go.GetComponent<CoinTrigger>();
            coins.World = world;

            var finish = go.GetComponent<FinishTrigger>();
            finish.World = world;
        }
    }
}