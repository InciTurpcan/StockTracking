using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.ResponseDto;

public record ProductDetailDto(Guid Id, string Name, int Stock, decimal Price, string CategoryName);

