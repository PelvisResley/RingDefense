using System;

namespace Ring_Defense
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = App.Instance)
                game.Run();
        }
    }
}
