using System;

namespace BoeSj.KnowledgeBase.Domain.Model
{
    public class BiAuth_Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        //new add 角色类型
        public int RoleType { get; set; }

        public int CheckStatus { get; set; }
        public string CheckNote { get; set; }
        public int UseStatus { get; set; }
        public int DataFlag { get; set; }
        public string Created_By { get; set; }
        public string Created_By_Id { get; set; }
        public DateTime Created_Time { get; set; }
        public string Last_Modify_By { get; set; }
        public string Last_Modify_By_Id { get; set; }
        public DateTime Last_Modify_Time { get; set; }
        public string Guid { get; set; }
    }
}