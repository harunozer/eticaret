using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace DataLayer
{
    public static class DbExtensions
    {
        #region Helpers
        public static bool HasColumn<T>(this IQueryable<T> query, string ColumnName)
        {
            Type elementType = query.ElementType;

            foreach (PropertyInfo pi in elementType.GetProperties())
            {
                if (pi.Name == ColumnName) return true;
            }

            return false;
        }
        #endregion

        public static IQueryable<T> Pageing<T>(this IQueryable<T> query, int PageNumber, int PageRecordCount)
        {
            if (PageNumber != 0 && PageRecordCount != 0)
            {
                int skipNum = (PageNumber * PageRecordCount) - PageRecordCount;
                query = query.Skip(skipNum).Take(PageRecordCount);

            }
            return query;
        }

        public static IQueryable<T> RetrieveNotSystemData<T>(this IQueryable<T> query)
        {
            if (!query.HasColumn("ID")) return query;

            var param = Expression.Parameter(typeof(T));

            var where = Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThan(Expression.Property(param, "ID"), Expression.Convert(Expression.Constant(0), typeof(int)))
                , param
            );

            return query.Where(where);
        }

        /// <summary>
        /// CancelID null olan kayıtlar için Query hazırlar. CancelID property yoksa querye eklenmez.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> RetrieveAll<T>(this IQueryable<T> query)
        {
            if (!query.HasColumn("CancelID")) return query;

            var param = Expression.Parameter(typeof(T));

            var where = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(Expression.Property(param, "CancelID"), Expression.Convert(Expression.Constant(null), typeof(int?))), param
            );

            return query.Where(where).RetrieveNotSystemData();
        }

        /// <summary>
        /// CancelID null veya pozitif olan kayıtlar için query verir. Silinmiş ve Sistemsel kayıtları vermez.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> RetrieveAllNotDeleted<T>(this IQueryable<T> query)
        {
            if (!query.HasColumn("CancelID")) return query;
            var param = Expression.Parameter(typeof(T));

            var where = Expression.Lambda<Func<T, bool>>(
                Expression.Or(
                    Expression.Equal(Expression.Property(param, "CancelID"), Expression.Convert(Expression.Constant(null), typeof(int?))),
                    Expression.GreaterThan(Expression.Property(param, "CancelID"), Expression.Convert(Expression.Constant(0), typeof(int?)))
                ),
                param
            );

            return query.Where(where).RetrieveNotSystemData();
        }

        private static IQueryable<T> RetrieveByID_<T>(this IQueryable<T> query, int ID)
        {
            if (!query.HasColumn("ID")) return query;
            var param = Expression.Parameter(typeof(T));

            var where = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(Expression.Property(param, "ID"), Expression.Constant(ID)), param
            );

            return query.Where(where).RetrieveNotSystemData();
        }

        /// <summary>
        /// ID alanına göre query oluşturur. ID yoksa query eklenmez. Aktif Kayıtlarda Arama yapar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static IQueryable<T> RetrieveByID<T>(this IQueryable<T> query, int ID)
        {
            return query.RetrieveByID_(ID).RetrieveNotSystemData().RetrieveAll();
        }

        /// <summary>
        /// ID alanına göre query oluşturur. ID yoksa query eklenmez. Silinmemiş Kayıtlarda Arama yapar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static IQueryable<T> RetrieveByIdNotDeleted<T>(this IQueryable<T> query,int ID)
        {
            return query.RetrieveByID_(ID).RetrieveNotSystemData().RetrieveAllNotDeleted();
        }
    }
}
