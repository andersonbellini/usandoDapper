﻿using System.Collections.Generic;

namespace Usando_Dapper
{
    public class Fornecedor
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public IEnumerable<Produto> Produtos { get; set; }
    }
}
