using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using BigDemo.Repasitory.Repos;


namespace $NameSpace$.Models.Entities
{
    public class UnitOfWork : IDisposable
    {
        private OurDbContext _dbContext;
       

        //this implementation is just for AppUser case and we have to remove this because it is a poor man emplementation
        public UnitOfWork():this(new OurDbContext())
        {
        }
        public UnitOfWork(OurDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Configuration.ProxyCreationEnabled = false;


        }



        #region $Entity$Repository
        //---------------------------------------
        //$Entity$Repository
        //---------------------------------------
        private $Entity$Repository _$Entity$Repo;
        public $Entity$Repository $Entity$Repo
        {
            get
            {
                return _$Entity$Repo ?? (_$Entity$Repo = new $Entity$Repository(this._dbContext));
            }
        }
        //---------------------------------------
        #endregion

        #region DepartmentRepository
        //---------------------------------------
        //DepartmentRepository
        //---------------------------------------
        private DepartmentRepository _DepartmentRepo;
        public DepartmentRepository DepartmentRepo
        {
            get
            {
                return _DepartmentRepo ?? (_DepartmentRepo = new DepartmentRepository(this._dbContext));
            }
        }
        //---------------------------------------
        #endregion

        #region Commit
        //---------------------------------------
        //Commit
        //---------------------------------------
        public void Commit()
        {
            try
            {
                this._dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw e;
            }
        }
        //---------------------------------------
        #endregion


        #region Dispose
        //---------------------------------------
        //SitesModulesRepository
        //---------------------------------------

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //---------------------------------------
        #endregion
    }
}
