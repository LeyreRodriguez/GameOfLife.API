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



        private void isDead()
        {
            State = State.Dead;
        }

        private void Alive()
        {
            State = State.Alive;
        }

        public State GetState()
        {
            return State;
        }

        public void UpdateState(State newState)
        {
            State = newState;
        }
    }
}