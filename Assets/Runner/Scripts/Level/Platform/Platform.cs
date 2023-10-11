using Runner.Core;
using Runner.Level.Player;
using UnityEngine;

namespace Runner.Level.Platform
{
    /// <summary>
    /// Контроллер бесконечной платформы. Перемещает тайлы платформы из начала в конец по мере перемещения игрока
    /// </summary>
    public class Platform : MonoBehaviour, IUpdatable
    {
        private ICharacter _player;
        
        private int _lastPlayerZPos;
        
        private int _numTiles;
        private float _platformTileLen;

        public void Initialize(ICharacter player)
        {
            _player = player;
            _numTiles = transform.childCount;
            var tile = GetComponentInChildren<PlatformTile>();
            _platformTileLen = tile.GetSize().y;
        }

        public void OnUpdate()
        {
            ObservePlayerPosition();
        }

        private void ObservePlayerPosition()
        {
            var playerZPos = (int) _player.Position.z;
            if (playerZPos % _platformTileLen == 0 && playerZPos != _lastPlayerZPos)
            {
                TranslatePlatformTile();
            }
            _lastPlayerZPos = playerZPos;
        }

        private void TranslatePlatformTile()
        {
            var firstTile = transform.GetChild(0);
            firstTile.position += Vector3.forward * _numTiles * _platformTileLen;
            firstTile.SetAsLastSibling();
        }
    }
}