using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyTshirt.Application.Abstractions
{
    public interface ITshirtService
    {
        List<TshirtResponse> GetAll();

        TshirtResponse? GetById(Guid id);

        TshirtResponse Create(TshirtRequest tshirt);

        TshirtResponse Update(Guid id, TshirtRequest tshirt);

        bool Delete(Guid id);
    }
}
