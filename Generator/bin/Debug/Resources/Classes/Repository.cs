using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using $NameSpace$.Models.Dto;
using $NameSpace$.Models.Entities;
using $NameSpace$.Utilities;
using System.Linq.Dynamic;
using PagedList;
using System.Data.Entity;
using System.Globalization;
using System.Data.Entity.Design.PluralizationServices;

namespace BigDemo.Repasitory.Repos
{
    public class $Entity$Repository: GenericRepository<$Entity$>
    {
        protected OurDbContext db;

        #region --------------Constructor--------------
        //---------------------------------------------------------------------
        //Constructor
        //---------------------------------------------------------------------
        public $Entity$Repository(OurDbContext dbContext):base(dbContext)
        {
            this.db = dbContext;
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Add$Entity$--------------
        //---------------------------------------------------------------------
        //Add$Entity$
        //---------------------------------------------------------------------
        /// <summary>
        /// add a new entity
        /// </summary>
        /// <param name="$Entity_smallCase$"></param>
        public void Add$Entity$($Entity$Dto dto)
        {
            $Entity$ $Entity_smallCase$Entity = ObjectMapper.GetEntity<$Entity$>(dto);
            db.$Entity$.Add($Entity_smallCase$Entity);
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Update$Entity$--------------
        //---------------------------------------------------------------------
        //Update$Entity$
        //---------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Update$Entity$($Entity$Dto dto)
        {
            $Entity$ $Entity_smallCase$Entity = this.db.$Entity$.Find(dto.$Entity$ID);
            ObjectMapper.UpdateEntity($Entity_smallCase$Entity,dto);
            $Entity_smallCase$Entity.CreationDate = $Entity_smallCase$Entity.CreationDate;
            $Entity_smallCase$Entity.ViewCounter = $Entity_smallCase$Entity.ViewCounter;

            //db.$Entity$.Add($Entity_smallCase$Entity);
        }
        //---------------------------------------------------------------------
        #endregion


        #region --------------Update$Entity$--------------
        //---------------------------------------------------------------------
        //Update$Entity$
        //---------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public void Update$Entity$Directly($Entity$Dto dto)
        {
            $Entity$ $Entity_smallCase$Entity = this.db.$Entity$.Find(dto.$Entity$ID);
            db.Entry($Entity_smallCase$Entity).State = EntityState.Modified;

            db.SaveChanges();
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Delete$Entity$--------------
        //---------------------------------------------------------------------
        //Delete$Entity$
        //---------------------------------------------------------------------
        public void Delete$Entity$(int $Entity_smallCase$Id)
        {
            $Entity$ $Entity_smallCase$Entity = this.db.$Entity$.Find($Entity_smallCase$Id);
            $Entity_smallCase$Entity.IsActive = true;
            //db.$Entity$.Remove($Entity_smallCase$Entity);
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Get$Entity$List--------------
        //---------------------------------------------------------------------
        //Get$Entity$List
        //---------------------------------------------------------------------
        public List<$Entity$> Get$Entity$List($Entity$ListFilter filter)
        {
            var listQuery = db.$Entity$.AsQueryable();
            if (filter != null && filter.DepartmentID>0)
            {
               
               listQuery = listQuery.Where(e => e.DepartmentID == filter.DepartmentID);
                
            }
            if (filter != null && !string.IsNullOrEmpty(filter.NameAr))
            {
                listQuery = listQuery.Where(e => e.NameAr.Contains(filter.NameAr));
            }
            if (filter != null && !string.IsNullOrEmpty(filter.NameEn))
            {
                listQuery = listQuery.Where(e => e.NameAr.Contains(filter.NameEn));
            }
                var entityList =
                db.$Entity$.Where(e => e.IsActive == true).OrderByDescending(e => e.$Entity$ID).ToList();

            return entityList;
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------GetPagedList--------------
        //---------------------------------------------------------------------
        //GetPagedList
        //---------------------------------------------------------------------
        public IPagedList<$Entity$> GetPagedList($Entity$ListFilter filter, int pageNumber, int pageSize, string searchValue, string sortColumnName, string sortDirection)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;
            var listQuery = db.$Entity$.AsQueryable();
            if (filter != null && filter.DepartmentID > 0)
            {
                listQuery = listQuery.Where(e => e.DepartmentID == filter.DepartmentID);
            }
            if (filter != null && !string.IsNullOrEmpty(filter.NameAr))
            {
                listQuery = listQuery.Where(e => e.NameAr.Contains(filter.NameAr));
            }
            if (filter != null && !string.IsNullOrEmpty(filter.NameEn))
            {
                listQuery = listQuery.Where(e => e.NameAr.Contains(filter.NameEn));
            }
            //
            // search
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                listQuery = listQuery.Where(x => x.NameAr.ToLower().Contains(searchValue.ToLower()) ||
                 x.NameEn.ToLower().Contains(searchValue.ToLower()) || x.Identifier.ToLower().Contains(searchValue.ToLower()) || x.ClassName.ToString().Contains(searchValue.ToLower()));
            }
            // sorting
            if (!string.IsNullOrEmpty(sortColumnName))
            {
                listQuery = listQuery.OrderBy(sortColumnName, sortDirection);
            }
            else
            {
                listQuery = listQuery.OrderByDescending(e=>e.$Entity$ID);
            }
            var pagedList = listQuery.ToPagedList(pageNumber, pageSize);

            return pagedList;
        }
        //---------------------------------------------------------------------
        #endregion

        #region --------------Get$Entity$--------------
        //---------------------------------------------------------------------
        //Get$Entity$
        //---------------------------------------------------------------------
        /// <summary>
        /// get $Entity_smallCase$ by Id
        /// </summary>
        /// <param name="$Entity_smallCase$"></param>
        public $Entity$Dto Get$Entity$(int $Entity_smallCase$Id)
        {
            $Entity$ $Entity_smallCase$Entity = db.$Entity$.Find($Entity_smallCase$Id);
            return ObjectMapper.GetEntity<$Entity$Dto>($Entity_smallCase$Entity);
        }
        //---------------------------------------------------------------------
        #endregion
    }
}
