using DigiMedia.Business.Exceptions;
using DigiMedia.Business.Extensions;
using DigiMedia.Business.Services.Abstracts;
using DigiMedia.Core.Models;
using DigiMedia.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiMedia.Business.Services.Concretes
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IWebHostEnvironment _env;
        public SliderService(ISliderRepository sliderRepository, IWebHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }

        public async Task AddAsyncSlider(Slider slider)
        {
            if (slider.ImageFile == null)
                throw new EntityNotFoundException("Sekil olmalidir!");

            slider.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\sliders", slider.ImageFile);

            await _sliderRepository.AddAsync(slider);
            await _sliderRepository.CommitAsync();
        }

        public void DeleteSlider(int id)
        {
            var existSlider = _sliderRepository.Get(x => x.Id == id);

            if (existSlider == null)
                throw new EntityNotFoundException("Slider yoxdur!");

            Helper.DeleteFile(_env.WebRootPath, @"uploads\sliders", existSlider.ImageUrl);

            _sliderRepository.Delete(existSlider);
            _sliderRepository.Commit();
        }

        public List<Slider> GetAllSliders(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            return _sliderRepository.Get(func);
        }

        public void UpdateSlider(int id, Slider newSlider)
        {
            var oldSlider = _sliderRepository.Get(x => x.Id == id);

            if (oldSlider == null)
                throw new EntityNotFoundException("Slider yoxdur!");

            if(newSlider.ImageFile != null)
            {
                Helper.DeleteFile(_env.WebRootPath, @"uploads\sliders", oldSlider.ImageUrl);

                oldSlider.ImageUrl = Helper.SaveFile(_env.WebRootPath, @"uploads\sliders", newSlider.ImageFile);

            }

            oldSlider.Title = newSlider.Title;  
            oldSlider.Profession = newSlider.Profession;

            _sliderRepository.Commit();

        }
    }
}
