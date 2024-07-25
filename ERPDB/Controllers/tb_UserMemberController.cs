using AutoMapper;
using ERPDB.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class tb_UserMemberController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _myDbContext;
        public tb_UserMemberController(IMapper iMapper, MyDbContext myDbContext)
        {
            _mapper = iMapper;
            _myDbContext = myDbContext;
        }


        ///<summary>      
        ///功能:全部使用者
        ///</summary>       
        [HttpGet]
        public async Task<IEnumerable<tb_UserMember>> GetAll()
        {
            var UserMemberList = await _myDbContext.tb_UserMember.OrderBy(x => x.Id).ToListAsync();

            var aa = 0;

            return UserMemberList;
        }
    }
}
