using AutoMapper;
using ERPDB.Model;
using ERPDB.Repositorys;
using ERPDB.ResourceParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class tb_UserMemberController : ControllerBase
    {
        private Itb_UserMemberRepository _tb_UserMemberRepository;
        private readonly IMapper _mapper;
        private readonly MyDbContext _myDbContext;
        public tb_UserMemberController(Itb_UserMemberRepository tb_UserMemberRepository, IMapper iMapper, MyDbContext myDbContext)
        {
            _tb_UserMemberRepository = tb_UserMemberRepository;
            _mapper = iMapper;
            _myDbContext = myDbContext;
        }


        ///<summary>      
        ///功能:取得全部使用者
        ///</summary>       
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _tb_UserMemberRepository.GetAll());
            }
            catch (Exception)
            {             
                return StatusCode(StatusCodes.Status500InternalServerError,"系統錯誤");
            }
        }

        ///<summary>      
        ///功能:查詢使用者
        ///</summary>       
        [HttpGet("Search")]
        public async Task<ActionResult> Search([FromQuery] UserMemberParamaters paramaters)
        {
            try
            {
                return Ok(await _tb_UserMemberRepository.Search(paramaters));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "系統錯誤");
            }
        }


        ///<summary>      
        ///功能:新增
        ///</summary>   
        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] tb_UserMember tb_UserMember)
        {
            try
            {
                var created = await _tb_UserMemberRepository.Add(tb_UserMember);
                await _tb_UserMemberRepository.SaveAsync();

                return Ok(created);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "系統錯誤");
            }
        }


        ///<summary>      
        ///功能:修改
        ///</summary>   
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(tb_UserMember userMember_Web)
        {
            try
            {   
                var result = await _tb_UserMemberRepository.Update(userMember_Web);
                await _tb_UserMemberRepository.SaveAsync();
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "系統錯誤");
            }
        }


        ///<summary>      
        ///功能:刪除
        ///</summary>   
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _tb_UserMemberRepository.Delete(id);
                await _tb_UserMemberRepository.SaveAsync();
                return Ok($"刪除成功");
            }
            catch (Exception)
            {               
                return StatusCode(StatusCodes.Status500InternalServerError, "系統錯誤");
            }
        }

    }
}
