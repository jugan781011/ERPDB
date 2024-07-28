using AutoMapper;
using ERPDB.Model;
using ERPDB.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPDB.Repositorys
{
    public class tb_UserMemberRepository : Itb_UserMemberRepository
    {
        private readonly MyDbContext _myDbContext;
        private readonly IMapper _mapper;

        public tb_UserMemberRepository(MyDbContext myDbContext, IMapper iMapper)
        {
            _myDbContext = myDbContext;
            _mapper = iMapper;
        }

        public async Task<tb_UserMember> Add(tb_UserMember list)
        {
            list.CreationDate= DateTime.Now;
            var result = await _myDbContext.tb_UserMember.AddAsync(list);
            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var delete = await(from a in _myDbContext.tb_UserMember
                               where a.Id == id
                               select a).SingleOrDefaultAsync();


            if (delete != null)
            {
                _myDbContext.tb_UserMember.Remove(delete);
                _myDbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<tb_UserMember>> GetAll()
        {
            var UserMemberList = await _myDbContext.tb_UserMember.OrderBy(x => x.Id).ToListAsync();

            return UserMemberList;
        }

        public async Task<bool> SaveAsync()
        {
            return await _myDbContext.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<tb_UserMember>> Search([FromQuery] UserMemberParamaters paramaters)
        {
            var result = from a in _myDbContext.tb_UserMember
                         select a;

            if (!string.IsNullOrWhiteSpace(paramaters.UserName1))
            {
                result = result.Where(a => a.UserName.Contains(paramaters.UserName1.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(paramaters.Account1))
            {
                result = result.Where(a => a.Account.Contains(paramaters.Account1.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(paramaters.Tel1))
            {
                result = result.Where(a => a.Tel.Equals(paramaters.Tel1));
            }

            return await result.ToListAsync();
        }

        public async Task<tb_UserMember> Update(tb_UserMember list)
        {
            var result = await _myDbContext.tb_UserMember
               .FirstOrDefaultAsync(e => e.Id == list.Id);

            if (result != null)
            {               
                _myDbContext.tb_UserMember.Update(result).CurrentValues.SetValues(list);

                await _myDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
