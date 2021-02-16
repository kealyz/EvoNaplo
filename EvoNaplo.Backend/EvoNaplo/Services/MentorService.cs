using EvoNaplo.DataAccessLayer;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Services
{
    public class MentorService
    {
        private readonly EvoNaploContext _evoNaploContext;
        private readonly UserActionService _userActionService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MentorService(EvoNaploContext EvoNaploContext, UserActionService userActionService)
        {
            _evoNaploContext = EvoNaploContext;
            _userActionService = userActionService;
        }

        public async Task<string> SigninMentor(LoginDTO loginDto)
        {
            return await _userActionService.SignInUser(loginDto);
        }

        //Mentor hozzáadása. Ha van phone number akkor úgy hívja meg a konstruktort, egyébként anélkül
        public async Task<int> AddMentor(MentorDto mentorDto)
        {
            _logger.Debug($"Mentor hozzáadása következik: {mentorDto}");
            if (mentorDto.PhoneNumber != null)
            {
                await _evoNaploContext.Users2.AddAsync(new User(mentorDto.Email, mentorDto.Password, mentorDto.FirstName, mentorDto.LastName, mentorDto.PhoneNumber, mentorDto.UserRole));
            }
            else
            {
                await _evoNaploContext.Users2.AddAsync(new User(mentorDto.Email, mentorDto.Password, mentorDto.FirstName, mentorDto.LastName, mentorDto.UserRole));
            }
            if (await _userActionService.SignUpMentor(mentorDto) == StatusCodes.Status200OK)
            {
                await _userActionService.AddUserToRole(mentorDto.Email, "Mentor");
                _evoNaploContext.SaveChanges();
                _logger.Debug($"Mentor hozzáadva");
                return StatusCodes.Status200OK;
            }
            else
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        public IEnumerable<User> ListMentors()
        {
            var mentors = _evoNaploContext.Users2.Where(m => m.Role == Role.Mentor);
            return mentors.ToList();

        }

        public async Task<IEnumerable<User>> EditMentor(int id, MentorDto mentorDto)
        {
            _logger.Debug($"{id} ID-vel rendelkező mentor keresése");
            var mentorToEdit = await _evoNaploContext.Users2.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező mentor módosítása indul {mentorDto} adatokra");
            mentorToEdit.Email = mentorDto.Email;
            //mentorToEdit.SetNewPassword(mentorDto.Password);
            mentorToEdit.FirstName = mentorDto.FirstName;
            mentorToEdit.LastName = mentorDto.LastName;
            mentorToEdit.PhoneNumber = mentorDto.PhoneNumber;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező mentor módosítása kész");
            var mentors = _evoNaploContext.Users2.Where(m => m.Role == Role.Mentor);
            return mentors.ToList();
        }

        public async Task<IEnumerable<User>> InactivateMentor(int id)
        {
            _logger.Debug($"{id} ID-vel rendelkező mentor keresése");
            var mentorToDelete = await _evoNaploContext.Users2.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező mentor inaktiválása indul");
            mentorToDelete.IsActive = false;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező mentor inaktiválása kész");
            var mentors = _evoNaploContext.Users2.Where(m => m.Role == Role.Mentor);
            return mentors.ToList();
        }

        public async Task<IEnumerable<User>> DeleteMentor(int id)
        {
            _logger.Debug($"{id} ID-vel rendelkező mentor keresése");
            var mentorToDelete = await _evoNaploContext.Users2.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező mentor törlése indul");
            _evoNaploContext.Users2.Remove(mentorToDelete);
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező mentor törlése kész");
            var mentors = _evoNaploContext.Users2.Where(m => m.Role == Role.Mentor);
            return mentors.ToList();
        }
    }
}
