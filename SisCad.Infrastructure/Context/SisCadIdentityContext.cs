using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SisCad.Infrastructure.Context
{
    public class SisCadIdentityContext : IdentityDbContext
    {
        #region Constructors

        public SisCadIdentityContext(DbContextOptions<SisCadIdentityContext> options)
            : base(options)
        {
        }

        #endregion Constructors
    }
}