using CuraGames.Models;

namespace CuraGames.Interface
{
    public interface IGames
    {
        public List<GameInfo> GetGamesByRegion(List<string> regions);
    }
}
