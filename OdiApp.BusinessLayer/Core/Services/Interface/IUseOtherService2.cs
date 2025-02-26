using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Odi.Shared.Services.Interface
{
    public interface IUseOtherService2<T>
    {
        Task<T> PostMethod(object obj, string endpoint, string jwtToken);
        Task<List<T>> PostMethodList(object obj, string endpoint, string jwtToken);
        Task<T> GetMethod(string endpoint, string jwtToken);
        Task<List<T>> GetMethodList(string endpoint, string jwtToken);
    }
}
