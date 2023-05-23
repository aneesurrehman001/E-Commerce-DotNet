using System;
using Core.Entities;

namespace Infrastructure
{
    public class generictest<TOne, TTwo> where TOne : BaseEntity where TTwo : Product
    {
        public void Addition<T1, T2>(T1 num1, T2 num2)
        {

        }
    }
}
