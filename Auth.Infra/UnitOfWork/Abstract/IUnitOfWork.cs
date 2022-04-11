using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infra.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
    }
}
