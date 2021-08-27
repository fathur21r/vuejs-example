using Entity;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Post : Base
    {
        public static IEnumerable<GetPost> Get(PostEnt ent)
        {
            var connection = Create();
            var param = new
            {
                CatId = ConvertNull(ent.CategoryId),
                IsActiveState = ConvertNull(ent.IsActive)
            };
            return connection.ExecuteQuery<GetPost>("sp_GetPost", param: param, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<GetPost> GetById(PostEnt ent)
        {
            using (var connection = Create())
            {
                var param = new
                {
                    Id = ConvertNull(ent.Id)
                };
                return connection.ExecuteQuery<GetPost>("sp_GetPostById", param, commandType: CommandType.StoredProcedure);
            }
        }

        public static PostEnt Save(PostEnt ent)
        {
            var connection = Create();
            var trans = connection.EnsureOpen().BeginTransaction();

            try
            {
                var param = new
                {
                    Id = ConvertNull(ent.Id),
                    CategoryId = ent.CategoryId,
                    Content = ent.Content,
                    Title = ent.Title,
                    IsActive = Convert.ToBoolean(Convert.ToInt32(ent.IsActive))
                };
                ent.RowsAffected = connection.ExecuteNonQuery("sp_SavePost", param: param, transaction: trans, commandType: CommandType.StoredProcedure);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw;
            }
            return ent;
        }

        public static PostEnt Delete(PostEnt ent)
        {
            var connection = Create();
            var trans = connection.EnsureOpen().BeginTransaction();

            try
            {
                var param = new
                {
                    Id = ent.Id
                };
                ent.RowsAffected = connection.ExecuteNonQuery("sp_DeletePost", param: param, transaction: trans, commandType: CommandType.StoredProcedure);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw;
            }
            return ent;
        }
    }
}
