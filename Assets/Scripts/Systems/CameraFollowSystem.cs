using Leopotam.EcsLite;
using UnityEngine;
using Leopotam.EcsLite.Di;

namespace Client 
{
    sealed class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem 
    {
        private int cameraEntity;
            
        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld(); // ������ �� ���
            var cameraEntity = systems.GetWorld().NewEntity(); // �������� ����� ��������
            var filter = world.Filter<CameraComponent>().End(); //������ �� ������
            var cameraPool = world.GetPool<CameraComponent>(); // ��������� ������� � ����������
                        
            cameraPool.Add(cameraEntity); 
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);//���������� �������� � ���

            cameraComponent.cameraTransform = Camera.main.transform;
            cameraComponent.cameraSmoothness = 0.5f;
            
            cameraComponent.curVelocity = Vector3.zero;
            cameraComponent.offset = new Vector3(0, 2, -5);

            this.cameraEntity = cameraEntity;
        }


        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = systems.GetWorld().Filter<ViewComponent>().End();
            var playerPool = systems.GetWorld().GetPool<ViewComponent>();
            var cameraPool = systems.GetWorld().GetPool<CameraComponent>();
            var viewPool = world.GetPool<ViewComponent>();

            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);

                ref ViewComponent viewComp = ref viewPool.Get(entity);

                Vector3 currentPosition = cameraComponent.cameraTransform.position;
                Vector3 targetPoint = viewComp.Transform.position + cameraComponent.offset;

                cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);

            }
        }
    }
}