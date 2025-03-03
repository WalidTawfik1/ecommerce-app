using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositores
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _context;
        private readonly IImageManagmentService _imageManagmentService;
        private readonly IMapper _mapper;
        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public UnitofWork(AppDbContext context, IImageManagmentService imageManagmentService, IMapper mapper)
        {
            _context = context;
            _imageManagmentService = imageManagmentService;
            _mapper = mapper;

            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _mapper, _imageManagmentService);
            PhotoRepository = new PhotoRepository(_context);

        }
    }
}
