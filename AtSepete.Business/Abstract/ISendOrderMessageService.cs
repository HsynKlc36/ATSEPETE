using Castle.DynamicProxy.Contributors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Abstract
{
    public interface ISendOrderMessageService
    {

        Task TriggerExecution();
        Task GetCreatedOrders(string message);
    }
}
