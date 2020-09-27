using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkuSuka.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAccounts()
        {

            try
            {
                var accounts = _repository.Account.GetAllAccounts();

                _logger.LogInfo($"Returned all accounts from database.");

                var accountsResult = _mapper.Map<IEnumerable<AccountDto>>(accounts);

                return Ok(accountsResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "AccountById")]
        public IActionResult GetAccountById(Guid id)
        {
            try
            {
                var account = _repository.Account.GetAccountById(id);

                if (account == null)
                {
                    _logger.LogError($"Account with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned account with id: {id}");
                    var accountResult = _mapper.Map<AccountDto>(account);

                    return Ok(accountResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAccountById action: {ex.Message}");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountForCreationDto account)
        {
            try
            {
                if (account == null)
                {
                    _logger.LogError($"Account object send from client is null");
                    return BadRequest("Account object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid account object send from client.");
                    return BadRequest("Invalid model object");
                }

                var accountEntity = _mapper.Map<Account>(account);

                _repository.Account.CreateAccount(accountEntity);
                _repository.Save();

                var createdAccount = _mapper.Map<AccountDto>(accountEntity);

                return CreatedAtRoute("AccountById", new { id = createdAccount.Id}, createdAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccount action: {ex.Message}");
                return StatusCode(500, "Internal server error" + ex.Message);
            }
            
        }
    }
}
