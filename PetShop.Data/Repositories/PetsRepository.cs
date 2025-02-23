﻿using Microsoft.EntityFrameworkCore;
using PetShop.Core.Base;
using PetShop.Core.Base.Interfaces;
using PetShop.Data.Context;
using PetShop.Data.Repositories.Interfaces;
using PetShop.Domain.Entities;
using PetShop.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repositories
{
    public class PetsRepository : RepositoryBase<Pets>, IPetsRepository
    {
        private readonly PetShopContext _context;

        public PetsRepository(PetShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Pets>> GetByUserId(int id)
        {
            var pets = await _context.Pets.Where(x => x.UserId == id).ToListAsync();

            return pets;
        }

        public async Task<List<Pets>> GetBySpecie(Species specie)
        {
            var pets = await _context.Pets.Where(x => x.Species == specie).ToListAsync();

            return pets;
        }

        public async Task<List<Pets>> GetByGender(Gender gender)
        {
            var pets = await _context.Pets.Where(x => x.Gender == gender).ToListAsync();

            return pets;
        }

    }
}
