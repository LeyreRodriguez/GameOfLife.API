namespace GameOfLife.Business
{
    public enum State
    {
        Dead,
        Alive
    }

    internal class Cell
    {



        public State State { get; private set; }



        private int x { get; set; }
        private int y { get; set; }

        public Cell(State State, int x, int y)
        {
            this.State = State;
            this.x = x;
            this.y = y;
        }



        public bool isAlive()
        {
            return this.State == State.Alive;
        }

        public void UpdateState(State newState)
        {
            State = newState;
        }
    }
}