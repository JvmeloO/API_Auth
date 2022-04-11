﻿using Auth.Domain.Entities;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;

namespace Auth.Infra.Repositories.Concrete
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(authdbContext context) : base(context) { }
    }
}
