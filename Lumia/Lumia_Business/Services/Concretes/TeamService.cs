using Lumia_Business.Exceptions;
using Lumia_Business.Services.Abstracts;
using Lumia_Core.Models;
using Lumia_Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumia_Business.Services.Concretes
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamService(ITeamRepository teamRepository, IWebHostEnvironment webHostEnvironment)
        {
            _teamRepository = teamRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddTeam(Team team)
        {
            if (team == null) throw new NullReferenceException("Team not null");

            if (!team.ImgFile.ContentType.Contains("image/"))
                throw new FileContentTypeException("ImageFile", "File content type error");
            if (team.ImgFile.Length > 2097152)
                throw new FileSizeException("ImageFile", "File size error");

            string fileName = team.ImgFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\upload\team\" + fileName;
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                team.ImgFile.CopyTo(fileStream);
            }
            team.ImgUrl = fileName;

            _teamRepository.Add(team);
            _teamRepository.Commit();
        }

        public void DeleteTeam(int id)
        {
            var existTeam = _teamRepository.Get(x => x.Id == id);
            if (existTeam == null)
                throw new EntityNotFoundException("", "Entity not found");

            string path = _webHostEnvironment.WebRootPath + @"\upload\team\" + existTeam.ImgUrl;
            if (!File.Exists(path))
                throw new Exceptions.FileNotFoundException("ImageFile", "File not found");

            File.Delete(path);

            _teamRepository.Delete(existTeam);
            _teamRepository.Commit();
        }

        public List<Team> GetAllTeams(Func<Team, bool>? func = null)
        {
            return _teamRepository.GetAll(func);
        }

        public Team GetTeam(Func<Team, bool>? func = null)
        {
            return _teamRepository.Get(func);
        }

        public void UpdateTeam(int id, Team team)
        {
            var existTeam = _teamRepository.Get(x => x.Id == id);
            if (existTeam == null)
                throw new EntityNotFoundException("", "Entity not found");

            if(team.ImgFile != null)
            {
                if (!team.ImgFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImageFile", "File content type error");
                if (team.ImgFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "File size error");

                string fileName = team.ImgFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\upload\team\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    team.ImgFile.CopyTo(fileStream);
                }
                team.ImgUrl = fileName;

                existTeam.ImgUrl = team.ImgUrl;
            }

            existTeam.Fullname = team.Fullname;
            existTeam.Position = team.Position;
            existTeam.Description = team.Description;

            _teamRepository.Commit();
        }
    }
}
