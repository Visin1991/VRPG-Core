using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V
{
    public abstract class AiStateInterface<T> where T : EntityCore
    {
        public abstract void Enter(T entity);
        public abstract void Execute(T entity);
        public abstract void Exit(T entity);
    }
}
