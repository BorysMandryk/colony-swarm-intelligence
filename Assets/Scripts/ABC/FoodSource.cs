using System;
using UnityEngine;

public class FoodSource : ICloneable
{
    public Vector3 Position { get; set; }
    public Vector3 NewPosition { get; set; }
    public float Fitness { get; set; }
    public float NewFitness { get; set; }
    public int Trial { get; set; }

    public void UpdateFoodSource()
    {
        if (Position == NewPosition)
        {
            Trial++;
        }
        else
        {
            Trial = 0;
            Position = NewPosition;
            Fitness = NewFitness;
        }
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}


//public class FoodSource/* : ICloneable*/
//{
//    public Vector3 Position { get; set; }
//    public Vector3 NewPosition { get; set; }
//    public float Fitness { get; set; }
//    public float NewFitness { get; set; }
//    public int Trial { get; set; }

//    public void UpdateFoodSource()
//    {
//        if (Position == NewPosition)
//        {
//            Trial++;
//        }
//        else
//        {
//            Trial = 0;
//            Position = NewPosition;
//            Fitness = NewFitness;
//        }
//    }

//    //public object Clone()
//    //{
//    //    return this.MemberwiseClone();
//    //}
//}