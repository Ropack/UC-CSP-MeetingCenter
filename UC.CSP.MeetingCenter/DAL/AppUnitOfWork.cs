using System;

namespace UC.CSP.MeetingCenter.DAL
{
    public class AppUnitOfWork : IDisposable
    {
        public AppUnitOfWork()
        {
            Context = new AppDbContext();
        }
        public AppDbContext Context { get; }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public event EventHandler Disposing;

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Disposing?.Invoke(this, EventArgs.Empty);
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}