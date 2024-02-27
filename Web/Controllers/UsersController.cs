using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Core.Services.Interfaces;
using Web.Models;

namespace Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetUsers();
            var usersViewModel = _mapper.Map<List<UsersViewModel>>(users);
            return View(usersViewModel);
        }
        
    }
}