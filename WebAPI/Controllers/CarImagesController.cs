using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
       
    
        ICarImageService _entityService;

        public CarImagesController(ICarImageService entityService)
        {
            _entityService = entityService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _entityService.GetAll();
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }
        [HttpGet("getfilebyid")]
        public IActionResult GetFileById(int id)
        {
            var result = _entityService.GetById(id);

            if (result.Success)
            {
                Byte[] b = System.IO.File.ReadAllBytes(result.Data.ImagePath);
                return File(b, "image/jpeg");
            }

            return BadRequest(result);
        }
        [HttpGet("getimagesbycarid")]
        public IActionResult GetImagesByCarId(int id)
        {
            var result = _entityService.GetImagesByCarId(id);
            if (result.Success) return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int carId)
        {
            var result = _entityService.GetByWithCarId(carId);
            
            
            if (result.Success)
            {
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory());
                foreach (var image in result.Data)
                {
                    image.ImagePath = Path.Combine(Directory.GetCurrentDirectory(), image.ImagePath);
                }
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("imageupload"), DisableRequestSizeLimit]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile [] files,int carId)
        {
            List<IResult> results=new List<IResult>();
        
            foreach (var file in files)
            {
                var result = _entityService.Add(file, carId);
                results.Add(result);
                if (!result.Success)
                {
                    
                    return BadRequest(results);
                }
                
            }

            return Ok(results) ;
        }

        //[HttpPost("imageupload2"),DisableRequestSizeLimit]
        //public IActionResult ImageUpload(int carId)
        //{

        //    try
        //    {
        //        CarImage carImage = new CarImage();


        //        var file = Request.Form.Files[0];

        //        string dbPath= SaveToFileDirectory(file);

        //        if (dbPath.Contains("Hata"))
        //        {
        //            return BadRequest(dbPath);
        //        }
        //        carImage.CarId = carId;
        //        carImage.Date = DateTime.Now;
        //        carImage.ImagePath = dbPath;
        //        var result=_entityService.Add(carImage);
        //        if (result.Success)
        //        {
        //            return Ok(new { dbPath });
        //        }
        //        DeleteFile(dbPath);
        //        return  BadRequest(result.Message);



        //    }
        //    catch(Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error:{ex}");

        //    }

        //}

        //[HttpPost("update")]
        //public IActionResult Update(int id)
        //{
        //    var entity = _entityService.GetById(id).Data;
        //    if (entity!=null)
        //    {
        //        DeleteFile(entity.ImagePath);
        //        var file = Request.Form.Files[0];
        //        string dbPath = SaveToFileDirectory(file);
        //        if (dbPath.Contains("Hata"))
        //        {
        //            return BadRequest(dbPath);
        //        }
        //        entity.ImagePath = dbPath;
        //        var result = _entityService.Update(entity);
        //        if (result.Success)
        //        {
        //            return Ok(result);

        //        }
        //        return BadRequest(result);
        //    }


        //    return BadRequest();
        //}

        //[HttpPost("delete")]
        //public IActionResult Delete(int id)
        //{
        //    var entity = _entityService.GetById(id).Data;
        //    if (entity!=null)
        //    {

        //        var result = _entityService.Delete(entity);
        //        if (result.Success)
        //        {
        //            DeleteFile(entity.ImagePath);
        //            return Ok(result);

        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest("Kayıt Bulunamadı");

        //}


        [HttpPost("delete")]
        public IActionResult Delete(int Id)
        {

            var carImage = _entityService.GetById(Id).Data;

            var result = _entityService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, int Id)
        {
            var carImage = _entityService.GetById(Id).Data;
            var result = _entityService.Update(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }




    }
}


