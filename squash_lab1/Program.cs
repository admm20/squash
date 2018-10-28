using System;

// GRUPA 2

namespace squash_lab1
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //if (dist_x > 40)
            //    rad = 0.5235987756;
            //else
            //    rad = 0.7853981634;

            //double x2 = -(Math.Cos(rad) * speed.x_direction - Math.Sin(rad) * speed.y_direction);
            //double y2 = -(Math.Sin(rad) * speed.x_direction + Math.Cos(rad) * speed.y_direction);
            Console.WriteLine(Math.Cos(0.3490658504) * 0.3 - Math.Sin(0.3490658504) * 0.3);
            Console.WriteLine(Math.Sin(0.3490658504) * 0.3 + Math.Cos(0.3490658504) * 0.3);

            using (var game = new SquashGame())
                game.Run();
        }
    }

#endif
}
