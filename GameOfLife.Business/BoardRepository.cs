﻿

namespace GameOfLife.Business
{
    public interface BoardRepository
    {
        void Save(Board board, string id);
        Board Load(string id);
    }
}
