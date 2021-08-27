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
    public class Category : Base
    {
        public static IEnumerable<GetCategory> Get(CategoryEnt ent)
         {
            var connection = Create();
            var param = new
            {
                Name = ConvertNull(ent.Name),
                IsActiveState = ConvertNull(ent.IsActive),
            };
            return connection.ExecuteQuery<GetCategory>("sp_GetCategory", param, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<GetCategory> GetById(CategoryEnt ent)
        {
            using (var connection = Create())
            {
                var param = new
                {
                    Id = ConvertNull(ent.Id)
                };
                return connection.ExecuteQuery<GetCategory>("sp_GetCategoryById", param, commandType: CommandType.StoredProcedure);
            }
        }

        public static CategoryEnt Save(CategoryEnt ent)
        {
            var connection = Create();
            var trans = connection.EnsureOpen().BeginTransaction();

            try
            {
                var prov = new
                {
                    Id = ConvertNull(ent.Id),
                    Name = ent.Name,
                    IsActive = Convert.ToBoolean(Convert.ToInt32(ent.IsActive))
                };
                ent.RowsAffected = connection.ExecuteNonQuery("sp_SaveCategory", param: prov, transaction: trans, commandType: CommandType.StoredProcedure);
                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                throw;
            }
            return ent;
        }

        public static CategoryEnt Delete(CategoryEnt ent)
        {
            var connection = Create();
            var trans = connection.EnsureOpen().BeginTransaction();

            try
            {
                var prov = new
                {
                    Id = ent.Id
                };
                ent.RowsAffected = connection.ExecuteNonQuery("sp_DeleteCategory", param: prov, transaction: trans, commandType: CommandType.StoredProcedure);
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
