﻿using Lumia_Business.Exceptions;
using Lumia_Business.Services.Abstracts;
using Lumia_Core.Models;
using Microsoft.AspNetCore.Mvc;
using FileNotFoundException = Lumia_Business.Exceptions.FileNotFoundException;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            List<Team> teams = _teamService.GetAllTeams();
            return View(teams);
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid) 
                return View();

            try
            {
                _teamService.AddTeam(team);
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existTeam = _teamService.GetTeam(x  => x.Id == id);
            if (existTeam == null)
                return NotFound();

            try
            {
                _teamService.DeleteTeam(id);
            }
            catch(EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            var existTeam = _teamService.GetTeam(x => x.Id == id);
            if (existTeam == null)
                return NotFound();

            return View(existTeam);
        }

        [HttpPost]
        public IActionResult Update(int id, Team team)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _teamService.UpdateTeam(id, team);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
