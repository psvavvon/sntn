﻿using System;

namespace SNTN
{
    namespace Core
    {
        internal static class Curricular
        {
            public static (int h, int m)[] GetCurricular(DateTimeOffset? startDateTime)
            {
                var curricular = new System.Collections.Generic.Queue<(int h, int m)>();
                DateTimeOffset sdt = (startDateTime ?? Constants.Dates.StartDateTime);
                DateTimeOffset postTime = DateTime.Now;
                Random rnd = new Random();
                int postsAmount = CalculatePostsAmount(startDateTime);
                for (int i = 0; i < postsAmount - 1; ++i)
                {
                    postTime = sdt.AddHours(i);
                    int offset = rnd.Next(5, 11);
                    postTime = postTime.AddMinutes(offset);
                    curricular.Enqueue((postTime.Hour, postTime.Minute));
                }
                // Last one is always on 23:59 to avoid bug with offset
                // Example: postTime = 23:59; offset = 3mins; 23:59 + 00:03 = 00:02 of the same day
                // TODO: fix 
                curricular.Enqueue((23, 59));
                return curricular.ToArray();
            }

            private static int CalculatePostsAmount(DateTimeOffset? startDateTime)
            {
                DateTimeOffset sdt = (startDateTime ?? Constants.Dates.StartDateTime);
                return (Constants.Dates.StopDateTime.Hour + 1) - sdt.Hour + 1;
            }
        }
    }
}
