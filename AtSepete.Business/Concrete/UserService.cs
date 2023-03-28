﻿using AtSepete.Business.Abstract;
using AtSepete.Dtos.Dto;
using AtSepete.Entities.Data;
using AtSepete.Repositories.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Concrete
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;


        }

        
    }
}
