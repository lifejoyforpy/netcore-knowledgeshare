using System;
using System.Collections.Generic;
using System.Text;
using SSOManagerLib.Dapper;
using SSOManagerLib.Model;

namespace SSOManagerLib.UserService
{
    public class UserServices : IUserServices
    {
       private  IDapperRepository _dapperRepository;
        public UserServices(IDapperRepository dapperRepository)
        {

            _dapperRepository = dapperRepository;
        }
        public List<string> GetDepartList()
        {
             return   _dapperRepository.Query<string>("select  DISTINCT  Department from ucenter_member WHERE  DataFlag=1 and UseStatus=1",null);
        }

        public List<string> GetFactoryList()
        {
            return _dapperRepository.Query<string>("select  DISTINCT  Factory from ucenter_member WHERE  DataFlag=1 and UseStatus=1", null);

        }

        public User GetUserInfo(string UserJobNo)
        {
            return _dapperRepository.QuerySingle<User>("select *  from ucenter_member WHERE  DataFlag=1 and UseStatus=1 and UserJobNo=:UserJobNo", new { UserJobNo = UserJobNo });
        }
    }
}
