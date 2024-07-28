using ERPDB.Model;
using ERPDB.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace ERPDB.Repositorys
{
    public interface Itb_UserMemberRepository
    {
        ///<summary>
        ///取得全部使用者資料
        ///</summary>
        Task<IEnumerable<tb_UserMember>> GetAll();

        ///<summary>
        ///使用者帳號查詢
        ///</summary>
        Task<IEnumerable<tb_UserMember>> Search([FromQuery] UserMemberParamaters paramaters);

        ///<summary>
        ///新增使用者
        ///</summary>
        Task<tb_UserMember> Add(tb_UserMember list);

        ///<summary>
        ///修改使用者
        ///</summary>
        Task<tb_UserMember> Update(tb_UserMember list);

        ///<summary>
        ///刪除使用者
        ///</summary>
        Task Delete(int id);

        ///<summary>
        ///儲存
        ///</summary>
        Task<bool> SaveAsync();
    }
}
