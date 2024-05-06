public class BallDict
{
    public static BallDict NONE = new BallDict(0);
    public static BallDict RED = new BallDict(1);
    public static BallDict YELLOW = new BallDict(2);
    public static BallDict PURPLE = new BallDict(3);

    public int ballType;

    private BallDict(int type) {
        ballType = type;
    }

    public override bool Equals(object obj)
    {
        return ballType == ((BallDict)obj).ballType;
    }
}
