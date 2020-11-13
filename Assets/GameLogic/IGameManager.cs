using System;

public abstract class IGameManager
{
    protected LostStoryGame lostStoryGame = null;
    public IGameManager(LostStoryGame LSGame)
    {
        lostStoryGame = LSGame;
    }

    public virtual void Initialize()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Release()
    {

    }
}

