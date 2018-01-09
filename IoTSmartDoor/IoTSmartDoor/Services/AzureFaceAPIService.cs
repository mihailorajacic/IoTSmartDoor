using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;

namespace IoTSmartDoor.Services
{
    public class AzureFaceAPIService : IFaceAPIService
    {
        private readonly IFaceServiceClient faceServiceClient = new FaceServiceClient("2d381ea6acc7491c8163db79b7b0439d", 
            "https://westeurope.api.cognitive.microsoft.com/face/v1.0");

        public async Task<AddPersistedFaceResult> AddFaceToFaceListAsync(string imageUrl, string faceListId)
        {
            using (var fileStream = System.IO.File.OpenRead(imageUrl))
            {
                return await faceServiceClient.AddFaceToFaceListAsync(faceListId, fileStream);
            }  
        }

        public async Task<Face[]> DetectAsync(string imageUrl)
        {
            using (var fileStream = System.IO.File.OpenRead(imageUrl))
            {
                return await faceServiceClient.DetectAsync(fileStream);
            }    
        }
    }
}
