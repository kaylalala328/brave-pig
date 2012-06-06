using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tile_Engine
{
    /// <summary>
    /// 직렬화
    /// serialization은 객체를 바이트 스트림으로 변환해 읽고 저장할 수 있는 것으로, 
    /// Map Editor와 게임에서 각 맵 파일을 생성하고 읽어 들일 수 있어야 하기 때문에 사용했다.
    /// </summary>
    [Serializable]
    public class MapSquare
    {
        #region Declarations
        /// <summary>
        /// 전경을 나타내는 정수 값 저장
        /// </summary>
        public int LayerTiles;
        /// <summary>
        /// 개별 타일에 추가적인 속성을 주기 위한 것
        /// 스테이지 이동, 트랩, 보이지 않는 장애물 등 여러 가지 속성이 될 수 있다.
        /// </summary>
        public string CodeValue = "";
        #endregion

        #region Constructor
        public MapSquare(int foreground, string code)
        {
            LayerTiles = foreground;
            CodeValue = code;
        }
        #endregion
    }
}
