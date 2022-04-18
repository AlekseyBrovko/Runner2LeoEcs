using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

namespace Client 
{
    sealed class CoinsSystem : IEcsRunSystem 
    {
        readonly EcsFilterInject<Inc<GetBonusEvent>> _filter = default;

        readonly EcsPoolInject<GetBonusEvent> _eventPool = default;

        public void Run (EcsSystems systems) 
        {
            foreach (int entity in _filter.Value)
            {
                ref var eventComp = ref _eventPool.Value.Get(entity);


                // � ���� ������� ����� �������� ���� �������
                GameState state = systems.GetShared<GameState>();
                state.CoinsValue += eventComp.CoinValue;
                state.CoinsValueText.text = state.CoinsValue.ToString();

                //������� �� ����� ������� �����
                Debug.Log($"BonusValue = {state.CoinsValue}");

            }
        }
    }
}