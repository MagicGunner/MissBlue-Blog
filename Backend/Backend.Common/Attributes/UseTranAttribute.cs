﻿using Backend.Common.Attributes;

namespace BCVP.Net8.Common {
    /// <summary>
    /// 这个Attribute就是使用时候的验证，把它添加到需要执行事务的方法中，即可完成事务的操作。
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class UseTranAttribute : Attribute {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;
    }
}