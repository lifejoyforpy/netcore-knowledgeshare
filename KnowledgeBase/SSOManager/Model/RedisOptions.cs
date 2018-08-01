﻿namespace SSOManagerLib.Model
{
    public class RedisOptions
    {
        public string WriteServerList { get; set; }

        /// <summary>
        /// 可读的Redis链接地址
        /// format:ip1,ip2
        /// </summary>
        public string ReadServerList { get; set; }

        /// <summary>
        /// 最大写链接数
        /// </summary>
        public int MaxWritePoolSize { get; set; } = 60;

        /// <summary>
        /// 最大读链接数
        /// </summary>
        public int MaxReadPoolSize { get; set; } = 60;

        /// <summary>
        /// 本地缓存到期时间，单位:秒
        /// </summary>
        public int LocalCacheTime { get; set; } = 180;

        /// <summary>
        /// 自动重启
        /// </summary>
        public bool AutoStart { get; set; } = true;

        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,
        /// 如redis工作正常,请关闭该项
        /// </summary>
        public bool RecordeLog { get; set; } = false;
    }
}