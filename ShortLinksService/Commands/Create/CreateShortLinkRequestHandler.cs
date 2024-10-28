using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using ShortLinksService.Entities;
using ShortLinksService.Extensions;
using ShortLinksService.Models.MongoEntities;
using ShortLinksService.Repositories;
using Visus.Cuid;

namespace ShortLinksService.Commands.Create;

public class CreateShortLinkRequestHandler:IRequestHandler<CreateShortLinkRequest, string>
{
   private readonly PasswordHasher _passwordHasher;
   public readonly IShortLinkRepository _shortLinkRepository;
   private readonly IMapper _mapper;

   
   public CreateShortLinkRequestHandler(PasswordHasher hasher, IShortLinkRepository shortLinkRepository, IMapper mapper)
   {
      _shortLinkRepository = shortLinkRepository;
      _mapper = mapper;
      _passwordHasher = hasher;
   }

   public async Task<string> Handle(CreateShortLinkRequest request, CancellationToken token)
   {  
      var shortlink = new Cuid2(5).ToString();
      request.ShortUrl = shortlink;
      var dbEntity = _mapper.Map<CreateLinkEntity>(request); 
      var password = _passwordHasher.HashPassword(request.Password);
      dbEntity.Password = password;
      var mongoEntity = _mapper.Map<MongoEntity>(dbEntity);
      var result = await _shortLinkRepository.CreateAsync(mongoEntity, token);
      return result;
   }
}