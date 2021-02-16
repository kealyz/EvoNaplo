using EvoNaplo.DataAccessLayer;
using EvoNaplo.Models;
using EvoNaplo.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoNaplo.Services
{
    public class SemesterService
    {
        private readonly EvoNaploContext _evoNaploContext;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SemesterService(EvoNaploContext EvoNaploContext)
        {
            _evoNaploContext = EvoNaploContext;
        }
        
        public async Task<IEnumerable<Semester>> PostAddSemester(SemesterDto semesterDto)
        {
            _logger.Debug($"Szemeszter hozzáadása következik: {semesterDto}");
            await _evoNaploContext.Semesters.AddAsync(new Semester(semesterDto.StartDate, semesterDto.EndDate, semesterDto.DemoDate));
            _evoNaploContext.SaveChanges();
            _logger.Debug($"Szemeszter hozzáadva");
            return _evoNaploContext.Semesters.ToList();
        }

        public IEnumerable<Semester> GetSemesters()
        {
            return _evoNaploContext.Semesters.ToList();
        }

        public async Task<IEnumerable<Semester>> PutDeleteSemester(int id)
        {
            _logger.Debug($"{id} ID-vel rendelkező szemeszter keresése");
            var semesterToDelete = await _evoNaploContext.Semesters.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező szemeszter deaktiválása indul");
            semesterToDelete.IsActive = false;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező szemeszter deaktiválása kész");
            return _evoNaploContext.Semesters.ToList(); 
        }

        public async Task<IEnumerable<Semester>> PutEditSemester(int id, SemesterDto semesterDto)
        {
            _logger.Debug($"{id} ID-vel rendelkező szemeszter keresése");
            var semesterToEdit = await _evoNaploContext.Semesters.FindAsync(id);
            _logger.Debug($"{id} ID-vel rendelkező szemeszter módosítása indul {semesterDto} adatokra");
            semesterToEdit.StartDate = semesterDto.StartDate;
            semesterToEdit.EndDate = semesterDto.EndDate;
            semesterToEdit.DemoDate = semesterDto.DemoDate;
            _evoNaploContext.SaveChanges();
            _logger.Debug($"{id} ID-vel rendelkező szemeszter módosítása kész");
            return _evoNaploContext.Semesters.ToList();
        }
    }
}
