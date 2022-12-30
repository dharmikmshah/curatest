using CuraGames.Enums;
using CuraGames.Interface;
using CuraGames.Models;
using System.Linq;

namespace CuraGames.Repository
{
    public class GamesRepository : IGames
    {
        #region Private Methods
        List<GameInfo> _games = new List<GameInfo>()
        {
            new GameInfo(){GameId = 1, GameName = "Board Game 1",GameRegions = new List<string>{ GameRegion.Board.ToString()} },
            new GameInfo(){GameId = 2, GameName = "Board Game 2",GameRegions = new List<string>{ GameRegion.Board.ToString()} },
            new GameInfo(){GameId = 3, GameName = "Board Game 3",GameRegions = new List<string>{ GameRegion.Board.ToString()} },
            new GameInfo(){GameId = 4, GameName = "Board Game 4",GameRegions = new List<string>{ GameRegion.Board.ToString()} },
            new GameInfo(){GameId = 5, GameName = "Board Game 5",GameRegions = new List<string>{ GameRegion.Board.ToString()} },
            new GameInfo(){GameId = 6, GameName = "Board Game 6",GameRegions = new List<string>{ GameRegion.Board.ToString()} },
            new GameInfo(){GameId = 7, GameName = "VIP Game 1",GameRegions = new List<string>{ GameRegion.Vip.ToString()} },
            new GameInfo(){GameId = 8, GameName = "VIP Game 2",GameRegions = new List<string>{ GameRegion.Vip.ToString()} },
            new GameInfo(){GameId = 9, GameName = "VIP Game 3",GameRegions = new List<string>{ GameRegion.Vip.ToString()} },
            new GameInfo(){GameId = 10, GameName = "VIP Game 4",GameRegions = new List<string>{ GameRegion.Vip.ToString()} }
        };
        #endregion


        #region Public Methods
        public List<GameInfo> GetGamesByRegion(List<string> regions)
        {
            return _games.Where(x => x.GameRegions.Any(y => regions.Contains(y))).ToList();
        }
        #endregion
    }
}
