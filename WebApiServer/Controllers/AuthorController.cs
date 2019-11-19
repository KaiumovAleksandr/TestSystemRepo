﻿using System.Collections.Generic;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiServer.Repositories;

namespace WebApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorController(ILogger<AuthorController> logger, IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return _authorsRepository.Get();
        }

        [HttpGet("{id}")]
        public Author Get(int id)
        {
            return _authorsRepository.Get(id);
        }

        [HttpPost]
        public int Create(Author author)
        {
            return _authorsRepository.Create(author);
        }

        [HttpPut]
        public Author Update(Author author)
        {
            return _authorsRepository.Update(author);
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return _authorsRepository.Delete(id);
        }
    }
}
