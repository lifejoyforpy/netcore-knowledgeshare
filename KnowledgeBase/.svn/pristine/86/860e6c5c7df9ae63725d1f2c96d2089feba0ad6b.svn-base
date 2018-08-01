using Core.Cache;
using SSOManagerLib.Dapper;
using SSOManagerLib.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSOManagerLib
{
    public class SSOHelper
    {
        private ICache _cache;

        public SSOHelper(ICache cache)
        {
            _cache = cache;
        }
        /// <summary>
        ///  //SSO登陆产生令牌
        /// </summary>
        /// <param name="userSSOInfo"></param>
        /// <returns></returns>

        public async Task<string> AddToken(UserSSOInfo userSSOInfo)
        {
            string token = $"{Guid.NewGuid().ToString()}";
            await _cache.AddAsync(token, userSSOInfo);
            return token;
        }
        public async Task<string> AddToken(UserSSOInfo userSSOInfo, TimeSpan expiresIn, bool isSliding = false)
        {
            string token = $"{Guid.NewGuid().ToString()}";
            await _cache.AddAsync(token, userSSOInfo, expiresIn, false);
            return token;
        }
        /// <summary>
        ///  退出清楚令牌
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveToekn(string key)
        {
            return await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// 查询token是否有效(在应用程序中)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
   
        public async Task<UserSSOInfo> ExistToken(string key)
        {
            var flag = await _cache.ExistsAsync(key);
            if (flag)
            {
                return await _cache.GetAsync<UserSSOInfo>(key);
            }
            return null;
        }

        /// <summary>
        //
        /// </summary>
        /// <returns></returns>
    
    }
}