using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



class FlagManager : IGameManager
{
    public FlagManager(LostStoryGame lostStoryGame) : base(lostStoryGame)
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        flags = new Dictionary<string, int>();
    }

    public Dictionary<string, int> flags;

    public int GetFlagValue(string flag)
    {
        flags.TryGetValue(flag, out int value);
        return value;
    }

    public void SetFlagValue(string flag, int value)
    {
        if(flags.ContainsKey(flag))
        {
            flags[flag] = value;
        }
        else
        {
            flags.Add(flag, value);
        }
    }
}

