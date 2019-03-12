using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMBasicPJK.Common.Interfaces
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);
    }
}
