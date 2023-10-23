

namespace GameOfLife.Business
{
    public interface BoardRepository
    {
        void Save(Board board);
        Board Load();
    }
}
