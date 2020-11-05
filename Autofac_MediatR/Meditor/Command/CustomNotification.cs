using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Autofac_MediatR
{
    public class CustomNotification : INotification
    {
        public string MsgId { get; set; }
    }
}
